using System.Linq;
using LP.Api.Shared.Interfaces.BusinessLayer.Authentication;
using LP.Api.Shared.Interfaces.BusinessLayer.Common;
using System.Threading.Tasks;
using LP.Model.Authentication;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.Content.BusinessLayer.Commands
{
    public class CultureMenuCommands : ICultureMenuCommands
    {
        private readonly IRoleCommands _roleCommand;

        public CultureMenuCommands(IRoleCommands roleCommand)
        {
            _roleCommand = roleCommand;
        }

        public async Task<CompleteCultureMenuResponseContract> GetAvailableCultures(UserDetails userDetails)
        {
            var availableCulturesIds = userDetails.CultureRoleIds;
            var availableCultures = await _roleCommand.GetRolesAsync(availableCulturesIds);

            var result = new CompleteCultureMenuResponseContract {AvailableCultures = availableCultures};

            return result;
        }

        public async Task<CompleteCultureMenuResponseContract> GetAvailableCulturesExceptEnglishGlobal(UserDetails userDetails)
        {
            var availableCulturesIds = userDetails.CultureRoleIds;
            var availableCultures = await _roleCommand.GetRolesAsync(availableCulturesIds);
            var availableCulturesExceptEnglishGlobal = availableCultures.Where(c => c.Key != "en").ToDictionary(role=>role.Key, role=>role.Value);

            var result = new CompleteCultureMenuResponseContract { AvailableCultures = availableCulturesExceptEnglishGlobal };

            return result;
        }
    }
}
