using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Build_IT_CommonTools.Interfaces;
using Build_IT_WebApplication.Common.Interfaces;
using Newtonsoft.Json;

namespace Build_IT_WebInfrastructure.Services
{
    public class MemoryCache : IDataCache
    {
        private readonly IDateTime _dateTime;
        private static Dictionary<string, (DateTimeOffset expireDateTime, string value)> _datas = new Dictionary<string, (DateTimeOffset expireDateTime, string value)>();

        public MemoryCache(IDateTime dateTime)
        {
            _dateTime = dateTime ?? throw new ArgumentNullException(nameof(dateTime));
        }

        public Task<T> GetCacheData<T>(string key)
        {
            if (_datas.TryGetValue(key, out (DateTimeOffset expireDateTime, string value) data))
            {
                if (data.expireDateTime >= _dateTime.UtcNow)
                    return Task.FromResult(JsonConvert.DeserializeObject<T>(data.value));
            }
            return Task.FromResult(default(T));
        }

        public Task<bool> RemoveData(string key)
        {
            var isKeyExists = _datas.ContainsKey(key);
            if (isKeyExists)
                return Task.FromResult(_datas.Remove(key));
            return Task.FromResult(false);
        }

        public Task<bool> SetCacheData<T>(string key, T value, TimeSpan expirationTime)
        {
            try
            {
                DateTimeOffset expireTime = _dateTime.UtcNow.Add(expirationTime);
                if (_datas.ContainsKey(key))
                {
                    _datas[key] = (expireTime, JsonConvert.SerializeObject(value));
                    return Task.FromResult(true);
                }
                _datas.Add(key, (expireTime, JsonConvert.SerializeObject(value)));
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }
    }
}
