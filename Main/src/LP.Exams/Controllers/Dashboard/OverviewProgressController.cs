using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams;
using LP.ServiceHost.DataContracts.Response.Exams;

namespace LP.Exams.Controllers.Dashboard
{
    [RoutePrefix("api/exams/dashboard/overview-progress")]
    public class OverviewProgressController : BaseApiController
    {
         public OverviewProgressController(IAskExamsApiBusiness askExamsApiBusiness) : base(askExamsApiBusiness)
         {
         }

         [Authorize]
         [HttpGet]
         [ResponseType(typeof(OverviewGroupTypeProgressResponseContract))]
         [Route("")]
         public async Task<IHttpActionResult> Get()
         {
             var overviewGroupProgressResponseContract =
                 await
                     AskExamsApiBusiness.OverviewGroupTypeProgressCommands.GetOverviewGroupTypeProgressResponseContract();

            return Ok(overviewGroupProgressResponseContract);
        }

         [Authorize]
         [HttpGet]
         [ResponseType(typeof(OverviewGroupTypeProgressResponseContract))]
         [Route("trainer")]
         public async Task<IHttpActionResult> GetOverviewTrainer()
         {
             
                 var userDetails = GetAuthenticatedUserDetails();
                 var trainerId = userDetails.UserId;
 

             var count = await AskExamsApiBusiness.OverviewGroupTypeProgressCommands.GetOverviewGroupTypeProgressResponseContract(trainerId);
             return Ok(count);
         }

         [Authorize]
         [HttpGet]
         [ResponseType(typeof(OverviewGroupTypeProgressResponseContract))]
         [Route("region/{regionId}")]
         public async Task<IHttpActionResult> GetOverviewRegion(int regionId)
         {
             var overviewRegionProgressResponseContract =
                 await
                     AskExamsApiBusiness.OverviewGroupTypeProgressCommands.GetOverviewGroupTypeProgressResponseContract(regionId, 0, 0, 0);

             return Ok(overviewRegionProgressResponseContract);
         }

         [Authorize]
         [HttpGet]
         [ResponseType(typeof(OverviewGroupTypeProgressResponseContract))]
         [Route("country/{countryId}")]
         public async Task<IHttpActionResult> GetOverviewCountry(int countryId)
         {
             var overviewRegionProgressResponseContract =
                 await
                     AskExamsApiBusiness.OverviewGroupTypeProgressCommands.GetOverviewGroupTypeProgressResponseContract(0, countryId, 0, 0);

             return Ok(overviewRegionProgressResponseContract);
         }

         [Authorize]
         [HttpGet]
         [ResponseType(typeof(OverviewCountryProgressResponseContract))]
         [Route("country-statistics")]
         public async Task<IHttpActionResult> GetOverviewCountry()
         {
             var overviewCountryResponseContract =
                 await
                     AskExamsApiBusiness.OverviewCountryProgressCommands.GetOverviewCountryProgressResponseContract();

             return Ok(overviewCountryResponseContract);
         }

        [Authorize]
        [HttpGet]
        [ResponseType(typeof(OverviewCountryProgressResponseContract))]
        [Route("country-statistics/{jobRolesIds}")]
        public async Task<IHttpActionResult> GetOverviewCountry(string jobRolesIds)
        {
            var overviewCountryResponseContract =
                await
                    AskExamsApiBusiness.OverviewCountryProgressCommands.GetOverviewCountryProgressFilteredByJobFunction(
                        jobRolesIds);

            return Ok(overviewCountryResponseContract);
        }
         [Authorize]
         [HttpGet]
         [ResponseType(typeof(OverviewCountryProgressResponseContract))]
         [Route("country-statistics/region/{regionId}")]
         public async Task<IHttpActionResult> GetOverviewRegionCountry(int regionId)
         {
             var overviewCountryResponseContract =
                 await
                     AskExamsApiBusiness.OverviewCountryProgressCommands.GetOverviewCountryProgressForRegionResponseContract(regionId);

             return Ok(overviewCountryResponseContract);
         }
         [Authorize]
         [HttpGet]
         [ResponseType(typeof(OverviewCountryProgressResponseContract))]
         [Route("country-statistics/region/{jobRoleIds}/{regionId}")]
        public async Task<IHttpActionResult> GetOverviewRegionCountry(string jobRoleIds,int regionId)
        {
            var overviewCountryResponseContract =
                await
                    AskExamsApiBusiness.OverviewCountryProgressCommands.GetOverviewCountryProgressForRegionFilteredResponseContract(jobRoleIds,regionId);

            return Ok(overviewCountryResponseContract);
        }
        [Authorize]
         [HttpGet]
         [ResponseType(typeof(GroupTypeHeadersResponseContract))]
         [Route("group-type-headers")]
         public async Task<IHttpActionResult> GetOverviewGroupTypeHeaders()
         {
             var performanceGroupTypeContract =
                 await
                     AskExamsApiBusiness.OverviewCountryProgressCommands.GetGroupTypeTableHeader();

             return Ok(performanceGroupTypeContract);
         }
    }
}
