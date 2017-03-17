using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using LP.Api.Shared.Interfaces.BusinessLayer.Authentication;
using LP.Api.Shared.Interfaces.Data;
using LP.EntityModels;

namespace LP.Authentication.BusinessLayer.Commands
{
    public class RoleCommands : IRoleCommands
    {
        private readonly IBaseCommands _baseCommands;

        public RoleCommands(IBaseCommands baseCommands)
        {
            _baseCommands = baseCommands;
        }

        public  async Task<string> GetFieldOfEmployment(ICollection<int> userRoles)
        {
            var roles = await _baseCommands.GetAllAsync<Role>();

            var fieldOfEmploymentRoleStrings = new List<string>
            {
                "MSL","Medical","Commercial","Sales","Other"
            };

            var fieldOfEmploymentRoles = roles.Where(r => fieldOfEmploymentRoleStrings.Contains(r.RoleName));

            var mslRole = await fieldOfEmploymentRoles.FirstOrDefaultAsync(x => x.RoleName == "MSL");
            var medicalRole = await fieldOfEmploymentRoles.FirstOrDefaultAsync(x => x.RoleName == "Medical");
            var commercialRole = await fieldOfEmploymentRoles.FirstOrDefaultAsync(x => x.RoleName == "Commercial");
            var salesRole = await fieldOfEmploymentRoles.FirstOrDefaultAsync(x => x.RoleName == "Sales");

            if (mslRole != null && userRoles.Contains(mslRole.RoleID))
            {
                return "MSL";
            }
            if (medicalRole != null && userRoles.Contains(medicalRole.RoleID))
            {
                return "Medical";
            }
            if (commercialRole != null && userRoles.Contains(commercialRole.RoleID))
            {
                return "Commercial";
            }
            if (salesRole != null && userRoles.Contains(salesRole.RoleID))
            {
                return "Sales";
            }

            return "Other";
        }

        public async Task<Dictionary<string, string>> GetRolesAsync(IEnumerable<int> roleIds)
        {
           var roles = await _baseCommands.GetConditionalAsync<Role>(r => roleIds.Contains(r.RoleID));

            return await roles.ToDictionaryAsync(role => role.RoleName, role => role.Description);
        }
    }
}
