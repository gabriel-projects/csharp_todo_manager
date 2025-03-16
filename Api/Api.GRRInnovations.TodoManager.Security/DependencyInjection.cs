using Api.GRRInnovations.TodoManager.Security.Interfaces;
using Api.GRRInnovations.TodoManager.Security.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Api.GRRInnovations.TodoManager.Security
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSecurityServices(this IServiceCollection services)
        {
            services.AddScoped<ICryptoService, CryptoService>();

            return services;
        }
    }
}
