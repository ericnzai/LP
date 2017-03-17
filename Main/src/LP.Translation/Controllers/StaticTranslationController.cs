using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LP.Api.Shared.Interfaces.BusinessLayer.Translation;
using LP.ServiceHost.DataContracts.Request.Translation;
using LP.ServiceHost.DataContracts.Response.Translation;

namespace LP.Translation.Controllers
{
    [RoutePrefix("api/translation/static")]
    public class StaticTranslationController : BaseApiController
    {
        public StaticTranslationController(IAskTranslationApiBusiness askTranslationApiBusiness) : base(askTranslationApiBusiness)
        {
        }

        [Route("")]
        [ResponseType(typeof(TranslationResponseContract))]
        [HttpPost]
        public async Task<IHttpActionResult> Post(TranslationRequestContract translationRequestContract)
        {
            var translationResponseContract = await AskTranslationApiBusiness.TranslationCommands.GetTranslatedItems(translationRequestContract);

            return Ok(translationResponseContract);
        }


    }
}
