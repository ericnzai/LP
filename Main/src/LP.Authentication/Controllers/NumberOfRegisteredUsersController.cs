using System.Threading.Tasks;
using System.Web.Http;
using LP.Api.Shared.Interfaces.BusinessLayer.Authentication;

namespace LP.Authentication.Controllers
{
    [RoutePrefix("api/authentication/number-of-registered-users")]
    public class NumberOfRegisteredUsersController : BaseApiController
    {
        public NumberOfRegisteredUsersController(IAskAuthenticationApiBusiness askAuthenticationApiBusiness)
            : base(askAuthenticationApiBusiness)
        {
        }

        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var count = await AskAuthenticationApiBusiness.UserCommands.GetCertificationOverviewUserCountTotal();
            return Ok(count);
        }

        [Route("trainer")]
        public async Task<IHttpActionResult> GetForTrainer()
        {

            var userDetails = GetAuthenticatedUserDetails();
            var trainerId = userDetails.UserId;


            var count = await AskAuthenticationApiBusiness.UserCommands.GetCertificationOverviewUserCountTotalByTrainer(trainerId);
            return Ok(count);
        }

        [Route("region/{regionId}")]
        public async Task<IHttpActionResult> GetForRegion(int regionId)
        {
            var count = await AskAuthenticationApiBusiness.UserCommands.GetCertificationOverviewUserCountByRegion(regionId);
            return Ok(count);
        }

        [Route("country/{countryId}")]
        public async Task<IHttpActionResult> GetForCountry(int countryId)
        {
            var userCount = await AskAuthenticationApiBusiness.UserCommands.GetUserCountFiltered(countryId: countryId);

            return Ok(userCount);
        }

        [Route("filtered/{regionId}/{countryId}/{jobRoleId}")]
        public async Task<IHttpActionResult> GetFiltered(int regionId, int countryId, int jobRoleId)
        {
            var userCount = await AskAuthenticationApiBusiness.UserCommands.GetUserCountFiltered(regionId, countryId,
                jobRoleId);

            return Ok(userCount);
        }

        [Route("filteredForCountry/{countryId}/{jobRoleId}/{trainerId}")]
        public async Task<IHttpActionResult> GetFilteredForCountry(int countryId, int jobRoleId, int trainerId)
        {
            var userCount = await AskAuthenticationApiBusiness.UserCommands.GetUserCountFiltered(0, countryId, jobRoleId, trainerId);

            return Ok(userCount);
        }
    }
}
