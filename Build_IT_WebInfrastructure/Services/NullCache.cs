using System;
using System.Threading.Tasks;
using Build_IT_WebApplication.Common.Interfaces;

namespace Build_IT_WebInfrastructure.Services
{
    public class NullCache : IDataCache
    {
        public Task<T> GetCacheData<T>(string key)
        {
            return Task.FromResult(default(T));
        }

        public Task<bool> RemoveData(string key)
        {
            return Task.FromResult(true);
        }

        public Task<bool> SetCacheData<T>(string key, T value, TimeSpan expirationTime)
        {
            return Task.FromResult(true);
        }
    }
}
