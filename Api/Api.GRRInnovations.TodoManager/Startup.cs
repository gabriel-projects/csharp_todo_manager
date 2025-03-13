using Api.GRRInnovations.TodoManager.Application.Services;
using Api.GRRInnovations.TodoManager.Domain.Extensions;
using Api.GRRInnovations.TodoManager.Infrastructure.Helpers;
using Api.GRRInnovations.TodoManager.Infrastructure.Persistence;
using Api.GRRInnovations.TodoManager.Infrastructure.Persistence.Repositories;
using Api.GRRInnovations.TodoManager.Infrastructure.Security.Authentication;
using Api.GRRInnovations.TodoManager.Infrastructure.Services;
using Api.GRRInnovations.TodoManager.Interfaces.Authentication;
using Api.GRRInnovations.TodoManager.Interfaces.Repositories;
using Api.GRRInnovations.TodoManager.Interfaces.Services;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

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
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            var assm = AppDomain.CurrentDomain.GetAssemblies()
                .Where(p => p.FullName.StartsWith("Api")).ToList();


            var jwtSettings = new JwtSettings();
            Configuration.GetSection("JwtSettings").Bind(jwtSettings);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
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


            var googleClientId = Configuration["Authentication:Google:ClientId"];
            var googleSecretId = Configuration["Authentication:Google:ClientSecret"];

            var gitHubClientId = Configuration["Authentication:GitHub:ClientId"];
            var gitHubSecretId = Configuration["Authentication:GitHub:ClientSecret"];

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
            .AddCookie()
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

                options.ClaimActions.MapJsonKey("urn:github:id", "id");
                options.ClaimActions.MapJsonKey("urn:github:login", "login");
                options.ClaimActions.MapJsonKey("urn:github:name", "name");
                options.ClaimActions.MapJsonKey("urn:github:email", "email");
                options.ClaimActions.MapJsonKey("urn:github:avatar", "avatar_url");

                options.SaveTokens = true;

                options.Events = new OAuthEvents
                {
                    OnCreatingTicket = async context =>
                    {
                        var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", context.AccessToken);
                        request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                        var response = await context.Backchannel.SendAsync(request);
                        response.EnsureSuccessStatusCode();

                        var user = System.Text.Json.JsonDocument.Parse(await response.Content.ReadAsStringAsync());
                        context.RunClaimActions(user.RootElement);
                    }
                };
            });

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

            var connectionString = Configuration.GetConnectionString("SqlConnectionString");
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

            Console.WriteLine($"sqlConnection Startup: {connectionString}");
            Console.WriteLine($"databaseUrl Startup: {databaseUrl}");

            var connection = string.IsNullOrEmpty(databaseUrl) ? connectionString : ConnectionHelper.BuildConnectionString(databaseUrl);

            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connection));


            //todo: move the dependency injections for other static class
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICryptoService, CryptoService>();


            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IOpenAIService, OpenAIService>();

            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));
            services.AddSingleton<IJwtService, JwtService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //todo: mover para metodo async
            //cron
            var scope = app.ApplicationServices.CreateScope();
            _ = MigrationHelper.ManageDataAsync(scope.ServiceProvider);

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
