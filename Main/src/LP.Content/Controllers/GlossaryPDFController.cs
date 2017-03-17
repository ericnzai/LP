using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LP.Api.Shared.Interfaces.BusinessLayer.Content;
using LP.ServiceHost.DataContracts.Request.Content;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.Content.Controllers
{
    [RoutePrefix("api/content")]
    public class GlossaryPdfController : BaseApiController
    {
        public GlossaryPdfController(IAskContentApiBusiness askContentApiBusiness)
            : base(askContentApiBusiness)
        {
        }

        [Route("glossary-pdf")]
        [HttpPost]
        [Authorize]
        [ResponseType(typeof(GlossaryPDFResponseContract))]
        public async Task<IHttpActionResult> Post(GlossaryPdfRequestContract glossaryPdfRequestContract)
        {
            var glossaryPdfResponseContract = await AskContentApiBusiness.GlossaryPdfCommands.GetWholeGlossaryPdfContent(GetCultureFromRequestHeader, glossaryPdfRequestContract.TranslatedItems);

            return Ok(glossaryPdfResponseContract);
        }
            
        [Route("glossary-filtered-pdf")]
        [HttpPost]
        [Authorize]
        [ResponseType(typeof(GlossaryPDFResponseContract))]
        public async Task<IHttpActionResult> Post(GlossaryFilteredPdfRequestContract glossaryPdfRequestContract)
        {
            var glossaryPdfResponseContract = await AskContentApiBusiness.GlossaryPdfCommands.GetFilteredGlossaryPdfContent(GetCultureFromRequestHeader, glossaryPdfRequestContract.TranslatedItems, glossaryPdfRequestContract.Filters, glossaryPdfRequestContract.Sort);

            return Ok(glossaryPdfResponseContract);
        }
    }
}
