using System.Web;
using System.Web.Mvc;
using LP.PresentationLayer.Filters;

namespace LP.PresentationLayer
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new JwtAuthorizeAttribute());
        }
    }
}
