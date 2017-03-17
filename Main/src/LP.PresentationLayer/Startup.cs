using System;
using System.Configuration;
using System.IdentityModel.Tokens;
using LP.Api.Shared.Formatters;
using LP.Api.Shared.Interfaces.BusinessLayer.Authentication;
using LP.Api.Shared.Providers;
using LP.PresentationLayer.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(LP.PresentationLayer.Startup))]

namespace LP.PresentationLayer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
            //ConfigureAuth(app);
            //ConfigureOAuthTokenGeneration(app);
            //ConfigureOAuthTokenConsumption(app);


        }

        //public void ConfigureAuth(IAppBuilder app)
        //{
        //    app.UseCookieAuthentication(new CookieAuthenticationOptions
        //    {
        //        //AuthenticationType = AuthenticationType.ApplicationCookie,
        //        LoginPath = new PathString("/api/authentication/Login"),
        //        CookieSecure = CookieSecureOption.SameAsRequest,
        //        CookieName = "AUTH_TOKEN", 
        //        AuthenticationMode = AuthenticationMode.Active,


        //    });
        //}
        
        private void ConfigureOAuthTokenGeneration(IAppBuilder app)
        {
            var oAuthProvider = new OAuthRequestProvider();

            var oAuthServerOptions = new OAuthAuthorizationServerOptions
            {
                //For Dev enviroment only (on production should be AllowInsecureHttp = false)
                AllowInsecureHttp = true, //  Convert.ToBoolean(ConfigurationManager.AppSettings["AllowInsecureHttp"]),
                TokenEndpointPath = new PathString("/oauth/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = oAuthProvider,
                AccessTokenFormat = new CustomJwtFormat(ConfigurationManager.AppSettings["BackendServiceUri"])
            };

            // OAuth 2.0 Bearer Access Token Generation
            app.UseOAuthAuthorizationServer(oAuthServerOptions);
        }

        private static void ConfigureOAuthTokenConsumption(IAppBuilder app)
        {
            var issuer = ConfigurationManager.AppSettings["BackendServiceUri"];
            var audienceId = ConfigurationManager.AppSettings["as:audienceId"];
            var audienceSecret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["as:AudienceSecret"]);

            var jwtBearerAuthenticationOptions = new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Passive,
                AllowedAudiences = new[] { audienceId },
                IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                {
                    new SymmetricKeyIssuerSecurityTokenProvider(issuer, audienceSecret)
                },
                //TokenHandler = new JwtSecurityTokenHandler()
            };

            app.UseJwtBearerAuthentication(jwtBearerAuthenticationOptions);
        }
    }
}
