using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LP.Api.Shared.Interfaces.BusinessLayer.Content;
using LP.ServiceHost.DataContracts.Request.Content;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.Content.Controllers
{
    [RoutePrefix("api/content/topic-category-translation")]
    public class TopicCategoryTranslationController : BaseApiController
    {
        public TopicCategoryTranslationController(IAskContentApiBusiness askContentApiBusiness) : base(askContentApiBusiness)
        {
        }

        [Route("")]
        [HttpGet]
        [Authorize]
        [ResponseType(typeof(CompleteTopicCategoryTranslationResponseContract))]
        public async Task<IHttpActionResult> Get()
        {
            var completeTopicCategoryTranslationResponseContract = await AskContentApiBusiness.TopicTranslationCommands.GetAllTopicCategories(GetCultureFromRequestHeader);

            return Ok(completeTopicCategoryTranslationResponseContract);
        }

        [Route("culture/{culture}")]
        [HttpGet]
        [Authorize]
        [ResponseType(typeof(CompleteTopicCategoryTranslationResponseContract))]
        public async Task<IHttpActionResult> GetByCulture(string culture)
        {
            var completeTopicCategoryTranslationResponseContract = await AskContentApiBusiness.TopicTranslationCommands.GetAllTopicCategories(culture);

            return Ok(completeTopicCategoryTranslationResponseContract);
        }

        [Route("{topicCategoryTranslationId}")]
        [HttpGet]
        [Authorize]
        [ResponseType(typeof(TopicCategoryTranslationFormResponseContract))]
        public async Task<IHttpActionResult> GetById(int topicCategoryTranslationId)
        {
            var topicCategoryTranslationResponseContract = await AskContentApiBusiness.TopicTranslationCommands.GetTopicCategoryByIdAndCulture(topicCategoryTranslationId, GetCultureFromRequestHeader);

            return Ok(topicCategoryTranslationResponseContract);
        }

        [Route("add")]
        [HttpPost]
        [Authorize]
        [ResponseType(typeof(TopicCategoryTranslationResponseContract))]
        public async Task<IHttpActionResult> Post(TopicCategoryTranslationRequestContract topicCategoryTranslationRequestContract)
        {
            var topicCategoryTranslationResponseContract = await AskContentApiBusiness.TopicTranslationCommands.AddTopicCategoryTranslation(GetCultureFromRequestHeader, topicCategoryTranslationRequestContract.TopicCategoryName, topicCategoryTranslationRequestContract.TopicCategoryId, GetAuthenticatedUserDetails().UserId, topicCategoryTranslationRequestContract.Staus);

            return Ok(topicCategoryTranslationResponseContract);
        }

        [Route("update")]
        [HttpPut]
        [Authorize]
        [ResponseType(typeof(TopicCategoryTranslationUpdateResponseContract))]
        public async Task<IHttpActionResult> Put(TopicCategoryTranslationUpdateRequestContract topicCategoryTranslationUpdateRequestContract)
        {
            var topicCategoryTranslationUpdateResponseContract = await AskContentApiBusiness.TopicTranslationCommands.UpdateTopicCategoryTranslation(GetCultureFromRequestHeader, topicCategoryTranslationUpdateRequestContract.TopicCategoryName, topicCategoryTranslationUpdateRequestContract.TopicCategoryId, GetAuthenticatedUserDetails().UserId, topicCategoryTranslationUpdateRequestContract.Staus);

            return Ok(topicCategoryTranslationUpdateResponseContract);
        }
    }
}