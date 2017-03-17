using LP.Api.Shared.Controllers;
using LP.Api.Shared.Interfaces.BusinessLayer.Content;

namespace LP.Content.Controllers
{
    public class BaseApiController : SharedBaseApiController
    {
        protected readonly IAskContentApiBusiness AskContentApiBusiness;

        public BaseApiController(IAskContentApiBusiness askContentApiBusiness)
        {
            AskContentApiBusiness = askContentApiBusiness;
        }
    }
}
