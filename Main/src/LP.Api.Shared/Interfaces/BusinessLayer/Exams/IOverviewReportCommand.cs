using LP.ServiceHost.DataContracts.Response.Exams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LP.Model.Authentication;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Exams
{
    public interface IOverviewReportCommand
    {
        Task<ReportRolesResponseContract> GetAllReportRolesResponseContractsForOverviewDropdown(string reportRoleGroup, UserDetails userDetails);
    }
}
