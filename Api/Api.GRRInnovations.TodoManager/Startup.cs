using Api.GRRInnovations.TodoManager.Domain.Extensions;
using Api.GRRInnovations.TodoManager.Infrastructure.Authentication;
using Api.GRRInnovations.TodoManager.Infrastructure.Helpers;
using Api.GRRInnovations.TodoManager.Infrastructure.Persistence;
using Api.GRRInnovations.TodoManager.Infrastructure.Persistence.Repositories;
using Api.GRRInnovations.TodoManager.Interfaces.Authentication;
using Api.GRRInnovations.TodoManager.Interfaces.Repositories;
using Api.GRRInnovations.TodoManager.Services;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = JwtService.Issuer,
                            ValidAudience = JwtService.Audience,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtService.Key))
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

            services.AddScoped<UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();

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
