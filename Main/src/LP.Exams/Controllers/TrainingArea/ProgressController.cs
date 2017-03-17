using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams;
using LP.ServiceHost.DataContracts.Response.Exams;

namespace LP.Exams.Controllers.TrainingArea
{
    [RoutePrefix("api/exams/training-area")]
    public class ProgressController : BaseApiController
    {
        public ProgressController(IAskExamsApiBusiness askExamsApiBusiness)
            : base(askExamsApiBusiness)
        {
        }

        [Route("progress/{trainingAreaId}")]
        [ResponseType(typeof(TrainingAreaProgressResponseContract))]
        public async Task<IHttpActionResult> Get(int trainingAreaId)
        {
            var authenticatedUserDetails = GetAuthenticatedUserDetails();

            var trainingAreaProgressResponseContract =
                await AskExamsApiBusiness.GroupCompletionCommands.GetAllCompleteGroupsForTrainingAreaAsync(trainingAreaId, authenticatedUserDetails);

            return Ok(trainingAreaProgressResponseContract);
        }
    }
}
