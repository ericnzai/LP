using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LP.Api.Shared.Interfaces.BusinessLayer.Content;
using LP.ServiceHost.DataContracts.Request.Content;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.Content.Controllers
{
    [RoutePrefix("api/content/conversion-tool-translation")]
    public class VAConversionToolTranslationController : BaseApiController
    {
        public VAConversionToolTranslationController(IAskContentApiBusiness askContentApiBusiness)
            : base(askContentApiBusiness)
        {
        }

        [Route("")]
        [HttpPost]
        [Authorize]
        [ResponseType(typeof (VAConversionToolTranslationDetailsResponseContract))]
        public async Task<IHttpActionResult> Post(VAConversionToolTranslationRequestContract conversionToolTranslationRequestContract)
        {
            var conversionToolTranslationDetailsResponseContract =
                await AskContentApiBusiness.VaConversionToolTranslationCommands.GetVAConversionToolTranslation(conversionToolTranslationRequestContract.Culture, conversionToolTranslationRequestContract.Path);

            return Ok(conversionToolTranslationDetailsResponseContract);
        }


        [Route("add")]
        [HttpPost]
        [Authorize]
        [ResponseType(typeof(VAConversionToolTranslationDetailsResponseContract))]
        public async Task<IHttpActionResult> Post(VAConversionToolTranslationSaveRequestContract conversionToolTranslationSaveRequestContract)
        {
            var conversionToolTranslationResponseContract = await AskContentApiBusiness.VaConversionToolTranslationCommands.SaveVAConversionToolTranslation(conversionToolTranslationSaveRequestContract.Culture, conversionToolTranslationSaveRequestContract.FileName, conversionToolTranslationSaveRequestContract.PermPath, conversionToolTranslationSaveRequestContract.TempPath, conversionToolTranslationSaveRequestContract.IsTranslationCompleted);

            return Ok(conversionToolTranslationResponseContract);
        }
    }
}
