using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ServiceStack.Redis;
using StackExchange.Redis;

namespace Business.BusinessAspects.Redis
{
    public class RedisCacheManager : IRedisCacheService
    {
        private readonly ConnectionMultiplexer _client;

        public RedisCacheManager(IConfiguration configuration)
        {
            //TODO: Redisin bağlanacağı url appsettings.json "localhost:6379" olarak ayarlandı.
            var connectionString = configuration.GetSection("RedisConfiguration:ConnectionString")?.Value;
            string databaseValue = configuration.GetSection("RedisConfiguration:DefaultDatabase")?.Value == "" ?
                configuration.GetSection("RedisConfiguration:DefaultDatabase").Value : "0";

            int dbNumber = Convert.ToInt32(databaseValue);

            ConfigurationOptions options = new ConfigurationOptions
            {
                DefaultDatabase = dbNumber,
                EndPoints =
                {
                    connectionString //Redis'e bağlanılacak olan url.
                },
                AbortOnConnectFail = false,//Redise bağlanamadığı durumda 
                SyncTimeout = 10000, //Redis'e async isteklerde 10 sn den geç yanıt verirse timeouta düşmesi için
                ConnectTimeout = 10000 //Redis'e normal isteklerde 10 sn den geç yanıt verirse timeouta düşmesi için
            };
            _client = ConnectionMultiplexer.Connect(options); // Redise bağlanmak için.
        }
        public T Get<T>(string key) where T : class
        {
            string value = _client.GetDatabase().StringGet(key);
            return value.ToObject<T>();
        }

        public string Get(string key)
        {
            return _client.GetDatabase().StringGet(key);
        }

        public void Remove(string key)
        {
            _client.GetDatabase().KeyDelete(key);
        }

        public void Set(string key, string value)
        {
            _client.GetDatabase().StringSet(key, value);
        }

        public void Set<T>(string key, T value) where T : class
        {
            //_client.GetDatabase().SetAdd(key, value.ToJson());
            _client.GetDatabase().StringSet(key, value.ToJson());
        }

        public void Set(string key, object value, TimeSpan expiration)
        {
            _client.GetDatabase().StringSet(key, value.ToJson(), expiration);
        }

        public Task SetAsync(string key, object value)
        {
            return _client.GetDatabase().StringSetAsync(key, value.ToJson());
        }

        public Task SetAsync(string key, object value, TimeSpan expiration)
        {
            return _client.GetDatabase().StringSetAsync(key, value.ToJson(), expiration);
        }


        public bool IsAdd(string key)
        {
            return _client.GetDatabase().StringGet(key) != RedisValue.Null;//var yok kontrol
        }

        //public async Task<T> GetAsync<T>(string key) where T : class
        //{

        //    return await _client.GetDatabase().StringGetAsync(key);
        //}
        public async Task<string> GetAsync<T>(string key) where T : class
        {

            return await _client.GetDatabase().StringGetAsync(key);
        }

        public void Update<T>(string key, T value) where T : class
        {
            _client.GetDatabase().SetAdd(key, value.ToJson());
        }

        public void Update<T>(string key, T value, TimeSpan expiration) where T : class
        {
            Remove(key);
            Set(key, value, expiration);
        }

    }
}
