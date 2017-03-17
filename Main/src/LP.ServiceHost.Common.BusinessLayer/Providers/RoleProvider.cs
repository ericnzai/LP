using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LP.Api.Shared.Interfaces.BusinessLayer.Common.Providers;
using LP.Api.Shared.Interfaces.Data;
using LP.EntityModels;
using LP.ServiceHost.DataContracts.Enums;

namespace LP.ServiceHost.Common.BusinessLayer.Providers
{
    public class RoleProvider : IRoleProvider
    {
        private readonly IBaseCommands _baseCommands;

        public RoleProvider(IBaseCommands baseCommands)
        {
            _baseCommands = baseCommands;
        }

        private readonly static List<string> JobFunctions = new List<string> { "MSL", "Medical", "Commercial", "Sales", "Other" }; 

        public async Task<bool> IsUserInAnyGivenRoleAsync(IEnumerable<string> roleNames, IEnumerable<int> userRoleIds)
        {
            var roles = await _baseCommands.GetAllAsync<Role>();

            return roles.Any(x => roleNames.Contains(x.RoleName) && userRoleIds.Contains(x.RoleID));
        }

        public async Task<IQueryable<Role>> GetAllCultureRoles()
        {
            var roles =
                await
                    _baseCommands.GetConditionalAsync<Role>(
                        r => r.askCore_RoleGroup.RoleGroupName == RoleGroup.CultureRoles.ToString());

            return roles;
        }

        public async Task<IQueryable<int>> GetAllCultureRoleIds()
        {
            var roles = await GetAllCultureRoles();

            return roles.Select(x => x.RoleID);
        }

        public async Task<IQueryable<UserRole>> GetAllUserCultureRolesAsync()
        {
            var cultureRoles = await GetAllCultureRoles();

            var cultureRoleIds = cultureRoles.Select(a => a.RoleID);

            return  await _baseCommands.GetConditionalWithIncludesAsync<UserRole>(
                               x => cultureRoleIds.Contains(x.RoleID), inc => inc.askCore_Roles, inc => inc.askCore_Users);
        }

        public async Task<IQueryable<Role>> GetUserJobFunctionRolesAsync()
        {
            var roles = await _baseCommands.GetConditionalAsync<Role>(x => JobFunctions.Contains(x.RoleName));

            return roles;
        }
    }
}
