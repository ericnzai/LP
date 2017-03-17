using System.Collections.Generic;
using LP.Model.Authentication;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Common
{
    public interface ICacheCommands
    {
        void UpdateDecryptedUserInCache(DecryptedUser decryptedUser);
        List<DecryptedUser> GetCachedDecryptedUsers();
        DecryptedUser GetDecryptedUser(string userName);
        DecryptedUser GetDecryptedUser(int userId);
        void RemoveDecryptedUserFromCache(int userId);
        void AddDecryptedUserToCache(DecryptedUser decryptedUser);
    }
}
