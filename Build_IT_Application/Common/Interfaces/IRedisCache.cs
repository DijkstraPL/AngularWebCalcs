using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Build_IT_WebApplication.Common.Interfaces
{
    public interface IDataCache
    {
        Task<T> GetCacheData<T>(string key);
        Task<bool> RemoveData(string key);
        Task<bool> SetCacheData<T>(string key,T value, TimeSpan expirationTime);
    }
}
