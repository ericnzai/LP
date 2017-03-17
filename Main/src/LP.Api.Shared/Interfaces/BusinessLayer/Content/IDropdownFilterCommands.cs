using System.Threading.Tasks;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Content
{
    public interface IDropdownFilterCommands
    {
        Task<DashboardFilterDropdownResponseContract> Region();
        Task<DashboardFilterDropdownResponseContract> Country();
        Task<DashboardFilterDropdownResponseContract> Country(int regionId);
        Task<DashboardFilterDropdownResponseContract> JobRole();
        Task<DashboardFilterDropdownResponseContract> Trainer(int countryId);
    }
}
