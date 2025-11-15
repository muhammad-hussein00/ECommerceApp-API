using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Entities.ProductModule;
using ECommerce.Persistance.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Persistance.Data.DataSeed
{
    public class DataIntializer : IDataInitializer
    {
        private readonly StoreDbContext _storeDbContext;

        public DataIntializer(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
        }
        public async Task InitializeAsync()
        {
            // Checking if tables has data before seeding
            var hasProduct = await _storeDbContext.Products.AnyAsync();
            var hasProductTypes = await _storeDbContext.productTypes.AnyAsync();
            var hasProductBrands = await _storeDbContext.productBrands.AnyAsync();
            if(hasProduct && hasProductTypes && hasProductBrands)
                return;

            // Seed data for product types and product brands first.
            if (!hasProductBrands)
                await SeedDataFromJSONAsync<ProductBrand, int>("brands.json", _storeDbContext.productBrands);

            if (!hasProductTypes)
                await SeedDataFromJSONAsync<ProductType, int>("types.json", _storeDbContext.productTypes);

            await _storeDbContext.SaveChangesAsync();

            // seed data for product
            if (!hasProduct)
                await SeedDataFromJSONAsync<Product, int>("products.json", _storeDbContext.Products);

            await _storeDbContext.SaveChangesAsync();
        }

        #region Helper methods
        private async Task SeedDataFromJSONAsync<T, TKey>(string fileName, DbSet<T> dbset) where T : BaseEntity<TKey>
        {
            var filePath = @"..\ECommerce.Persistance\Data\DataSeed\JSONFiles\" + fileName;
            if(!File.Exists(filePath))
                throw new FileNotFoundException(filePath);
            try
            {
                var dataStream = File.OpenRead(filePath);
                var data = await JsonSerializer.DeserializeAsync<List<T>>(dataStream, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                if(data != null)
                    await dbset.AddRangeAsync(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while seeding data from JSON file. {ex}");
            }
        }
        #endregion
    }
}
