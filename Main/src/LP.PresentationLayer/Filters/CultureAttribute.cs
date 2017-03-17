using System.Web.Mvc;

namespace LP.PresentationLayer.Filters
{
    public class CultureAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var headers = filterContext.RequestContext.HttpContext.Request.Headers;

            var cultureHeaderValue = headers["x-culture"];

            filterContext.RouteData.Values.Add("Culture", cultureHeaderValue ?? "en");
        }
    }
}