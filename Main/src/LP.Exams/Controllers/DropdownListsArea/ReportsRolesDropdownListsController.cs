using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams;
using LP.ServiceHost.DataContracts.Response.Exams;
using LP.Api.Shared.Interfaces.BusinessLayer.Authentication;
namespace LP.Exams.Controllers.DropdownListsArea
{
    [RoutePrefix("api/exams/reports-roles-dropdownlist")]
    public class ReportsRolesDropdownListsController : BaseApiController
    {
        public ReportsRolesDropdownListsController(IAskExamsApiBusiness askExamsApiBusiness) : base(askExamsApiBusiness)
        {

        }

        [Route("")]
        [HttpGet]
        [Authorize]
        [ResponseType(typeof(ReportRolesResponseContract))]
        public async Task<IHttpActionResult> Get()
        {
            var userDetails = GetAuthenticatedUserDetails();

            var reportRolesResponseContract =
                await
                    AskExamsApiBusiness.OverviewReportCommand.GetAllReportRolesResponseContractsForOverviewDropdown
                        ("Report Roles",userDetails);
            return Ok(reportRolesResponseContract);
        }
    }
}