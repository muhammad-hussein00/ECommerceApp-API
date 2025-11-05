using ECommerce.Persistance.Data.DataSeed;
using ECommerce.Persistance.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Web.Extentions
{
    public static class WebApplicationRegister
    {
        public static WebApplication MigrateDataBase(this WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var storeDbContext = scope.ServiceProvider.GetRequiredService<StoreDbContext>();

            if (storeDbContext.Database.GetPendingMigrations().Any())
                storeDbContext.Database.Migrate();

            return app;
        }

        public static WebApplication SeedData(this WebApplication app)
        {
            var scope = app.Services.CreateScope();
            var dataIntializer = scope.ServiceProvider.GetRequiredService<DataIntializer>();
            dataIntializer.Initialize();
            return app;
        }
    }
}
