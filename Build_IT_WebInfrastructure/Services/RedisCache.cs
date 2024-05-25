using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Build_IT_CommonTools.Interfaces;
using Build_IT_WebApplication.Common.Interfaces;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Build_IT_WebInfrastructure.Services
{
    public class RedisCache : IDataCache
    {
        private readonly IDatabase _database;
        private readonly IDateTime _dateTime;

        public RedisCache(IConnectionMultiplexer connectionMultiplexer, IDateTime dateTime)
        {
            ArgumentNullException.ThrowIfNull(connectionMultiplexer);

            _database = connectionMultiplexer.GetDatabase();
            _dateTime = dateTime ?? throw new ArgumentNullException(nameof(dateTime));
        }  

        public async Task<T> GetCacheData<T>(string key)
        {
            var value = await _database.StringGetAsync(key);
            if (!string.IsNullOrEmpty(value))
                return JsonConvert.DeserializeObject<T>(value);
            return default;
        }

        public async Task<bool> RemoveData(string key)
        {
            var isKeyExists = await _database.KeyExistsAsync(key);
            if (isKeyExists)
                return await _database.KeyDeleteAsync(key);
            return false;
        }

        public async Task<bool> SetCacheData<T>(string key, T value, TimeSpan expirationTime)
        {
            var isSet = await _database.StringSetAsync(key, JsonConvert.SerializeObject(value), expirationTime);
            return isSet;
        }
    }
}
