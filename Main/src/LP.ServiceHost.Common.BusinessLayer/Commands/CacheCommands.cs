using System.Collections.Generic;
using System.Linq;
using LP.Api.Shared.Interfaces.BusinessLayer.Common;
using LP.Api.Shared.Interfaces.Core.Caching;
using LP.Api.Shared.Interfaces.Core.Encryption;
using LP.Api.Shared.Interfaces.Data;
using LP.EntityModels;
using LP.Model.Authentication;

namespace LP.ServiceHost.Common.BusinessLayer.Commands
{
    public class CacheCommands : ICacheCommands
    {
        private readonly IBaseCommands _baseCommands;
        private readonly IMemoryCacheWrapper<DecryptedUser> _decryptedUserMemoryCacheWrapper;
        private readonly IEncryptionHandler _encryptionHandler;
        public CacheCommands(IBaseCommands baseCommands, IMemoryCacheWrapper<DecryptedUser> decryptedUserMemoryCacheWrapper, IEncryptionHandler encryptionHandler)
        {
            _baseCommands = baseCommands;
            _decryptedUserMemoryCacheWrapper = decryptedUserMemoryCacheWrapper;
            _encryptionHandler = encryptionHandler;
        }

        public void UpdateDecryptedUserInCache(DecryptedUser decryptedUser)
        {
            var cachedUsers = GetDecryptedUserCacheAndRefreshCacheIfNeeded();

            if (cachedUsers.FirstOrDefault(a => a.UserId == decryptedUser.UserId) != null) return;

            cachedUsers.Add(new DecryptedUser { UserId = decryptedUser.UserId, DecryptedUserName = decryptedUser.DecryptedUserName });
            
            _decryptedUserMemoryCacheWrapper.Set(cachedUsers);
        }

        public List<DecryptedUser> GetCachedDecryptedUsers()
        {
            return GetDecryptedUserCacheAndRefreshCacheIfNeeded();
        }

        private List<DecryptedUser> GetDecryptedUserCacheAndRefreshCacheIfNeeded()
        {
            var decryptedUserCache = _decryptedUserMemoryCacheWrapper.Get();

            if (decryptedUserCache == null || !decryptedUserCache.Any()) decryptedUserCache = PopulateDecryptedUsersCache();

            return decryptedUserCache;
        }

        private List<DecryptedUser> PopulateDecryptedUsersCache()
        {
            var users = _baseCommands.GetAll<User>().AsEnumerable();

            var decryptedUsers = users.Select(user => new DecryptedUser { UserId = user.UserID, DecryptedUserName = _encryptionHandler.DecryptString(user.UserName), DecryptedDisplayName = _encryptionHandler.DecryptString(user.DisplayName) }).ToList();

            _decryptedUserMemoryCacheWrapper.Set(decryptedUsers);

            return decryptedUsers;
        }

        public DecryptedUser GetDecryptedUser(string userName)
        {
            var cachedUsers = GetDecryptedUserCacheAndRefreshCacheIfNeeded();

            return cachedUsers.FirstOrDefault(un => un.DecryptedUserName == userName);
        }

        public DecryptedUser GetDecryptedUser(int userId)
        {
            var cachedUsers = GetDecryptedUserCacheAndRefreshCacheIfNeeded();

            return cachedUsers.FirstOrDefault(un => un.UserId == userId);
        }

        public void RemoveDecryptedUserFromCache(int userId)
        {
            var cachedUsers = GetDecryptedUserCacheAndRefreshCacheIfNeeded();

            var userToDelete = cachedUsers.FirstOrDefault(a => a.UserId == userId);

            if (userToDelete == null) return;

            cachedUsers.Remove(userToDelete);
            _decryptedUserMemoryCacheWrapper.Set(cachedUsers);
        }

        public void AddDecryptedUserToCache(DecryptedUser decryptedUser)
        {
            _decryptedUserMemoryCacheWrapper.Add(decryptedUser);
        }
    }
}
