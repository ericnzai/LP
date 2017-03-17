using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LP.Api.Shared.Interfaces.BusinessLayer.Content;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.Content.Controllers
{
    [RoutePrefix("api/content/glossary-item")]
    public class GlossaryItemController : BaseApiController
    {
        public GlossaryItemController(IAskContentApiBusiness askContentApiBusiness)
            : base(askContentApiBusiness)
        {
        }

        [Route("")]
        [HttpGet]
        [Authorize]
        [ResponseType(typeof(GlossaryItemsResponseContract))]
        public async Task<IHttpActionResult> Get()
        {
            var glossaryItemsResponseContract = await AskContentApiBusiness.GlossaryCommands.GetAllGlossaryItems(GetCultureFromRequestHeader);

            return Ok(glossaryItemsResponseContract);
        }
    }
}
