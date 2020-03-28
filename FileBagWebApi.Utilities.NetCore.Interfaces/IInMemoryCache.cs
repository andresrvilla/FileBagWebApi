using System;

namespace FileBagWebApi.Utilities.NetCore.Interfaces
{
    public interface IInMemoryCache
    {
        bool TryGetValue<T>(string entryKey, out T cacheEntry);

        T Set<T>(string entryKey, T cacheEntry, InMemoryCacheOffset offset);

        void Remove(string entryKey);
    }
}
