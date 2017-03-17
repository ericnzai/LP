using System.Web.Http;
using LP.Api.Shared.Controllers;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams;

namespace LP.Exams.Controllers
{
    [Authorize]
    public class BaseApiController : SharedBaseApiController
    {
        protected readonly IAskExamsApiBusiness AskExamsApiBusiness;

        public BaseApiController(IAskExamsApiBusiness askExamsApiBusiness)
        {
            AskExamsApiBusiness = askExamsApiBusiness;
        }
    }
}
