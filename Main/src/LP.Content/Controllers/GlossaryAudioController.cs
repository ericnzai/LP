using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LP.Api.Shared.Interfaces.BusinessLayer.Content;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.Content.Controllers
{
     [RoutePrefix("api/content/glossary-audio")]
    public class GlossaryAudioController : BaseApiController
    {
        public GlossaryAudioController(IAskContentApiBusiness askContentApiBusiness) : base(askContentApiBusiness)
        {
        }

        [Route("{glossaryItemId}")]
        [HttpGet]
        [Authorize]
        [ResponseType(typeof (GlossaryAudioResponseContract))]
        public async Task<IHttpActionResult> Get(int glossaryItemId)
        {
            var glossaryAudioResponseContract = await AskContentApiBusiness.GlossaryCommands.GetGlossaryAudio(glossaryItemId);

            if (glossaryAudioResponseContract == null || !glossaryAudioResponseContract.IsEnabled)
            {
                return NotFound();
            }
            return Ok(glossaryAudioResponseContract);
        }
    }
}