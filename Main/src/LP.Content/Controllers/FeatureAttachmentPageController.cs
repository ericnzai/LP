using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LP.Api.Shared.Interfaces.BusinessLayer.Content;
using LP.ServiceHost.DataContracts.Response.Content.FeatureAttachment;

namespace LP.Content.Controllers
{
    [RoutePrefix("api/content/feature-attachment-page")]
    public class FeatureAttachmentPageController : BaseApiController
    {
        public FeatureAttachmentPageController(IAskContentApiBusiness askContentApiBusiness) : base(askContentApiBusiness)
        {
        }

        [Route("{pageNumber}")]
        [HttpGet]
        [Authorize]
        [ResponseType(typeof(FeatureAttachmentPageResponseContract))]
        public async Task<IHttpActionResult> Get(int pageNumber)
        {
            var userDetails = GetAuthenticatedUserDetails();

            var featureAttachmentPageResponseContract =
                await AskContentApiBusiness.FeatureAttachmentCommands.GetFeatureAttachmentPageAsync(pageNumber, userDetails);

            return Ok(featureAttachmentPageResponseContract);
        }
    }
}
