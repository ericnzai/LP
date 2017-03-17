using System;
using System.Configuration;
using Microsoft.Owin.Security.Jwt;

namespace LP.Api.Shared.Providers
{
    public class JwtOptions : JwtBearerAuthenticationOptions
    {
        public JwtOptions()
        {
            var issuer = ConfigurationManager.AppSettings["BaseUrl"];
            var audience = ConfigurationManager.AppSettings["as:AudienceSecret"];
            var key = Convert.FromBase64String(audience);

            AllowedAudiences = new[] {audience};
            IssuerSecurityTokenProviders = new[]
            {
                new SymmetricKeyIssuerSecurityTokenProvider(issuer, key)
            };
        }
    }
}