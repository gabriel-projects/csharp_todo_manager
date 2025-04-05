using Api.GRRInnovations.TodoManager.Infrastructure.Hangfire;
using Api.GRRInnovations.TodoManager.Infrastructure.Hangfire.Jobs;
using Api.GRRInnovations.TodoManager.Infrastructure.Helpers;
using Api.GRRInnovations.TodoManager.Infrastructure.Interfaces;
using Api.GRRInnovations.TodoManager.Infrastructure.Persistence;
using Api.GRRInnovations.TodoManager.Infrastructure.Persistence.Repositories;
using Api.GRRInnovations.TodoManager.Infrastructure.Repositories;
using Api.GRRInnovations.TodoManager.Infrastructure.Security;
using Api.GRRInnovations.TodoManager.Infrastructure.Security.Authentication;
using Api.GRRInnovations.TodoManager.Infrastructure.Services;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Api.GRRInnovations.TodoManager.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IOpenAIService, OpenAIService>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserClaimsMapper, UserClaimsMapper>();

            AddDbContext(services, configuration);
            AddContextHandFire(services, configuration);

            services.AddSingleton<IJwtService, JwtService>();

            return services;
        }

        private static void AddContextHandFire(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITaskCleanupService, TaskCleanupService>();
            services.AddScoped<ITaskReminderService, TaskReminderService>();

            string connection = GetConnectionStringSQL(configuration);

            services.AddHangfire(config =>
            {
                config.UsePostgreSqlStorage(x =>
                {
                    x.UseNpgsqlConnection(connection);
                });
            });
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            string connection = GetConnectionStringSQL(configuration);

            Console.WriteLine($"connection Startup: {connection}");

            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connection));
        }

        private static string GetConnectionStringSQL(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlConnectionString");
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

            Console.WriteLine($"sqlConnection Startup: {connectionString}");
            Console.WriteLine($"databaseUrl Startup: {databaseUrl}");

            var connection = string.IsNullOrEmpty(databaseUrl) ? connectionString : ConnectionHelper.BuildConnectionString(databaseUrl);
            return connection;
        }
    }
}
