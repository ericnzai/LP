using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using  LP.Model.ViewModels.Reports;
using  LP.PresentationLayer.Controllers;
using LP.ServiceHost.DataContracts.Common.Exams;
using LP.ServiceHost.DataContracts.Response.Exams;
using LP.Api.Shared.Providers;

namespace LP.PresentationLayer.Areas.Eylea.Controllers
{
    public class ReportController : BaseController
    {
        // GET: Eylea/Report
        public async Task<PartialViewResult> OverviewRolesDropdown()
        {
            var reportRolesResponseContract =
                await GetResponseFromService<ReportRolesResponseContract>(UriProvider.Exams.OverviewReportRoles);

            var roleItems = new List<Role>() ;

            roleItems = reportRolesResponseContract.ReportRoles.ToList();

            var reportsRolesViewModel = new ReportsRolesViewModel()
            {
                ReportRoles = roleItems
            };

            return PartialView("~/Areas/Eylea/Views/Report/_ReportRolesDropdown.cshtml", reportsRolesViewModel);
        }
    }
}