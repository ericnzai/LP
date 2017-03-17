using System.Diagnostics;
using System.Reflection;
using System.Web.Mvc;

namespace LP.PresentationLayer.Controllers
{
    public class WhoAmIController : Controller
    {
        public ActionResult Index()
        {
            var assemblyVersion = string.Empty;

            try
            {
                var assembly = Assembly.GetExecutingAssembly();

                var fileInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

                assemblyVersion = fileInfo.FileVersion;
            }
            catch
            { }

            return View("Index", null, assemblyVersion);
        }
    }
}