using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LP.Api.Shared.Interfaces.BusinessLayer.Content;
using LP.ServiceHost.DataContracts.Request.Content;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.Content.Controllers
{
    [RoutePrefix("api/content/topic-category")]
    public class TopicCategoryController : BaseApiController
    {
        public TopicCategoryController(IAskContentApiBusiness askContentApiBusiness)
            : base(askContentApiBusiness)
        {

        }

        [Route("")]
        [HttpGet]
        [Authorize]
        [ResponseType(typeof(TopicCategoriesResponseContract))]
        public async Task<IHttpActionResult> Get()
        {
            var topicCategoryResponseContract = await AskContentApiBusiness.TopicCommands.GetAllTopicCategories(GetCultureFromRequestHeader);

            return Ok(topicCategoryResponseContract);
        }

        [Route("add")]
        [HttpPost]
        [Authorize]
        [ResponseType(typeof(TopicCategoryResponseContract))]
        public async Task<IHttpActionResult> Post(TopicCategoryRequestContract topicCategoryRequestContract)
        {
            var topicCategoryResponseContract = await AskContentApiBusiness.TopicCommands.AddTopicCategory(GetCultureFromRequestHeader, topicCategoryRequestContract.CategoryName);

            return Ok(topicCategoryResponseContract);
        }

        [Route("delete/{categoryId}")]
        [HttpDelete]
        [Authorize]
        [ResponseType(typeof(TopicCategoryDeleteResponseContract))]
        public async Task<IHttpActionResult> Delete(int categoryId)
        {
            var topicCategoryDeleteResponseContract = await AskContentApiBusiness.TopicCommands.DeleteTopicCategory(GetCultureFromRequestHeader, categoryId);

            return Ok(topicCategoryDeleteResponseContract);
        }

        [Route("update")]
        [HttpPut]
        [Authorize]
        [ResponseType(typeof(TopicCategoryUpdateResponseContract))]
        public async Task<IHttpActionResult> Put(TopicCategoryUpdateRequestContract topicCategoryUpdateRequestContract)
        {
            var topicCategoryUpdateResponseContract = await AskContentApiBusiness.TopicCommands.UpdateTopicCategory(GetCultureFromRequestHeader, topicCategoryUpdateRequestContract.CategoryId, topicCategoryUpdateRequestContract.CategoryName);

            return Ok(topicCategoryUpdateResponseContract);
        }
    }
}
