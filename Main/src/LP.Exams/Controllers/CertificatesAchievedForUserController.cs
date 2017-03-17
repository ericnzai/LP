using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams;
using LP.ServiceHost.DataContracts.Common.Exams;

namespace LP.Exams.Controllers
{
    [RoutePrefix("api/exams/certificatesachievedforuser")]
    public class CertificatesAchievedForUserController : BaseApiController
    {
        public CertificatesAchievedForUserController(IAskExamsApiBusiness askExamsApiBusiness) : base(askExamsApiBusiness)
        {
        }

        [Route("")]
        [ResponseType(typeof(IEnumerable<CertificateAchievedInformation>))]
        [Authorize]
        public async Task<IHttpActionResult> Get()
        {
            var userDetails = GetAuthenticatedUserDetails();

            var certificatesAchievedByUser = await AskExamsApiBusiness.CertificatesAchievedCommands.GetCertificatesAchievedByUser(userDetails);

            return Ok(certificatesAchievedByUser);
        }
    }
}
