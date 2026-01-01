using ECommerce.Domain.Contracts;
using ECommerce.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Services
{
    public class CacheService : ICacheService
    {
        private readonly ICacheRepository _cachRepository;

        public CacheService(ICacheRepository cachRepository)
        {
            _cachRepository = cachRepository;
        }
        public async Task<string?> GetAsync(string cachKey)
        {
            return await _cachRepository.GetAsync(cachKey);
        }

        public async Task SetAsync(string cachKey, object cachValue, TimeSpan timeToLive)
        {
            var redisValue = JsonSerializer.Serialize(cachValue, new JsonSerializerOptions()
            {
                 PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await _cachRepository.SetAsync(cachKey, redisValue, timeToLive);
        }
    }
}
