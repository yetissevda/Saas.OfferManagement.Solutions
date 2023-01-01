using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Saas.Core.Utilities.IoC;

namespace Saas.Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManager :ICacheManager
    {

        private readonly IMemoryCache _memoryCache;
        public MemoryCacheManager() : this(ServiceTool.ServiceProvider.GetService<IMemoryCache>())
        {
        }
        public MemoryCacheManager(IMemoryCache cache)
        {
            _memoryCache = cache;
        }
        public void Add(string key,object data,int duration)
        {
            _memoryCache.Set(key,data,TimeSpan.FromMinutes(duration));
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public object Get(string key)
        {
            return _memoryCache.Get(key);
        }

        public bool IsAdd(string key)
        {
            return _memoryCache.TryGetValue(key,out _);//var yok kontrol
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection",System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            var cacheEntriesCollection = cacheEntriesCollectionDefinition!.GetValue(_memoryCache) as dynamic;

            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();

            if (cacheEntriesCollection != null)
                foreach (var cacheItem in cacheEntriesCollection)
                {
                    ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                    cacheCollectionValues.Add(cacheItemValue);
                }

            var regex = new Regex(pattern,RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString() ?? throw new InvalidOperationException())).Select(d => d.Key).ToList();

            //06.04.2021
            foreach (var key in keysToRemove)
            {
                _memoryCache.Remove(key);
            }

        }
    }
}
