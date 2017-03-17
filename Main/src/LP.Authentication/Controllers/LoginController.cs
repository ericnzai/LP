using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LP.Api.Shared.Interfaces.BusinessLayer.Authentication;
using LP.ServiceHost.DataContracts.Enums;
using LP.ServiceHost.DataContracts.Request.Authentication;
using LP.ServiceHost.DataContracts.Response.Authentication;

namespace LP.Authentication.Controllers
{
    [RoutePrefix("api/authentication/login")]
    public class LoginController : BaseApiController
    {
        public LoginController(IAskAuthenticationApiBusiness askAuthenticationApiBusiness)
            : base(askAuthenticationApiBusiness)
        {
        }

        [Route("")]
        [HttpPost]
        [ResponseType(typeof(LoginResponseContract))]
        public async Task<IHttpActionResult> Post(LoginRequestContract loginRequestContract)
        {
            if (string.IsNullOrEmpty(loginRequestContract.UserName) ||
                string.IsNullOrEmpty(loginRequestContract.Password))
            {
                return NotFound();
            }

            var httpResponseStatus =
                await AskAuthenticationApiBusiness.UserCommands.AuthenticateUserAsync(loginRequestContract.UserName,
                    loginRequestContract.Password);

            switch (httpResponseStatus)
            {
                case HttpResponseStatus.Unauthorised:
                    return Unauthorized();
                case HttpResponseStatus.NotFound:
                    return NotFound();
            }

            var accessTokenModel = await GetAccessToken(loginRequestContract.UserName, loginRequestContract.Password);

            var loginResponseContract = new LoginResponseContract
            {
                AccessToken = accessTokenModel.AccessToken,
                TokenExpiry = accessTokenModel.TokenExpiry,
                HttpResponseStatus = HttpResponseStatus.Success
            };

            return Ok(loginResponseContract);
        }
    }
}
