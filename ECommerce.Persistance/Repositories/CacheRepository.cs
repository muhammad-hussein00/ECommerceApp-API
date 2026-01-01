using ECommerce.Domain.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistance.Repositories
{
    public class CacheRepository : ICacheRepository
    {
        private readonly IDatabase _database;
        public CacheRepository(IConnectionMultiplexer connectionMultiplexer)
        {
            _database = connectionMultiplexer.GetDatabase();
        }
        public async Task<string?> GetAsync(string cachKey)
        {
            var cachedValue = await _database.StringGetAsync(cachKey);

            return cachedValue.IsNullOrEmpty? null: cachedValue.ToString();
        }

        public async Task SetAsync(string cachKey, string value, TimeSpan timeToLive)
        {
            await _database.StringSetAsync(cachKey, value, timeToLive);
        }
    }
}
