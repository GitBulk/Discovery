using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace Tam.Core.Cache
{
    public class MemoryCache
    {
        private readonly IMemoryCache cache;
        public MemoryCache(IMemoryCache cache)
        {
            this.cache = cache;
        }

        public string Get(string key)
        {
            string resut = cache.Get(key) as string;
            return resut;
        }

        public T Get<T>(string key)
        {
            return cache.Get<T>(key);
        }

        public void Set<T>(string key, T value, PostEvictionDelegate evictionDelegate)
        {
            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));
            //.RegisterPostEvictionCallback(evictionDelegate);
            if (evictionDelegate != null)
            {
                options.RegisterPostEvictionCallback(evictionDelegate);
            }

            cache.Set<T>(key, value, options);
        }
    }
}
