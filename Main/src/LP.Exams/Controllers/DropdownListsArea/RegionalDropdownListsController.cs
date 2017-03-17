using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams;
using LP.Model.ViewModels.Dashboards.Country;
using LP.Model.ViewModels.Dashboards.Regional;
using LP.ServiceHost.DataContracts.Response.Exams;

namespace LP.Exams.Controllers.DropdownListsArea
{
      [RoutePrefix("api/exams/ddl-area/regional")]
    public class RegionalDropdownListsController : BaseApiController
    {
          public RegionalDropdownListsController(IAskExamsApiBusiness askExamsApiBusiness)
              : base(askExamsApiBusiness)
         {
         }

         [Authorize]
         [HttpGet]
         [ResponseType(typeof(RegionalDropdownListsViewModel))]
         [Route("")]
         public async Task<IHttpActionResult> Get()
         {
             var userDetails = GetAuthenticatedUserDetails();

             var regionalDropdownListsViewModel =
                 await
                    AskExamsApiBusiness.DashboardDropdownListsCommands.GetRegionalDropdownLists(userDetails);

             return Ok(regionalDropdownListsViewModel);
         }
    }
}
