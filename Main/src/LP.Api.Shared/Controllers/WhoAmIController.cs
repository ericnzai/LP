using System.Diagnostics;
using System.Reflection;
using System.Web.Http;

namespace LP.Api.Shared.Controllers
{
    [RoutePrefix("api")]
    public class WhoAmIController : SharedBaseApiController
    {
        [HttpGet]
        [Route("who-am-i")]
        public IHttpActionResult Get()
        {
            var assemblyVersion = string.Empty;

            try
            {
                var assembly = Assembly.GetExecutingAssembly();

                var fileInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

                assemblyVersion = fileInfo.FileVersion;
            }
            catch
            {
                
            }

            return Ok(assemblyVersion);
        }
    }
}
