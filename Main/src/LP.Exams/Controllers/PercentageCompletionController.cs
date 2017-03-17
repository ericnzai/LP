using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams;
using LP.ServiceHost.DataContracts.Common.Exams;
using LP.ServiceHost.DataContracts.Response.Exams;

namespace LP.Exams.Controllers
{
    [RoutePrefix("api/exams/percentage-completion")]
    public class PercentageCompletionController : BaseApiController
    {
        public PercentageCompletionController(IAskExamsApiBusiness askExamsApiBusiness) : base(askExamsApiBusiness)
        {
        }

        [Route("{groupId}")]
        [ResponseType(typeof(GroupsPercentageCompleteResponseContract))]
        [HttpGet]
        public async Task<IHttpActionResult> Get(int groupId)
        {
            var userDetails = GetAuthenticatedUserDetails();

            var groupPercentageComplete = await AskExamsApiBusiness.PercentageCompletionCommands.PercentageAchievedForGroup(userDetails.UserId, groupId);

            var groupsPercentageCompleteResponseContract = new GroupsPercentageCompleteResponseContract
            {
                GroupsPercentageComplete = new List<GroupPercentageComplete>
                {
                    groupPercentageComplete
                }
            };

            return Ok(groupsPercentageCompleteResponseContract);
        }
    }
}
