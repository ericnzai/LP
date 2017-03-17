using System.Collections.Generic;
using System.Threading.Tasks;
using LP.EntityModels;
using LP.Model.Authentication;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Common
{
    public interface IGroupPermissionCommands
    {
        Task<IEnumerable<Group>> GroupsWithPermissionsForRoles(UserDetails userDetails);
        Task<IEnumerable<Group>> GroupsWithPermissionsForRoles(IEnumerable<int> roleIds, IEnumerable<int> availableStatuses);

        Task<IEnumerable<Group>> GroupsWithPermissionsForRolesForTrainingArea(IEnumerable<int> roleIds,
            int trainingAreaId, IEnumerable<int> availableStatuses);
    }
}
