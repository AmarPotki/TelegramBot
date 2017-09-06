namespace TelegramBot.Common.Cache
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class Cache<T> : ICache<T>
    {
        private readonly Dictionary<string, CachInfo<T>> _cacheDictionary;
        public Cache( )
        {
            _cacheDictionary = new Dictionary<string, CachInfo<T>>();
        }

        public void Set(string key, T obj, int duration)
        {
            var info = new CachInfo<T>
            {
                InsertTime = DateTime.UtcNow.Ticks,
                CachedObject = obj,
                Duration = duration
            };            
            _cacheDictionary[key] = info;
        }

        public T Get(string key)
        {
            if (_cacheDictionary.ContainsKey(key))
            {
                return _cacheDictionary[key].CachedObject;
            }
            return default(T);
        }

        public async Task SetAsync(string key, T obj, int duration)
        {
            var info = new CachInfo<T>
            {
                InsertTime = DateTime.UtcNow.Ticks,
                CachedObject = obj,
                Duration = duration
            };
            _cacheDictionary[key] = info;
        }

        public async Task<T> GetAsync(string key)
        {
            if (_cacheDictionary.ContainsKey(key))
            {
                return _cacheDictionary[key].CachedObject;
            }
            return default(T);
        }
    }
}