using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace LP.Api.Shared.Interfaces.Api
{
    public interface ICultureProvider
    {
        string DefaultCultureString { get; }
        CultureInfo GetCultureInfoWithDefault(string culture);
        Task<string> GetCultureDisplayName(string culture);
        Task<Dictionary<string, string>> GetCultureRoles();
    }
}