using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LP.Api.Shared.Interfaces.BusinessLayer.Content;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.Content.Controllers
{
    [RoutePrefix("api/content/group")]
    public class GroupController : BaseApiController
    {
        public GroupController(IAskContentApiBusiness askContentApiBusiness)
            : base(askContentApiBusiness)
        {
        }

        [Route("")]
        [HttpGet]
        [Authorize]
        [ResponseType(typeof(GroupResponseContract))]
        public async Task<IHttpActionResult> Get()
        {
            var glossaryItemsResponseContract = await AskContentApiBusiness.GroupCommands.GetAllLiveGroupResponseContractsForGlossaryDropDown(GetCultureFromRequestHeader);

            return Ok(glossaryItemsResponseContract);
        }
    }
}