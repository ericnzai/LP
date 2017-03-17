using System.Collections.Generic;
using System.Threading.Tasks;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Authentication
{
    public interface IRoleCommands
    {
        Task<string> GetFieldOfEmployment(ICollection<int> userRoles);
        Task<Dictionary<string, string>> GetRolesAsync(IEnumerable<int> roleIds);
    }
}
