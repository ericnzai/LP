using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LP.Api.Shared.Interfaces.BusinessLayer.Content;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.Content.Controllers
{
    [RoutePrefix("api/content/latest-news")]
    public class LatestNewsController : BaseApiController
    {
        public LatestNewsController(IAskContentApiBusiness askContentApiBusiness) : base(askContentApiBusiness)
        {
        }

        [Route("")]
        [HttpGet]
        [Authorize]
        [ResponseType(typeof(LatestNewsResponseContract))]
        public async Task<IHttpActionResult> Get()
        {
            var latestNewsResponseContract = await AskContentApiBusiness.NewsCommands.GetLatestNewsAsync(CurrentRequestCultureInfo);

            return Ok(latestNewsResponseContract);
        }
    }
}
