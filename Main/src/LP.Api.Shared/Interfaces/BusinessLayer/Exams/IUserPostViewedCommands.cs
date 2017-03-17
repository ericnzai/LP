using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LP.EntityModels;
using LP.Model.Exams;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Exams
{
    public interface IUserPostViewedCommands
    {
        Task<int> GetNumberOfUsersWithTrainingStartedForGroupType(int groupTypeId);
        Task<int> GetNumberOfUsersWithTrainingStartedForGroupTypeByRegion(int groupTypeId, List<int> regionUserIds);
        Task<IEnumerable<IGrouping<short, short>>> GetUserPostsViewedGrouped(IEnumerable<int> userIds);
        Task<List<IGrouping<short, short>>> GetUserPostsViewedGroupedList(IEnumerable<int> userIds);

        int GetNumberOfUsersWithTrainingStartedForGroupType(int groupTypeId, IEnumerable<int> userIds,
            IEnumerable<int> groupIds, List<ltl_UserPostViewed> userPostsViewed);

        Task<List<ltl_UserPostViewed>> GetUserPostsViewedForGroupIds(IEnumerable<int> groupIds);

        Task<Dictionary<int, int>> GetUserPostsViewedGroupedByGroupType(IEnumerable<int> userIds,
            List<IGrouping<ltl_GroupType, Group>> groupsGroupedByGroupTypes);

        Task<int> GetUserPostsViewedCountForGroupIds(IEnumerable<int> userIds, IEnumerable<int> groupIds);
        
        Task<List<IGrouping<short, short>>> GetUserPostsViewedGroupedByGroup(IEnumerable<int> userIds);
    }
}
