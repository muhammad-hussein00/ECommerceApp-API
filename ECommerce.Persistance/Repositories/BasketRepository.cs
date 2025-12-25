using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.BasketModule;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Persistance.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer connection)
        {
            _database = connection.GetDatabase();
        }
        #region Create or update
        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan timeToLive = default)
        {
            var JsonBasket = JsonSerializer.Serialize(basket);
            var isCreatedOrUpdated = await _database.StringSetAsync(basket.Id, JsonBasket, (timeToLive == default) ?
                                                                             TimeSpan.FromDays(7) : timeToLive);


            return await GetBasketAsync(basket.Id);
        } 
        #endregion

        #region Delete
        public Task<bool> DeleteBasket(string BasketId) => _database.KeyDeleteAsync(BasketId);

        #endregion

        #region Get basket
        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
            var redisValuebasket = await _database.StringGetAsync(basketId);

            if (redisValuebasket.IsNullOrEmpty)
                return null;

            return JsonSerializer.Deserialize<CustomerBasket>(redisValuebasket!);
        } 
        #endregion
    }
}