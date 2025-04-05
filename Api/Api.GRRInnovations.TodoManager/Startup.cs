using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Api.GRRInnovations.TodoManager.Application;
using Api.GRRInnovations.TodoManager.Domain.Entities;
using Api.GRRInnovations.TodoManager.Infrastructure;
using Api.GRRInnovations.TodoManager.Infrastructure.Hangfire;
using Api.GRRInnovations.TodoManager.Infrastructure.Helpers;
using Api.GRRInnovations.TodoManager.Infrastructure.Security.Authentication;
using Api.GRRInnovations.TodoManager.Security;
using Asp.Versioning;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.IdentityModel.Tokens;

namespace Api.GRRInnovations.TodoManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            ConfigureSwagger(services);
            ConfigureSecurity(services);
            ConfigureInfrastructure(services);
            ConfigureApplication(services);
            ConfigureAuthentication(services);
            ConfigureApiVersioning(services);
            ConfigureHttpClients(services);

            services.AddAuthorization();

            services.AddHangfireServer();

            services.AddLogging(logging =>
            {
                logging.AddConsole();
            });

            services.AddHttpContextAccessor();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<IUrlHelperFactory, UrlHelperFactory>();
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        private void ConfigureSecurity(IServiceCollection services)
        {
            services.AddSecurityServices();
        }

        private void ConfigureInfrastructure(IServiceCollection services)
        {
            services.AddInfrastructureServices(Configuration);
        }

        private void ConfigureApplication(IServiceCollection services)
        {
            services.AddApplicationServices();
        }

        private void ConfigureAuthentication(IServiceCollection services)
        {

            var jwtSection = Configuration.GetSection("JwtSettings");
            services.Configure<JwtSettings>(jwtSection);

            var jwtSettings = new JwtSettings();
            jwtSection.Bind(jwtSettings);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
                };
            });

            ConfigureOAuthProviders(services);
        }

        private void ConfigureApiVersioning(IServiceCollection services)
        {
            // learn more about https://www.milanjovanovic.tech/blog/api-versioning-in-aspnetcore

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1);
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("X-Api-Version"));
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });
        }

        private void ConfigureHttpClients(IServiceCollection services)
        {
            var openAISecret = Configuration["Authentication:OpenAI:ClientSecret"];

            services.AddHttpClient("OpenAI", client =>
            {
                client.BaseAddress = new Uri("https://api.openai.com/v1/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            }).ConfigureHttpClient(client =>
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", openAISecret);
            });
        }

        private void ConfigureOAuthProviders(IServiceCollection services)
        {
            var googleClientId = Configuration["Authentication:Google:ClientId"];
            var googleSecretId = Configuration["Authentication:Google:ClientSecret"];

            var gitHubClientId = Configuration["Authentication:GitHub:ClientId"];
            var gitHubSecretId = Configuration["Authentication:GitHub:ClientSecret"];

            services.AddAuthentication()
                .AddGoogle(googleOptions =>
                {
                    googleOptions.ClientId = googleClientId;
                    googleOptions.ClientSecret = googleSecretId;
                })
                .AddOAuth("GitHub", options =>
                {
                    options.ClientId = gitHubClientId;
                    options.ClientSecret = gitHubSecretId;
                    options.CallbackPath = new PathString("/github-response");

                    options.AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
                    options.TokenEndpoint = "https://github.com/login/oauth/access_token";
                    options.UserInformationEndpoint = "https://api.github.com/user";

                    options.Scope.Add("user:email");

                    options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
                    options.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
                    options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
                    options.ClaimActions.MapJsonKey("urn:github:avatar", "avatar_url");

                    options.SaveTokens = true;
                    options.Events = new OAuthEvents
                    {
                        OnCreatingTicket = async context =>
                        {
                            var email = await GetGitHubEmailAsync(context);
                            if (!string.IsNullOrEmpty(email))
                            {
                                var identity = (ClaimsIdentity)context.Principal.Identity;
                                identity.AddClaim(new Claim(ClaimTypes.Email, email));
                            }

                            var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint)
                            {
                                Headers =
                                {
                                    Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken),
                                    Accept = { new MediaTypeWithQualityHeaderValue("application/json") }
                                }
                            };

                            var response = await context.Backchannel.SendAsync(request);
                            response.EnsureSuccessStatusCode();

                            var user = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
                            context.RunClaimActions(user.RootElement);
                        }
                    };
                });
        }

        private static async Task<string> GetGitHubEmailAsync(OAuthCreatingTicketContext context)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.github.com/user/emails");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", context.AccessToken);
            request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var response = await context.Backchannel.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var emails = JsonSerializer.Deserialize<List<GitHubEmail>>(json);

            return emails?.FirstOrDefault(e => e.Primary && e.Verified)?.Email;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            var scope = app.ApplicationServices.CreateScope();
            _ = MigrationHelper.ManageDataAsync(scope.ServiceProvider);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // to acess dashboard shoud be /hangfire
            app.UseHangfireDashboard();

            HangfireJobsInitializer.ConfigureRecurringJobs();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            RecurringJob.AddOrUpdate("job-teste", () => Console.WriteLine("Executando job no PostgreSQL..."), Cron.Minutely);
        }
    }
}
