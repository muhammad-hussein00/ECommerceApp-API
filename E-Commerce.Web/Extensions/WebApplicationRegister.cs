using ECommerce.Domain.Contracts;
using ECommerce.Persistance.Data.DataSeed;
using ECommerce.Persistance.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace E_Commerce.Web.Extentions
{
    public static class WebApplicationRegister
    {
        public static async Task<WebApplication> MigrateDataBaseAsync(this WebApplication app)
        {
            await using var scope =  app.Services.CreateAsyncScope();
            var storeDbContext = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
            var pendingMigration = await storeDbContext.Database.GetPendingMigrationsAsync();

            if (pendingMigration.Any())
                await storeDbContext.Database.MigrateAsync();

            return app;
        }

        public static async Task<WebApplication> SeedDataAsync(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();
            var dataIntializer = scope.ServiceProvider.GetRequiredService<IDataInitializer>();
            await dataIntializer.InitializeAsync();
            return app;
        }
    }
}
