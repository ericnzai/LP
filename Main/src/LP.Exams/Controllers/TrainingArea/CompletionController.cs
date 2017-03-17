using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams;
using LP.ServiceHost.DataContracts.Response.Exams;

namespace LP.Exams.Controllers.TrainingArea
{
    [RoutePrefix("api/exams/training-area/completion")]
    public class CompletionController : BaseApiController
    {
        public CompletionController(IAskExamsApiBusiness askExamsApiBusiness) : base(askExamsApiBusiness)
        {
        }
        [Route("")]
        [ResponseType(typeof(TrainingAreasCompleteResponseContract))]
        public async Task<IHttpActionResult> Get()
        {
            var authenticatedUserDetails = GetAuthenticatedUserDetails();

            var trainingAreaCompletions = await
                    AskExamsApiBusiness.NumberAchievedCommands.NumberOfModulesCompletedForAllTrainingAreasAsync(
                        authenticatedUserDetails);

            var trainingAreasCompleteResponseContract = new TrainingAreasCompleteResponseContract
            {
                TrainingAreaCompletions = trainingAreaCompletions
            };

            return Ok(trainingAreasCompleteResponseContract);
        }
    }
}
