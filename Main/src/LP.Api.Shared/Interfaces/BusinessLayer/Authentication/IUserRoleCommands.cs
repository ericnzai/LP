using System.Collections.Generic;
using System.Threading.Tasks;
using LP.EntityModels;
using LP.Model.Authentication;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Authentication
{
    public interface IUserRoleCommands
    {
        Task<List<UserRole>> GetRolesForUserAsync(int userId);
        bool IsUserAdmin(IEnumerable<UserRole> userRoles);
        bool IsUserTranslator(IEnumerable<UserRole> userRoles);
        List<int> GetViewableStatusesForUser(UserDetails userDetails);
        List<int> GetViewableStatusesForUser(bool isAdmin, bool isTranslator);
        Task<IEnumerable<Role>> GetCultureRolesForUserAsync(int userId);
        Task<List<UserRole>> GetCultureRolesForUsers(List<int> userIds);

    }
}
