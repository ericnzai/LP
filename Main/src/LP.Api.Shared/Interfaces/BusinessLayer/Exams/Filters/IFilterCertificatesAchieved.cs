using System.Collections.Generic;
using System.Threading.Tasks;
using LP.EntityModels.Exam;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Exams.Filters
{
    public interface IFilterCertificatesAchieved
    {
        Task<IEnumerable<CertificatesAchieved>> GetOnlyWithLiveGroup(IEnumerable<CertificatesAchieved> input);
    }
}
