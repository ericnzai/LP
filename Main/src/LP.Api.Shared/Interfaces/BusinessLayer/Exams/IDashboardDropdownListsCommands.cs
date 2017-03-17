using System.Threading.Tasks;
using log4net;
using LP.Model.Authentication;
using LP.Model.ViewModels.Dashboards.Country;
using LP.Model.ViewModels.Dashboards.Global;
using LP.Model.ViewModels.Dashboards.Regional;
using LP.ServiceHost.DataContracts.Response.Exams;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Exams
{
    public interface IDashboardDropdownListsCommands
    {
        Task<CountryDropdownListsViewModel> GetCountryDropdownLists(UserDetails userDetails);
        Task<RegionalDropdownListsViewModel> GetRegionalDropdownLists(UserDetails userDetails);
        Task<GlobalDropdownListsViewModel> GetGlobalDropdownLists();
    }
}
