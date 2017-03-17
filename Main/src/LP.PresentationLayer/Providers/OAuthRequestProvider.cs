using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;

namespace LP.PresentationLayer.Providers
{
    public class OAuthRequestProvider : OAuthAuthorizationServerProvider
    {
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
    }

}