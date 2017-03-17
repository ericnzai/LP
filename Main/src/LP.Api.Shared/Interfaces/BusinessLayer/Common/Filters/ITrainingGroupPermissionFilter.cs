using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LP.EntityModels;
using LP.ServiceHost.DataContracts.Enums;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Common.Filters
{
    public interface ITrainingGroupPermissionFilter
    {
        IQueryable<Group> GetAvailableByPermissionsOf<T>(IQueryable<Group> groups, IEnumerable<int> userRoleIds);
        Task<IQueryable<Group>> GetAvailableByStatusOfAsync<T>(IQueryable<Group> groups, IEnumerable<int> userRoleIds, AccessType accessType);
    }
}
