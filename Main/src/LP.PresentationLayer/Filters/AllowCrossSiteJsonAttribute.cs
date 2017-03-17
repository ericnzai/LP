using System.Collections.Generic;
using System.Web.Mvc;

namespace LP.PresentationLayer.Filters
{
    public class AllowCrossSiteJsonAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var domains = new List<string> { "https://preview.eyleatraining.com", "https://eyleatraining.com", "https://www.eyleatraining.com", "staging.asandk.com", "eyleatraining.com", "www.eyleatraining.com", "preview.eyleatraining.com", "previewapi.eyleatraining.com", "api.eyleatraining.com" };

            if (domains.Contains(GetHost(filterContext)))
            {
                filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "*");
            }

            base.OnActionExecuting(filterContext);
        }

        private string GetHost(ActionExecutingContext filterContext)
        {
            if (filterContext != null && 
                filterContext.RequestContext != null && 
                filterContext.RequestContext.HttpContext != null && 
                filterContext.RequestContext.HttpContext.Request != null && 
                filterContext.RequestContext.HttpContext.Request.UrlReferrer != null)
            {
                return filterContext.RequestContext.HttpContext.Request.UrlReferrer.Host;
            }

            return string.Empty;
        }
    }
}