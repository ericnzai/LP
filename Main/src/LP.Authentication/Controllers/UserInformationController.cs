using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LP.Api.Shared.Interfaces.BusinessLayer.Authentication;
using LP.ServiceHost.DataContracts.Response.Authentication;

namespace LP.Authentication.Controllers
{
    [Authorize]
    [RoutePrefix("api/authentication/user-information")]
    public class UserInformationController : BaseApiController
    {
        public UserInformationController(IAskAuthenticationApiBusiness askAuthenticationApiBusiness) : base(askAuthenticationApiBusiness)
        {
        }
        [Route("")]
        [HttpGet]
        [ResponseType(typeof(UserInformationResponseContract))]
        public async Task<IHttpActionResult> Get()
        {
            var userDetails = GetAuthenticatedUserDetails();

            var userInformation = await AskAuthenticationApiBusiness.UserCommands.GetUserInformationAsync(userDetails);

            return Ok(userInformation);
        }
    }
}
