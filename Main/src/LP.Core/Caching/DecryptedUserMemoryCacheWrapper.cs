using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using LP.Api.Shared.Interfaces.Core.Caching;
using LP.Model.Authentication;

namespace LP.Core.Caching
{
    public class DecryptedUserMemoryCacheWrapper : IMemoryCacheWrapper<DecryptedUser>
    {
        private const string DecryptedUserCacheKey = "decryptedUserCache";

        private readonly MemoryCache _memoryCache = MemoryCache.Default;

        public List<DecryptedUser> Get()
        {
            return (List<DecryptedUser>)_memoryCache.Get(DecryptedUserCacheKey);
        }

        public void Add(DecryptedUser userCacheItem)
        {
            _memoryCache.Add(DecryptedUserCacheKey, userCacheItem, GetCacheItemPolicy());
        }

        public void Set(List<DecryptedUser> userCacheItems)
        {
            _memoryCache.Set(DecryptedUserCacheKey, userCacheItems, GetCacheItemPolicy());
        }

        private static CacheItemPolicy GetCacheItemPolicy()
        {
            return new CacheItemPolicy { AbsoluteExpiration = DateTime.Now.AddDays(100) };
        }
    }
}
