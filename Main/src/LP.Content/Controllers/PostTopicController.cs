using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LP.Api.Shared.Interfaces.BusinessLayer.Content;
using LP.ServiceHost.DataContracts.Request.Content;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.Content.Controllers
{
    [RoutePrefix("api/content/post-topic")]
    public class PostTopicController : BaseApiController
    {
        public PostTopicController(IAskContentApiBusiness askContentApiBusiness)
            : base(askContentApiBusiness)
        {
        }

        [Route("add")]
        [HttpPost]
        [Authorize]
        [ResponseType(typeof(TopicResponseContract))]
        public async Task<IHttpActionResult> Post(PostTopicsRequestContract postTopicsRequestContract)
        {
            var postTopicsResponseContract = await AskContentApiBusiness.TopicCommands.SavePostTopics(postTopicsRequestContract.Topics, postTopicsRequestContract.PostId);

            return Ok(postTopicsResponseContract);
        }

        [Route("")]
        [HttpPost]
        [Authorize]
        [ResponseType(typeof(PostTopicResponseContract))]
        public async Task<IHttpActionResult> Post(PostTopicRequestContract postTopicRequestContract)
        {
            var postTopicsResponseContract = await AskContentApiBusiness.TopicCommands.GetPostTopics(GetCultureFromRequestHeader, postTopicRequestContract.PostId);

            return Ok(postTopicsResponseContract);
        }

        [Route("search")]
        [HttpGet]
        [Authorize]
        [ResponseType(typeof(PostTopicResponseContract))]
        public async Task<IHttpActionResult> Get()
        {
            var postTopicsResponseContract = await AskContentApiBusiness.TopicCommands.GetSearchTopics(GetCultureFromRequestHeader);

            return Ok(postTopicsResponseContract);
        }
    }
}
