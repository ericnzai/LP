using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LP.Api.Shared.Interfaces.BusinessLayer.Content;
using System.Web.Http;
using System.Web.Http.Description;
using LP.ServiceHost.DataContracts.Response.Content;
using System.Threading.Tasks;

namespace LP.Content.Controllers
{
    [RoutePrefix("api/content/culture-menu")]
    public class CultureMenuController : BaseApiController
    {
        public CultureMenuController(IAskContentApiBusiness askContentApiBusiness) : base(askContentApiBusiness)
        {
        }

        [Route("")]
        [HttpGet]
        [Authorize]
        [ResponseType(typeof(CompleteCultureMenuResponseContract))]
        public async Task<IHttpActionResult> Get()
        {
            var completeCultureMenuResponseContract = await AskContentApiBusiness.CultureCommands.GetAvailableCultures(GetAuthenticatedUserDetails());

            return Ok(completeCultureMenuResponseContract);
        }
    }
}