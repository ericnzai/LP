using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams;
using LP.ServiceHost.DataContracts.Common.Exams.Dashboards.Trainer;

namespace LP.Exams.Controllers.Dashboard
{
    [RoutePrefix("api/exams/dashboard/activities")]
    public class TrainerActivitiesController : BaseApiController
    {
         public TrainerActivitiesController(IAskExamsApiBusiness askExamsApiBusiness)
             : base(askExamsApiBusiness)
         {
         }

         [Authorize]
         [HttpGet]
         [ResponseType(typeof(TrainerActivitiesContract))]
         [Route("trainer")]
         public async Task<IHttpActionResult> Get()
         {
             var userDetails = GetAuthenticatedUserDetails();

             var trainerActivitiesResponseContract =
                 await
                     AskExamsApiBusiness.DashboardActivitiesCommands.GetTrainerActivities(userDetails.UserId);

             return Ok(trainerActivitiesResponseContract);
        }

        [Authorize]
        [HttpGet]
        [ResponseType(typeof(TrainerActivitiesContract))]
        [Route("trainer/{jobRoleIds}")]
        public async Task<IHttpActionResult> GetTrainerActivities(string jobRoleIds)
        {
            var userDetails = GetAuthenticatedUserDetails();

            var trainerActivitiesResponseContract =
                await
                    AskExamsApiBusiness.DashboardActivitiesCommands.GetTrainerActivities(userDetails.UserId, jobRoleIds);

            return Ok(trainerActivitiesResponseContract);
        }
        [Authorize]
         [HttpGet]
         [ResponseType(typeof(TrainerActivitiesContract))]
         [Route("country/{id}")]
         public async Task<IHttpActionResult> GetCountryActivities(int id)
         {
             var trainerActivitiesResponseContract =
                 await
                     AskExamsApiBusiness.DashboardActivitiesCommands.GetCountryActivities(id);

             return Ok(trainerActivitiesResponseContract);
         }

        [Authorize]
        [HttpGet]
        [ResponseType(typeof(TrainerActivitiesContract))]
        [Route("country/{id}/{jobRoleIds}")]
        public async Task<IHttpActionResult> GetCountryActivities(int id, string jobRoleIds)
        {
            var trainerActivitiesResponseContract =
                await
                    AskExamsApiBusiness.DashboardActivitiesCommands.GetCountryActivities(id,jobRoleIds);

            return Ok(trainerActivitiesResponseContract);
        }
    }
}
