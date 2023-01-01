using System;
using System.Threading.Tasks;
using Core.Utilities.Results;

namespace Business.BusinessAspects.Redis
{
    public interface IRedisCacheService
    {
        void Set(string key, string value);
        void Set<T>(string key, T value) where T : class;
        Task SetAsync(string key, object value);
        void Set(string key, object value, TimeSpan expiration);
        Task SetAsync(string key, object value, TimeSpan expiration);
        T Get<T>(string key) where T : class;
        string Get(string key);
        void Remove(string key);
        bool IsAdd(string key);
        Task<string> GetAsync<T>(string key) where T : class;
        //Task<T> GetAsync<T>(string key) where T : class;
        void Update<T>(string key, T value) where T : class;
        void Update<T>(string key, T value, TimeSpan expiration) where T : class;
    }
}
