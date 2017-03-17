using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LP.Api.Shared.Interfaces.BusinessLayer.Common;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams.Filters;
using LP.EntityModels.Exam;

namespace LP.Exams.BusinessLayer.Filters
{
    public class FilterCertificatesAchieved : IFilterCertificatesAchieved
    {
        private readonly IGroupCommands _groupCommands;

        public FilterCertificatesAchieved(IGroupCommands groupCommands)
        {
            _groupCommands = groupCommands;
        }

        public async Task<IEnumerable<CertificatesAchieved>> GetOnlyWithLiveGroup(IEnumerable<CertificatesAchieved> input)
        {
            var certificatesAchieved = input.ToList();

            var liveGroupIds = (await _groupCommands.AreLiveByIds(certificatesAchieved.Select(c => c.GroupId))).Where(groupIdToIsLiveFlag => groupIdToIsLiveFlag.Value).Select(groupIdToIsLiveFlag => groupIdToIsLiveFlag.Key);
            return certificatesAchieved.Where(c => liveGroupIds.Contains(c.GroupId));
        }
    }
}
