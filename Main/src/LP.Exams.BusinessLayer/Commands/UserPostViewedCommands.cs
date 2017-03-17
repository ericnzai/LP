using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams;
using LP.Api.Shared.Interfaces.Data;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams.Filters;
using LP.EntityModels;
using Status = LP.ServiceHost.DataContracts.Enums.Status;
namespace LP.Exams.BusinessLayer.Commands
{
    public class UserPostViewedCommands : IUserPostViewedCommands
    {
        private readonly IBaseCommands _baseCommands;
        private readonly IFilterAllowedUser _allowedUserFilter;

        public UserPostViewedCommands(IBaseCommands baseCommands, IFilterAllowedUser allowedUserFilter)
        {
            _baseCommands = baseCommands;
            _allowedUserFilter = allowedUserFilter;
        }

        public async Task<int> GetNumberOfUsersWithTrainingStartedForGroupType(int groupTypeId)
        {
            var groups = await _baseCommands.GetAllAsync<Group>();
            var queriedGroups = groups.Where(g => g.GroupTypeID == groupTypeId && g.StatusBankID == (int)Status.Live);
            var queriedGroupIds = queriedGroups.Select(g => g.GroupID).ToList();
            var userIds = await _allowedUserFilter.GetAllLiveUsersNotHiddenFromReportsIds();

            var postViewed = await _baseCommands.GetAllAsync<ltl_UserPostViewed>();
            var userCount =
                postViewed.Where(pv => queriedGroupIds.Contains(pv.upv_GroupId) && userIds.Contains(pv.upv_UserId)).Select(pv => pv.upv_UserId).Distinct().Count();

            return userCount;
        }

        public async Task<List<ltl_UserPostViewed>> GetUserPostsViewedForGroupIds(IEnumerable<int> groupIds)
        {
            var postsViewed = await 
                _baseCommands.GetConditionalAsync<ltl_UserPostViewed>(x => groupIds.Contains(x.upv_GroupId));

            return await postsViewed.ToListAsync();
        }

        public int GetNumberOfUsersWithTrainingStartedForGroupType(int groupTypeId, IEnumerable<int> userIds, IEnumerable<int> groupIds, List<ltl_UserPostViewed> userPostsViewed)
        {
            var userCount = userPostsViewed.Where(pv => groupIds.Contains(pv.upv_GroupId) && userIds.Contains(pv.upv_UserId)).Select(pv => pv.upv_UserId).Distinct().Count();

            return userCount;
        }

        public async Task<int> GetNumberOfUsersWithTrainingStartedForGroupTypeByRegion(int groupTypeId, List<int> regionUserIds)
        {
            var groups = await _baseCommands.GetAllAsync<Group>();
            var queriedGroups = groups.Where(g => g.GroupTypeID == groupTypeId && g.StatusBankID == (int)Status.Live);
            var queriedGroupIds = queriedGroups.Select(g => g.GroupID).ToList();

            var postViewed = await _baseCommands.GetAllAsync<ltl_UserPostViewed>();
            var userCount =
                postViewed.Where(pv => queriedGroupIds.Contains(pv.upv_GroupId) && regionUserIds.Contains(pv.upv_UserId)).Select(pv => pv.upv_UserId).Distinct().Count();

            return userCount;
        }

        public async Task<IEnumerable<IGrouping<short, short>>> GetUserPostsViewedGrouped(IEnumerable<int> userIds)
        {
            var userPostsViewed = await _baseCommands.GetConditionalAsync<ltl_UserPostViewed>(x => x.upv_GroupId > 0 && userIds.Contains(x.upv_UserId));

            return userPostsViewed.Select(m => new { m.upv_UserId,  m.upv_GroupId})
                .Distinct().GroupBy(a=>a.upv_UserId, a=>a.upv_GroupId);
        }

        public async Task<List<IGrouping<short, short>>> GetUserPostsViewedGroupedByGroup(IEnumerable<int> userIds)
        {
            var userPostsViewed = await _baseCommands.GetConditionalAsync<ltl_UserPostViewed>(x => x.upv_GroupId > 0 && userIds.Contains(x.upv_UserId));

            var grouped = userPostsViewed.Select(m => new { m.upv_GroupId, m.upv_UserId })
                .Distinct().GroupBy(a => a.upv_GroupId, a => a.upv_UserId);

            return await grouped.ToListAsync();
        }

        public async Task<int> GetUserPostsViewedCountForGroupIds(IEnumerable<int> userIds, IEnumerable<int> groupIds)
        {
            var userPostsViewed = await _baseCommands.GetConditionalAsync<ltl_UserPostViewed>(x => groupIds.Contains(x.upv_GroupId) && userIds.Contains(x.upv_UserId));

            

            return userPostsViewed.Select(u => u.upv_UserId).Distinct().Count();
        }

        public async Task<Dictionary<int, int>> GetUserPostsViewedGroupedByGroupType(IEnumerable<int> userIds, List<IGrouping<ltl_GroupType, Group>> groupsGroupedByGroupTypes)
        {
            var userPostsViewed = await _baseCommands.GetConditionalAsync<ltl_UserPostViewed>(x => x.upv_GroupId > 0 && userIds.Contains(x.upv_UserId));

            var userPostsViewedGroupedByGroupId = userPostsViewed.Select(m => new {m.upv_GroupId, m.upv_UserId})
                .Distinct().GroupBy(a => a.upv_GroupId, a => a.upv_UserId);

            var dict = new Dictionary<int, int>();

            foreach (var groupsGroupedByGroupType in groupsGroupedByGroupTypes)
            {
                var groupType = groupsGroupedByGroupType.Key;   

                var groupIdsForGroupType = groupsGroupedByGroupType.Select(group => group.GroupID);

                var userIdsWhoViewedGroupType = userPostsViewedGroupedByGroupId.Where(a => groupIdsForGroupType.Contains(a.Key)).SelectMany(uid => uid);

                dict.Add(groupType.ID, userIdsWhoViewedGroupType.Distinct().Count());
            }

            return dict;
        }
        
        public async Task<List<IGrouping<short, short>>> GetUserPostsViewedGroupedList(IEnumerable<int> userIds)
        {
            var userPostsViewed = await GetUserPostsViewedGrouped(userIds);

            return userPostsViewed.ToList();
        }
    }
}
