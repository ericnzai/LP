using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using LP.Api.Shared.Interfaces.Api;
using LP.Api.Shared.Interfaces.Data;
using LP.EntityModels;


namespace LP.Host.Providers
{
    public class CultureProvider : ICultureProvider
    {
        private const string DefaultMainCultureString = "en";
        private const string GlobalLanguageDisplayName = "Global English";

        public string DefaultCultureString { get { return DefaultMainCultureString; } }

        private readonly IBaseCommands _baseCommands;

        public CultureProvider(IBaseCommands baseCommands)
        {
            _baseCommands = baseCommands;
        }


        public CultureInfo GetCultureInfoWithDefault(string culture)
        {
            try
            {
                return CultureInfo.GetCultureInfo(culture);
            }
            catch (Exception)
            {
                return CultureInfo.GetCultureInfo(DefaultMainCultureString);
            }
        }

        public async Task<string> GetCultureDisplayName(string culture)
        {
            if (culture == DefaultMainCultureString) return GlobalLanguageDisplayName;

            try
            {
                var cultureRoles = await GetCultureRoles();

                var role = cultureRoles.FirstOrDefault(r => r.Key == culture);
                return role.Value;
            }
            catch (Exception)
            {
                return GlobalLanguageDisplayName;
            }
        }

        public async Task<Dictionary<string, string>> GetCultureRoles()
        {
            var cultureRoles = await _baseCommands.GetConditionalWithIncludesAsync<Role>(r => r.askCore_RoleGroup.RoleGroupName == "CultureRoles", inc => inc.askCore_RoleGroup);

            var cultureRoleDic = await cultureRoles.ToDictionaryAsync(role => role.RoleName, role => role.Description);

            return cultureRoleDic;
        }
    }
}