using System.Collections.Generic;

namespace LP.Api.Shared.Interfaces.Core.Caching
{
    public interface IMemoryCacheWrapper<T> where T : class
    {
        List<T> Get();
        void Add(T userCacheItem);
        void Set(List<T> userCacheItems);
    }
}
