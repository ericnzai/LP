using System.Web.Http.Description;
using LP.Api.Shared.Interfaces.BusinessLayer.Content;
using LP.ServiceHost.DataContracts.Request.Content;
using LP.ServiceHost.DataContracts.Response.Content;
using System.Web.Http;
using System.Threading.Tasks;
using System;

namespace LP.Content.Controllers
{
    [RoutePrefix("api/content/topic-translation")]
    public class TopicTranslationController : BaseApiController
    {
        public TopicTranslationController(IAskContentApiBusiness askContentApiBusiness) : base(askContentApiBusiness)
        {
        }

        [Route("")]
        [HttpGet]
        [Authorize]
        [ResponseType(typeof(CompleteTopicTranslationResponseContract))]
        public async Task<IHttpActionResult> Get()
        {
            var completeTopicTranslationResponseContract = await AskContentApiBusiness.TopicTranslationCommands.GetAllTopics(GetCultureFromRequestHeader);

            return Ok(completeTopicTranslationResponseContract);
        }

        [Route("{topicTranslationId}")]
        [HttpGet]
        [Authorize]
        [ResponseType(typeof(TopicTranslationFormResponseContract))]
        public async Task<IHttpActionResult> GetById(int topicTranslationId)
        {
            var topicTranslationResponseContract = await AskContentApiBusiness.TopicTranslationCommands.GetTopicByIdAndCulture(topicTranslationId, GetCultureFromRequestHeader);

            return Ok(topicTranslationResponseContract);
        }

        [Route("add")]
        [HttpPost]
        [Authorize]
        [ResponseType(typeof(TopicTranslationResponseContract))]
        public async Task<IHttpActionResult> Post(TopicTranslationRequestContract topicTranslationRequestContract)
        {
            var topicTranslationResponseContract = await AskContentApiBusiness.TopicTranslationCommands.AddTopicTranslation(GetCultureFromRequestHeader, topicTranslationRequestContract.TopicName, topicTranslationRequestContract.TopicId, GetAuthenticatedUserDetails().UserId, topicTranslationRequestContract.Staus);

            return Ok(topicTranslationResponseContract);
        }

        [Route("update")]
        [HttpPut]
        [Authorize]
        [ResponseType(typeof(TopicTranslationUpdateResponseContract))]
        public async Task<IHttpActionResult> Put(TopicTranslationUpdateRequestContract topicTranslationUpdateRequestContract)
        {
            var topicTranslationUpdateResponseContract = await AskContentApiBusiness.TopicTranslationCommands.UpdateTopicTranslation(GetCultureFromRequestHeader, topicTranslationUpdateRequestContract.TopicName, topicTranslationUpdateRequestContract.TopicId, GetAuthenticatedUserDetails().UserId, topicTranslationUpdateRequestContract.Staus);

            return Ok(topicTranslationUpdateResponseContract);
        }
    }
}