using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LP.Api.Shared.Interfaces.BusinessLayer.Content;
using LP.Api.Shared.Providers;
using LP.ServiceHost.DataContracts.Request.Content;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.Content.Controllers
{
    [RoutePrefix("api/content/conversion-tool")]
    public class VAConversionToolController : BaseApiController
    {
        public VAConversionToolController(IAskContentApiBusiness askContentApiBusiness)
            : base(askContentApiBusiness)
        {
        }

        [Route("")]
        [HttpPost]
        [Authorize]
        [ResponseType(typeof(VAConversionToolDetailsResponseContract))]
        public async Task<IHttpActionResult> Post(VAConversionToolPathRequestContract conversionToolPathRequestContract)
        {
            var conversionToolDetailsResponseContract = await AskContentApiBusiness.VaConversionToolCommands.GetVAConversionTool(ConstantProvider.GlobalCulture, GetAuthenticatedUserDetails(), conversionToolPathRequestContract.PermPath);

            return Ok(conversionToolDetailsResponseContract);
        }

        [Route("add")]
        [HttpPost]
        [Authorize]
        [ResponseType(typeof(VAConversionToolResponseContract))]
        public async Task<IHttpActionResult> Post(VAConversionToolRequestContract conversionToolRequestContract)
        {
            var conversionToolResponseContract = await AskContentApiBusiness.VaConversionToolCommands.SaveVAConversionTool(ConstantProvider.GlobalCulture, conversionToolRequestContract.FileName, conversionToolRequestContract.FileDownloadPath, conversionToolRequestContract.Comments);

            return Ok(conversionToolResponseContract);
        }

        [Route("download")]
        [HttpPost]
        [Authorize]
        [ResponseType(typeof(VAConversionToolDownloadPdfResponseContract))]
        public async Task<IHttpActionResult> Post(VAConversionToolDownloadPdfRequestContract conversionToolDownloadPdfRequestContract)
        {
            var conversionToolDownloadResponseContract = await AskContentApiBusiness.VaConversionToolCommands.GetConversionToolTranslationFilePath(GetCultureFromRequestHeader, conversionToolDownloadPdfRequestContract.Path);

            return Ok(conversionToolDownloadResponseContract);
        }
    }
}
