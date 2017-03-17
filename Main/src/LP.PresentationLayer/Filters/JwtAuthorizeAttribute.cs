using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Mvc;

namespace LP.PresentationLayer.Filters
{
    public class JwtAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Request.HttpMethod.ToLower() == "options") return true;

            var cookie = httpContext.Request.Cookies["AUTH_TOKEN"];

            if (cookie == null) return false;

            var authHeader = cookie.Value;

            if (string.IsNullOrEmpty(authHeader))
            {
                var controller = ((MvcHandler)httpContext.Handler).RequestContext.RouteData.Values["controller"].ToString().ToLower();

                return AllowedControllers.Contains(controller);
            }

            var authBits = authHeader.Split(' ');
            if (authBits.Length != 2)
            {
                return false;
            }
            if (!authBits[0].ToLowerInvariant().Equals("bearer"))
            {
                return false;
            }

            var base64Secret = ConfigurationManager.AppSettings["as:AudienceSecret"]
                .Replace('_', '/').Replace('-', '+');
            var secret = Convert.FromBase64String(base64Secret);

            try
            {
                var jsonPayload = JWT.JsonWebToken.Decode(authBits[1], secret);
                Console.WriteLine(jsonPayload);
            }
            catch (JWT.SignatureVerificationException)
            {
                Console.WriteLine("Invalid token!");
                return false;
            }


            var routeData = ((MvcHandler) httpContext.Handler).RequestContext.RouteData;

            if (routeData.Values.ContainsKey("AUTH_TOKEN"))
            {
                routeData.Values.Remove("AUTH_TOKEN");
            }

            routeData.Values.Add("AUTH_TOKEN", authBits[1]);

            return true;


        }

        private List<string> AllowedControllers { get { return new List<string> {"whoami"};} } 
    }
}