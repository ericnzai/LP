using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LP.Api.Shared.Interfaces.BusinessLayer.Content;
using LP.ServiceHost.DataContracts.Request.Content;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.Content.Controllers
{
    [RoutePrefix("api/content/topic")]
    public class TopicController : BaseApiController
    {
        public TopicController(IAskContentApiBusiness askContentApiBusiness)
            : base(askContentApiBusiness)
        {
        }

        [Route("")]
        [HttpGet]
        [Authorize]
        [ResponseType(typeof(TopicsResponseContract))]
        public async Task<IHttpActionResult> Get()
        {
            var topicResponseContract = await AskContentApiBusiness.TopicCommands.GetAllTopics(GetCultureFromRequestHeader);

            return Ok(topicResponseContract);
        }

        [Route("add")]
        [HttpPost]
        [Authorize]
        [ResponseType(typeof(TopicResponseContract))]
        public async Task<IHttpActionResult> Post(TopicRequestContract topicRequestContract)
        {
            var topicResponseContract = await AskContentApiBusiness.TopicCommands.AddTopic(GetCultureFromRequestHeader, topicRequestContract.TopicName, topicRequestContract.CategoryId, DateTime.Now, GetAuthenticatedUserDetails().UserId);

            return Ok(topicResponseContract);
        }

        [Route("delete/{topicId}")]
        [HttpDelete]
        [Authorize]
        [ResponseType(typeof(TopicDeleteResponseContract))]
        public async Task<IHttpActionResult> Delete(int topicId)
        {
            var topicDeleteResponseContract = await AskContentApiBusiness.TopicCommands.DeleteTopic(GetCultureFromRequestHeader, topicId);

            return Ok(topicDeleteResponseContract);
        }

        [Route("update")]
        [HttpPut]
        [Authorize]
        [ResponseType(typeof(TopicUpdateResponseContract))]
        public async Task<IHttpActionResult> Put(TopicUpdateRequestContract topicUpdateRequestContract)
        {
            var topicUpdateResponseContract = await AskContentApiBusiness.TopicCommands.UpdateTopic(GetCultureFromRequestHeader, topicUpdateRequestContract.TopicId, topicUpdateRequestContract.TopicName, topicUpdateRequestContract.CategoryId);

            return Ok(topicUpdateResponseContract);
        }
    }
}
