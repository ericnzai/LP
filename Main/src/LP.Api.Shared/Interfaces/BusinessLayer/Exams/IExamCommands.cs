using System.Collections.Generic;
using System.Threading.Tasks;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Exams
{
    public interface IExamCommands
    {
        Task<List<short>> GetExamIdsForGroup(int groupId, bool onlyLive = false);
        Task<List<short>> GetExamIdsForGroups(IEnumerable<int> groupIds, bool onlyLive = false);
    }
}
