using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using LP.Api.Shared.Interfaces.BusinessLayer.Authentication;
using LP.Model.Authentication;
using LP.ServiceHost.DataContracts.Enums;
using Microsoft.Owin.Security.OAuth;

namespace LP.Api.Shared.Providers
{
    public class OAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly IUserCommands _userCommands;

        public OAuthProvider(IUserCommands userCommands)
        {
            _userCommands = userCommands;
        }

        public async override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity("otc");
            
            var username = HttpUtility.UrlDecode(context.OwinContext.Get<string>("otc:username"));
            
            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", username));

            var userDetails = await _userCommands.GetUserDetailsAsync(username);

            identity.AddClaim(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", userDetails.RoleIdsString));
            identity.AddClaim(new Claim("CultureRoleIds", userDetails.CultureRoleIdsString));
            identity.AddClaim(new Claim("UserId", userDetails.UserId.ToString(CultureInfo.InvariantCulture)));
            identity.AddClaim(new Claim("IsAdmin", userDetails.IsAdmin.ToString(CultureInfo.InvariantCulture)));
            identity.AddClaim(new Claim("IsTranslator", userDetails.IsTranslator.ToString(CultureInfo.InvariantCulture)));
            identity.AddClaim(new Claim("AvailableStatuses", userDetails.AvailableStatusesString));
            
            context.Validated(identity);
        }

        public override Task MatchEndpoint(OAuthMatchEndpointContext context)
        {
            if (context.OwinContext.Request.Method != "OPTIONS" || !context.IsTokenEndpoint) return base.MatchEndpoint(context);
            
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Methods", new[] { "POST", "GET", "PUT", "DELETE", "OPTIONS" });
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "accept", "authorization", "content-type", "x-culture" });
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            context.OwinContext.Response.StatusCode = 200;
            context.RequestCompleted();

            return Task.FromResult(0);
        }

        public async override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            try
            {
                var username = context.Parameters["username"];
                var password = context.Parameters["password"];

                var isuserAuthenticated =
                    await _userCommands.AuthenticateUserAsync(username, password);

                switch (isuserAuthenticated)
                {
                    case HttpResponseStatus.Success:
                        context.OwinContext.Set("otc:username", username);
                        context.Validated();
                        break;
                    case HttpResponseStatus.Unauthorised:
                        context.SetError("Invalid credentials");
                        context.Rejected();
                        break;
                    case HttpResponseStatus.NotFound:
                        context.SetError("Not Found");
                        context.Rejected();
                        break;
                }
            }
            catch
            {
                context.SetError("Server error");
                context.Rejected();
            }
            
        }
    }
}