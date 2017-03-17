using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LP.Api.Shared.Interfaces.BusinessLayer.Content;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.Content.Controllers
{
    [RoutePrefix("api/content/dashboard-dropdown")]
    public class DashboardFilterController : BaseApiController
    {
        public DashboardFilterController(IAskContentApiBusiness askContentApiBusiness) : base(askContentApiBusiness)
        {
        }

        [Route("country")]
        [HttpGet]
        [Authorize]
        [ResponseType(typeof (DashboardFilterDropdownResponseContract))]
        public async Task<IHttpActionResult> GetCountry()
        {
            var dashboardFilterDropdownResponseContract = await AskContentApiBusiness.DropdownFilterCommands.Country();

            return Ok(dashboardFilterDropdownResponseContract);
        }

        [Route("country/{regionId}")]
        [HttpGet]
        [Authorize]
        [ResponseType(typeof(DashboardFilterDropdownResponseContract))]
        public async Task<IHttpActionResult> GetCountry(int regionId)
        {
            var dashboardFilterDropdownResponseContract = await AskContentApiBusiness.DropdownFilterCommands.Country(regionId);

            return Ok(dashboardFilterDropdownResponseContract);
        }

        [Route("region")]
        [HttpGet]
        [Authorize]
        [ResponseType(typeof(DashboardFilterDropdownResponseContract))]
        public async Task<IHttpActionResult> GetRegion()
        {
            var dashboardFilterDropdownResponseContract = await AskContentApiBusiness.DropdownFilterCommands.Region();

            return Ok(dashboardFilterDropdownResponseContract);
        }

        [Route("trainer/{countryId}")]
        [HttpGet]
        [Authorize]
        [ResponseType(typeof(DashboardFilterDropdownResponseContract))]
        public async Task<IHttpActionResult> GetTrainer(int countryId)
        {
            var dashboardFilterDropdownResponseContract = await AskContentApiBusiness.DropdownFilterCommands.Trainer(countryId);

            return Ok(dashboardFilterDropdownResponseContract);
        }

        [Route("job-role")]
        [HttpGet]
        [Authorize]
        [ResponseType(typeof(DashboardFilterDropdownResponseContract))]
        public async Task<IHttpActionResult> GetJobRole()
        {
            var dashboardFilterDropdownResponseContract = await AskContentApiBusiness.DropdownFilterCommands.JobRole();

            return Ok(dashboardFilterDropdownResponseContract);
        }
    }
}
