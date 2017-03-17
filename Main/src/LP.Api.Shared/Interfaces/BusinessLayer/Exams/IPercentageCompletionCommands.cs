using System.Collections.Generic;
using System.Threading.Tasks;
using LP.ServiceHost.DataContracts.Common.Exams;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Exams
{
    public interface IPercentageCompletionCommands
    {
        Task<GroupPercentageComplete> PercentageAchievedForGroup(int userId, int groupId);
        Task<List<GroupPercentageComplete>> PercentageAchievedForGroups(int userId, IEnumerable<int> groupIds);
    }
}
