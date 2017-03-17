using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams;
using LP.Model.ViewModels.Dashboards.Country;
using LP.ServiceHost.DataContracts.Response.Exams;

namespace LP.Exams.Controllers.DropdownListsArea
{
      [RoutePrefix("api/exams/ddl-area/global")]
    public class GlobalDropdownListsController : BaseApiController
    {
          public GlobalDropdownListsController(IAskExamsApiBusiness askExamsApiBusiness)
              : base(askExamsApiBusiness)
         {
         }

         [Authorize]
         [HttpGet]
         [ResponseType(typeof(GlobalDropdownListsController))]
         [Route("")]
         public async Task<IHttpActionResult> Get()
         {
             var countryDropdownListsViewModel =
                 await
                    AskExamsApiBusiness.DashboardDropdownListsCommands.GetGlobalDropdownLists();

             return Ok(countryDropdownListsViewModel);

         }
    }
}
