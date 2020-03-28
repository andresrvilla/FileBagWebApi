using FileBagWebApi.Utilities.NetCore.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace FileBagWebApi.Utilities.NetCore
{
    public class InMemoryCache : IInMemoryCache
    {

        private IMemoryCache _cache;

        public InMemoryCache(IMemoryCache cache)
        {
            _cache = cache;
        }

        public bool TryGetValue<T>(string entryKey, out T cacheEntry)
        {
            return _cache.TryGetValue<T>(entryKey, out cacheEntry);
        }

        public T Set<T>(string entryKey, T cacheEntry, InMemoryCacheOffset offset)
        {
            if (offset == InMemoryCacheOffset.NoLimit)
            {
                return _cache.Set<T>(entryKey, cacheEntry, new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.NeverRemove));
            }
            else
            {
                return _cache.Set<T>(entryKey, cacheEntry, TimeSpanResolver.Resolve(offset));
            }
        }

        public void Remove(string entryKey)
        {
            throw new NotImplementedException();
        }
    }

}
