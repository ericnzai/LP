using System.Collections.Generic;
using LP.ServiceHost.DataContracts.Common.Exams.Dashboards;
using LP.ServiceHost.DataContracts.Response.Exams;
using System.Threading.Tasks;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Exams
{
    public interface IOverviewCountryProgressCommands
    {
        Task<OverviewCountryProgressResponseContract> GetOverviewCountryProgressResponseContract();

        Task<OverviewCountryProgressResponseContract> GetOverviewCountryProgressFilteredByJobFunction(
            string jobRoles);
        Task<OverviewCountryProgressResponseContract> GetOverviewCountryProgressForRegionResponseContract(int regionId);

        Task<OverviewCountryProgressResponseContract> GetOverviewCountryProgressForRegionFilteredResponseContract(
            string jobRoles, int regionId);
        Task<GroupTypeHeadersResponseContract> GetGroupTypeTableHeader();
    }
}
