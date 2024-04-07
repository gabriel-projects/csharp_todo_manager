using Api.GRRInnovations.TodoManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.GRRInnovations.TodoManager.Infrastructure.Helpers
{
    public static class MigrationHelper
    {
        public static async Task ManageDataAsync(IServiceProvider svcProvider)
        {
            Console.WriteLine("Aplicando migração CW");

            var dbContextSvc = svcProvider.GetRequiredService<ApplicationDbContext>();

            await dbContextSvc.Database.MigrateAsync();
        }
    }
}
