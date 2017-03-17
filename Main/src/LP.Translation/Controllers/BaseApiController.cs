using System.Web.Http;
using LP.Api.Shared.Interfaces.BusinessLayer.Translation;

namespace LP.Translation.Controllers
{
    public class BaseApiController : ApiController
    {
        protected IAskTranslationApiBusiness AskTranslationApiBusiness;

        public BaseApiController(IAskTranslationApiBusiness askTranslationApiBusiness)
        {
            AskTranslationApiBusiness = askTranslationApiBusiness;
        }
    }
}
