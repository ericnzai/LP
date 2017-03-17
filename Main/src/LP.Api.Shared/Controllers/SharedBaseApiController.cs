using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using LP.Api.Shared.Attributes;
using LP.Api.Shared.Providers;
using LP.Model.Authentication;

namespace LP.Api.Shared.Controllers
{
    [AskError]
    public class SharedBaseApiController : ApiController
    {
        protected UserDetails GetAuthenticatedUserDetails()
        {
            if (!User.Identity.IsAuthenticated) return null;

            var claimsIdentity = User.Identity as ClaimsIdentity;

            if (claimsIdentity == null) return null;

            var roleIds = GetIntListFromClaim(claimsIdentity, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role");

            var cultureRoleIds = GetIntListFromClaim(claimsIdentity, "CultureRoleIds");

            var userId = GetIntValueFromClaim(claimsIdentity, "UserId");

            var availableStatuses = GetIntListFromClaim(claimsIdentity, "AvailableStatuses");

            var isAdmin = GetBoolValueFromClaim(claimsIdentity, "IsAdmin");
            
            var isTranslator = GetBoolValueFromClaim(claimsIdentity, "IsTranslator");

            var userDetails = new UserDetails
            {
                RoleIds = roleIds,
                CultureRoleIds = cultureRoleIds,
                UserName = claimsIdentity.Name,
                UserId = userId,
                IsAdmin = isAdmin,
                IsTranslator = isTranslator,
                AvailableStatuses = availableStatuses,
                CurrentCulture = GetCultureFromRequestHeader
            };

            return userDetails;
        }

        private static List<int> GetIntListFromClaim(ClaimsIdentity claimsIdentity, string typeName)
        {
            var claim =
                claimsIdentity.Claims.FirstOrDefault(
                    t => t.Type == typeName);

            var list = new List<int>();

            if (claim == null) return list;

            var listValue = claim.Value;

            if (!string.IsNullOrEmpty(listValue))
            {
                list = listValue.Split(',').Select(int.Parse).ToList();
            }

            return list;
        }

        private static int GetIntValueFromClaim(ClaimsIdentity claimsIdentity, string typeName)
        {
            var userIdClaim =
                 claimsIdentity.Claims.FirstOrDefault(
                     t => t.Type == typeName);

            var userId = 0;

            if (userIdClaim != null)
            {
                int.TryParse(userIdClaim.Value, out userId);
            }

            return userId;
        }

        private static bool GetBoolValueFromClaim(ClaimsIdentity claimsIdentity, string typeName)
        {
            var claim =
                claimsIdentity.Claims.FirstOrDefault(
                    t => t.Type == typeName);

            var boolean = false;

            if (claim != null)
            {
                bool.TryParse(claim.Value, out boolean);
            }

            return boolean;
        }

        protected string GetCultureFromRequestHeader
        {
            get
            {
                if (Request == null || Request.Headers == null) return ConstantProvider.GlobalCulture;

                return Request.Headers.Any(x => x.Key == "x-culture")
                    ? Request.Headers.GetValues("x-culture").First()
                    : ConstantProvider.GlobalCulture;
            }
        }

        public CultureInfo CurrentRequestCultureInfo
        {
            get
            {
                var headers = GetCultureFromRequestHeader;

                try
                {
                    return new CultureInfo(headers);
                }
                catch (Exception)
                {
                    return new CultureInfo(ConstantProvider.GlobalCulture);
                }
            }
        }
    }
}
