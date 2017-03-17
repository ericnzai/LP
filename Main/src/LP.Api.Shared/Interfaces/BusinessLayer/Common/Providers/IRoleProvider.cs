using LP.EntityModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Common.Providers
{
    public interface IRoleProvider
    {
        Task<bool> IsUserInAnyGivenRoleAsync(IEnumerable<string> roleNames, IEnumerable<int> userRoleIds);
        Task<IQueryable<int>> GetAllCultureRoleIds();
        Task<IQueryable<Role>> GetAllCultureRoles();
        Task<IQueryable<UserRole>> GetAllUserCultureRolesAsync();
        Task<IQueryable<Role>> GetUserJobFunctionRolesAsync();
    }
}
