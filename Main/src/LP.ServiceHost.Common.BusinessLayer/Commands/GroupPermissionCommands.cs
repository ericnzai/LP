using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using LP.Api.Shared.Interfaces.BusinessLayer.Common;
using LP.Api.Shared.Interfaces.Data;
using LP.EntityModels;
using LP.Model.Authentication;


namespace LP.ServiceHost.Common.BusinessLayer.Commands
{
    public class GroupPermissionCommands : IGroupPermissionCommands
    {
        private readonly IBaseCommands _baseCommands;

        public GroupPermissionCommands(IBaseCommands baseCommands)
        {
            _baseCommands = baseCommands;
        }

        public async Task<IEnumerable<Group>> GroupsWithPermissionsForRoles(UserDetails userDetails)
        {
            var groups = await GroupsWithPermissionsForRolesAsQueryable(userDetails.RoleIds, userDetails.AvailableStatuses);

            return groups;
        }

        public async Task<IEnumerable<Group>> GroupsWithPermissionsForRoles(IEnumerable<int> roleIds, IEnumerable<int> availableStatuses)
        {
            var groups = await GroupsWithPermissionsForRolesAsQueryable(roleIds, availableStatuses);

            return groups;
        }

        public async Task<IEnumerable<Group>> GroupsWithPermissionsForRolesForTrainingArea(IEnumerable<int> roleIds, int trainingAreaId, IEnumerable<int> availableStatuses)
        {
            var groups = await GroupsWithPermissionsForRolesAsQueryable(roleIds, availableStatuses);

            return await groups.Where(r => r.TrainingAreaID == trainingAreaId).ToListAsync();
        }

        private async Task<IQueryable<Group>> GroupsWithPermissionsForRolesAsQueryable(IEnumerable<int> roleIds, IEnumerable<int> availableStatuses)
        {
            var groupPermissions = await _baseCommands.GetWithIncludesAsync<GroupPermission>(x => x.ltl_Groups);

            return groupPermissions.Where(r => roleIds.Contains(r.RoleID) &&
             (r.ltl_Groups.StatusBankID.HasValue && availableStatuses.Contains(r.ltl_Groups.StatusBankID.Value))).Select(g => g.ltl_Groups);
        }
    }
}
