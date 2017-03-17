using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using LP.Api.Shared.Interfaces.BusinessLayer.Authentication;
using LP.Api.Shared.Interfaces.BusinessLayer.Common;
using LP.Api.Shared.Interfaces.Core.Encryption;
using LP.Api.Shared.Interfaces.Data;
using LP.Model.Authentication;
using LP.ServiceHost.DataContracts.Enums;
using LP.ServiceHost.DataContracts.Response.Authentication;
using LP.EntityModels;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams.Filters;

namespace LP.Authentication.BusinessLayer.Commands
{
    public class UserCommands : IUserCommands
    {
        private readonly IBaseCommands _baseCommands;
        private readonly IEncryptionHandler _encryptionHandler;
        private readonly ICacheCommands _cacheCommands;
        private readonly IRoleCommands _roleCommands;
        private readonly IUserRoleCommands _userRoleCommands;
        private readonly IFilterAllowedUser _allowedUserFilter;

        public UserCommands(IBaseCommands baseCommands, IEncryptionHandler encryptionHandler, ICacheCommands cacheCommands, IRoleCommands roleCommands, IUserRoleCommands userRoleCommands, IFilterAllowedUser allowedUserFilter)
        {
            _baseCommands = baseCommands;
            _encryptionHandler = encryptionHandler;
            _cacheCommands = cacheCommands;
            _roleCommands = roleCommands;
            _userRoleCommands = userRoleCommands;
            _allowedUserFilter = allowedUserFilter;
        }

        public async Task<HttpResponseStatus> AuthenticateUserAsync(string userName, string password)
        {
            var decodedUserName = HttpUtility.UrlDecode(userName);

            var decryptedUser = _cacheCommands.GetDecryptedUser(decodedUserName);

            var allUsers = await _baseCommands.GetAllAsync<User>();

            if (decryptedUser == null)
            {
                decryptedUser = await HandleNullDecryptedUser(allUsers, decodedUserName);

                if (decryptedUser == null)
                {
                    return HttpResponseStatus.NotFound;
                }

            }

            var user = allUsers.FirstOrDefault(a => a.UserID == decryptedUser.UserId);

            if (user == null)
            {
                _cacheCommands.RemoveDecryptedUserFromCache(decryptedUser.UserId);
                return HttpResponseStatus.NotFound;
            }

            var decryptedUserPassword = _encryptionHandler.DecryptString(user.Password);
            var decryptedInputPassword = _encryptionHandler.DecryptString(password);

            if (decryptedUserPassword == decryptedInputPassword)
            {
                return HttpResponseStatus.Success;
            }

            return HttpResponseStatus.Unauthorised;
        }

        private async Task<DecryptedUser> HandleNullDecryptedUser(IQueryable<User> allUsers, string userName)
        {
            var userNotExistingInDecryptedUsers = await allUsers.FirstOrDefaultAsync(a => a.UserName == userName);

            if (userNotExistingInDecryptedUsers != null)
            {
                return AddUserToCache(userNotExistingInDecryptedUsers);
            }

            var encryptedUserName = _encryptionHandler.EncryptString(userName);

            var userNotExistingInDecryptedUsersWithEncryptedUserName = allUsers.FirstOrDefault(a => a.UserName == encryptedUserName);

            if (userNotExistingInDecryptedUsersWithEncryptedUserName == null)
            {
                return null;
            }

            return AddUserToCache(userNotExistingInDecryptedUsersWithEncryptedUserName);
        }

        private DecryptedUser AddUserToCache(User user)
        {
            return AddUserToCache(user.UserName, user.UserID);
        }

        private DecryptedUser AddUserToCache(string encryptedUserName, int userId)
        {
            var decryptedUser = new DecryptedUser
            {
                UserId = userId,
                DecryptedUserName = _encryptionHandler.DecryptString(encryptedUserName)
            };

            _cacheCommands.AddDecryptedUserToCache(decryptedUser);

            return decryptedUser;
        }

