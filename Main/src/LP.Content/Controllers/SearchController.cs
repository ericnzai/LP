using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LP.Api.Shared.Interfaces.BusinessLayer.Content;
using LP.ServiceHost.DataContracts.Request.Content;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.Content.Controllers
{
    [RoutePrefix("api/content/search-item")]
    public class SearchController : BaseApiController
    {
        public SearchController(IAskContentApiBusiness askContentApiBusiness)
            : base(askContentApiBusiness)
        {
        }

        [Route("")]
        [HttpPost]
        [Authorize]
        [ResponseType(typeof(SearchItemsResponseContract))]
        public async Task<IHttpActionResult> Post(SearchRequestContract searchRequestContract)
        {
            var searchItemsResponseContract = await AskContentApiBusiness.SearchCommands.GetAllSearchItems(GetCultureFromRequestHeader, searchRequestContract.SearchTerm, GetAuthenticatedUserDetails().RoleIds, searchRequestContract.GroupTypeId, searchRequestContract.TopicIds);

            return Ok(searchItemsResponseContract);
        }
    }
}
