using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LP.Api.Shared.Interfaces.BusinessLayer.Content;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.Content.Controllers
{
    [RoutePrefix("api/content/feature-attachment/modal")]
    public class FeatureAttachmentModalController : BaseApiController
    {
        public FeatureAttachmentModalController(IAskContentApiBusiness askContentApiBusiness) : base(askContentApiBusiness)
        {
        }

        [Route("{featureAttachmentId}")]
        [HttpGet]
        [Authorize]
        [ResponseType(typeof(FeatureAttachmentModalResponseContract))]
        public async Task<IHttpActionResult> Get(int featureAttachmentId)
        {
            var userDetails = GetAuthenticatedUserDetails();

            var featureAttachmentModalResponseContract = await
                AskContentApiBusiness.FeatureAttachmentCommands.GetFeatureAttachmentModalResponseContract(featureAttachmentId, userDetails);

            return Ok(featureAttachmentModalResponseContract);
        }
    }
}