        public async Task<UserDetails> GetUserDetailsAsync(string username)
        {
            var decryptedUser = _cacheCommands.GetDecryptedUser(username);

            if (decryptedUser == null)
            {
                var allUsers = await _baseCommands.GetAllAsync<User>();

                decryptedUser = await HandleNullDecryptedUser(allUsers, username);
            }

            var roles = await _userRoleCommands.GetRolesForUserAsync(decryptedUser.UserId);

            var roleIds = roles.Select(r => r.RoleID).ToList();

            var cultureRoleIds = roles.Where(r=>r.askCore_Roles.RoleGroupID == (int)RoleGroup.CultureRoles).Select(r => r.RoleID).ToList();

            var isAdmin = _userRoleCommands.IsUserAdmin(roles);

            var isTranslator = _userRoleCommands.IsUserTranslator(roles);

            var availableStatuses = _userRoleCommands.GetViewableStatusesForUser(isAdmin, isTranslator);

            var userDetails = new UserDetails
            {
                RoleIds = roleIds,
                CultureRoleIds = cultureRoleIds,
                UserId = decryptedUser.UserId,
                UserName = username,
                IsAdmin = isAdmin,
                IsTranslator = isTranslator,
                AvailableStatuses = availableStatuses
            };

            return userDetails;
        }

        public async Task<UserInformationResponseContract> GetUserInformationAsync(UserDetails userDetails)
        {
            var user = await _baseCommands.GetByIdAsync<User>(userDetails.UserId);

            var displayName = _encryptionHandler.DecryptString(user.DisplayName);

            var userCountryName = string.Empty;

            if (user.CountryID.HasValue)
            {
                var userCountry = await _baseCommands.GetByIdAsync<Country>(user.CountryID.Value);
                userCountryName = userCountry.CountryName;
            }

            var fieldOfEmployment = await _roleCommands.GetFieldOfEmployment(userDetails.RoleIds);

            var userInformationResponseContract = new UserInformationResponseContract
            {
                UserName = userDetails.UserName,
                UserId = userDetails.UserId,
                DisplayName = displayName,
                FieldOfEmployment = fieldOfEmployment,
                UserCountry = userCountryName
            };

            return userInformationResponseContract;
        }

        public string GetUserNameByIdAsync(int userId)
        {
           var decryptedUser = _cacheCommands.GetDecryptedUser(userId);

           return decryptedUser.DecryptedDisplayName;

        }

        public List<DecryptedUser> GetDecryptedUsers(IEnumerable<int> userIds)
        {
            var decryptedUsers = new List<DecryptedUser>();

            if (userIds == null) return decryptedUsers;

            decryptedUsers.AddRange(userIds.Select(userId => _cacheCommands.GetDecryptedUser(userId)));

            return decryptedUsers; 
        }

        public async Task<int> GetCertificationOverviewUserCountTotal()
        {
            return await _allowedUserFilter.GetUserCountTotal();
        }

        public async Task<int> GetCertificationOverviewUserCountTotalByTrainer(int trainerId)
        {
            return await _allowedUserFilter.GetUserCountByTrainer(trainerId);
        }

        public async Task<int> GetCertificationOverviewUserCountByRegion(int regionId)
        {
            return await _allowedUserFilter.GetUserCountByRegion(regionId);
        }

        public async Task<int> GetUserCountFiltered(int regionId = 0, int countryId = 0, int jobRole = 0, int trainerId = 0)
        {
            var users = await _allowedUserFilter.GetAllLiveUsersNotHiddenFromReports();

            if (regionId > 0)
            {
                users = users.Where(region => region.askCore_Countries.RegionId == regionId);
            }

            if (countryId > 0)
            {
                users = users.Where(c => c.CountryID == countryId);
            }

            if (jobRole > 0)
            {
                users = users.Where(ur => ur.askCore_UsersRoles.Select(a => a.RoleID).Contains(jobRole));
            }

            if (trainerId > 0)
            {
                users = users.Where(u => u.ParentID == trainerId);
            }

            return await users.CountAsync();
        }
    }
}
