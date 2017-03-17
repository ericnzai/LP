using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams;
using LP.ServiceHost.DataContracts.Response.Exams;

namespace LP.Exams.Controllers.Dashboard
{
    [RoutePrefix("api/exams/dashboard/overview-progress-filtered")]
    public class OverviewProgressFilteredController : BaseApiController
    {
        public OverviewProgressFilteredController(IAskExamsApiBusiness askExamsApiBusiness) : base(askExamsApiBusiness)
        {
        }

        [Authorize]
        [HttpGet]
        [ResponseType(typeof (OverviewGroupTypeProgressResponseContract))]
        [Route("{regionId}/{countryId}/{jobRoleId}")]
        public async Task<IHttpActionResult> Get(int regionId, int countryId, int jobRoleId)
        {
            var overviewGroupProgressResponseContract =
                await
                    AskExamsApiBusiness.OverviewGroupTypeProgressCommands.GetOverviewGroupTypeProgressResponseContract(regionId, countryId, jobRoleId, 0);

            return Ok(overviewGroupProgressResponseContract);
        }

        [Authorize]
        [HttpGet]
        [ResponseType(typeof(OverviewGroupTypeProgressResponseContract))]
        [Route("country/{countryId}/{jobRoleId}/{trainerId}")]
        public async Task<IHttpActionResult> GetForCountry(int countryId, int jobRoleId, int trainerId)
        {
            var overviewGroupProgressResponseContract =
                await
                    AskExamsApiBusiness.OverviewGroupTypeProgressCommands.GetOverviewGroupTypeProgressResponseContract(0, countryId, jobRoleId, trainerId);

            return Ok(overviewGroupProgressResponseContract);
        }
    }
}