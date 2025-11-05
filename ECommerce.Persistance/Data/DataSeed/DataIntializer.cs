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
        public void Initialize()
        {
            // Checking if tables has data before seeding
            var hasProduct = _storeDbContext.Products.Any();
            var hasProductTypes = _storeDbContext.productTypes.Any();
            var hasProductBrands = _storeDbContext.productBrands.Any();
            if(hasProduct && hasProductTypes && hasProductBrands)
                return;

            // Seed data for product types and product brands first.
            if (!hasProductTypes)
                SeedDataFromJSON<ProductType, int>("types.json", _storeDbContext.productTypes);

            if (!hasProductBrands)
                SeedDataFromJSON<ProductBrand, int>("types.json", _storeDbContext.productBrands);

            _storeDbContext.SaveChanges();

            // seed data for product
            if (!hasProductTypes)
                SeedDataFromJSON<ProductType, int>("types.json", _storeDbContext.productTypes);

            _storeDbContext.SaveChanges();
        }

        #region Helper methods
        private void SeedDataFromJSON<T, TKey>(string fileName, DbSet<T> dbset) where T : BaseEntity<TKey>
        {
            var filePath = @"..\ECommerce.Persistance\Data\DataSeed\JSONFiles" + fileName;
            if(!File.Exists(filePath))
                throw new FileNotFoundException(filePath);
            try
            {
                var dataStream = File.OpenRead(filePath);
                var data = JsonSerializer.Deserialize<List<T>>(dataStream, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                if(data != null)
                    dbset.AddRange(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while seeding data from JSON file. {ex}");
            }
        }
        #endregion
    }
}
