using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams;
using LP.ServiceHost.DataContracts.Response.Exams;
using LP.Api.Shared.Interfaces.Data;
using LP.EntityModels;
using LP.Model.Authentication;

namespace LP.Exams.BusinessLayer.Commands
{
    public class OverviewReportCommand : IOverviewReportCommand
    {
        private readonly IBaseCommands _baseCommands;
        public OverviewReportCommand(IBaseCommands baseCommands)
        {
            _baseCommands = baseCommands;
        }

        public async Task<ReportRolesResponseContract> GetAllReportRolesResponseContractsForOverviewDropdown(string reportRoleGroup, UserDetails userDetails)
        {
            
            var reportRoles =
               await _baseCommands.GetConditionalAsync<Role>(r => r.askCore_RoleGroup.RoleGroupName == reportRoleGroup);

            var sectionRoles =
                reportRoles.Where(
                    r => r.RoleName.StartsWith("Report_OverviewReport_"))// && userDetails.CultureRoleIds.Contains(r.RoleID))
                    .ToList();

            var result = new ReportRolesResponseContract();

            result.ReportRoles.AddRange(sectionRoles.ToList().Select(r => new ServiceHost.DataContracts.Common.Exams.Role()
            {
                Id = r.RoleID,
                RoleName = r.Description
            }));
            return result;
        }
    }
}
