using System.Web.Mvc;

namespace LP.PresentationLayer.Areas.Xarelto
{
    public class XareltoAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Xarelto";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Xarelto_default",
                "Xarelto/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}