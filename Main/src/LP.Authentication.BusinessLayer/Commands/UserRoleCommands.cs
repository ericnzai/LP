using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using LP.Api.Shared.Interfaces.BusinessLayer.Authentication;
using LP.Api.Shared.Interfaces.Data;
using LP.EntityModels;
using LP.Model.Authentication;
using LP.ServiceHost.DataContracts.Enums;

namespace LP.Authentication.BusinessLayer.Commands
{
    public class UserRoleCommands : IUserRoleCommands
    {
        private const string AdminRoleName = "Administrator";
        private const string TranslatorRoleName = "Admin_ContentTranslationManagement";

        private readonly IBaseCommands _baseCommands;

        public UserRoleCommands(IBaseCommands baseCommands)
        {
            _baseCommands = baseCommands;
        }

        public async Task<List<UserRole>> GetRolesForUserAsync(int userId)
        {
            var userRoles = await GetRolesForUserAsQueryableAsync(userId);

            return await userRoles.ToListAsync();
        }

        private async Task<IQueryable<UserRole>> GetRolesForUserAsQueryableAsync(int userId)
        {
            var userRoles = await _baseCommands.GetWithIncludesAsync<UserRole>(inc => inc.askCore_Roles);

            return userRoles.Where(u => u.UserID == userId);
        }

        public bool IsUserAdmin(IEnumerable<UserRole> userRoles)
        {
            return userRoles.Select(r => r.askCore_Roles.RoleName).Contains(AdminRoleName);
        }

        public bool IsUserTranslator(IEnumerable<UserRole> userRoles)
        {
            return userRoles.Select(r => r.askCore_Roles.RoleName).Contains(TranslatorRoleName);
        }

        public List<int> GetViewableStatusesForUser(UserDetails userDetails)
        {
            return GetViewableStatusesForUser(userDetails.IsAdmin, userDetails.IsTranslator);
        }

        public List<int> GetViewableStatusesForUser(bool isAdmin, bool isTranslator)
        {
            var allowedStatuses = new List<int> { (int)Status.Live, (int)Status.ComingSoon };

            if (isAdmin || isTranslator)
            {
                allowedStatuses.Add((int)Status.TranslationInProgress);
            }

            return allowedStatuses;
        }

        public async Task<IEnumerable<Role>> GetCultureRolesForUserAsync(int userId)
        {
            var userRoles = await _baseCommands.GetConditionalAsync<UserRole>(x => x.UserID == userId);
            //Debug.WriteLine(userRoles.ToList().Count);
            if (userRoles == null || !userRoles.Any()) return null;

            var userRolesIds = userRoles.Select(u => u.RoleID);
            var cultureRoles = await _baseCommands.GetConditionalWithIncludesAsync<Role>(r => r.askCore_RoleGroup.RoleGroupName == RoleGroup.CultureRoles.ToString(), inc => inc.askCore_RoleGroup);
            
            var userCultureRoles = cultureRoles.Where(c => userRolesIds.Contains(c.RoleID));

            return userCultureRoles;
        }
        
        public async Task<List<UserRole>> GetCultureRolesForUsers(List<int> userIds)
        {
            var userCultureRoles = await _baseCommands.GetConditionalWithIncludesAsync<UserRole>(x => userIds.Contains(x.UserID) && x.askCore_Roles.RoleGroupID == (int)RoleGroup.CultureRoles,
                                                                                        inc => inc.askCore_Roles);

            return await userCultureRoles.ToListAsync();
        }

    }
}
