using Microsoft.Extensions.Caching.Memory;
using System;

namespace Sympli.Search.Usecases.Cache
{
    public class MemoryCacheAdapter : ICache
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheAdapter(IMemoryCache memory)
        {
            _memoryCache = memory;
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public void Set<T>(string key, T value, TimeSpan time)
        {
            _memoryCache.Set(key, value, time);
        }
    }
}
