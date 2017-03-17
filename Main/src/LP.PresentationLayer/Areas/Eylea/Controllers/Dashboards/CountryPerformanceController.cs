using System.Web.Mvc;
using LP.PresentationLayer.Controllers;

namespace LP.PresentationLayer.Areas.Eylea.Controllers.Dashboards
{
    public class CountryPerformanceController : BaseController
    {
        // GET: Eylea/CountryPerformance
        public ActionResult Index()
        {
            return View();
        }
    }
}