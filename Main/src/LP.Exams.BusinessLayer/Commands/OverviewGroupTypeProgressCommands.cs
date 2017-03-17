using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LP.Api.Shared.Interfaces.BusinessLayer.Common;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams.Filters;
using LP.EntityModels;
using LP.EntityModels.Exam;
using LP.ServiceHost.DataContracts.Common.Exams.Dashboards;
using LP.ServiceHost.DataContracts.Response.Exams;

namespace LP.Exams.BusinessLayer.Commands
{
    public class OverviewGroupTypeProgressCommands : IOverviewGroupTypeProgressCommands
    {
        private readonly IAttemptsCommands _attemptsCommands;
        private readonly IFilterAllowedUser _allowedUserFilter;
        private readonly ICommonCalculatorCommands _commonCalculatorCommands;
        private readonly ICertificatesAchievedCommands _certificatesAchievedCommands;
        private readonly IUserPostViewedCommands _userPostViewedCommands;
        private readonly IFilterAllowedGroups _filterAllowedGroups;

        private List<Group> _groups;
        private List<CertificatesAchieved> _certificatesAchieved;
        private List<Attempt> _attempts;
        private List<IGrouping<short, short>> _userIdsViewedGroupsGroupedByGroupId;
        

        public OverviewGroupTypeProgressCommands(
            IAttemptsCommands attemptsCommands, 
            IFilterAllowedUser allowedUserFilter,
            ICommonCalculatorCommands commonCalculatorCommands, ICertificatesAchievedCommands certificatesAchievedCommands, 
            IUserPostViewedCommands userPostViewedCommands, IFilterAllowedGroups filterAllowedGroups)
        {
            _attemptsCommands = attemptsCommands;
            _allowedUserFilter = allowedUserFilter;
            _commonCalculatorCommands = commonCalculatorCommands;
            _certificatesAchievedCommands = certificatesAchievedCommands;
            _userPostViewedCommands = userPostViewedCommands;
            _filterAllowedGroups = filterAllowedGroups;
        }

        public async Task<OverviewGroupTypeProgressResponseContract> GetOverviewGroupTypeProgressResponseContract(int regionId, int countryId, int jobRoleId, int trainerId)
        {
            var userIds = await _allowedUserFilter.GetFilteredUserIdsNotHiddenFromReports(regionId, countryId, jobRoleId, trainerId);

            return await GetOverviewGroupTypeProgressResponseContract(userIds);
        }

        public async Task<OverviewGroupTypeProgressResponseContract> GetOverviewGroupTypeProgressResponseContract()
        {
            var userIds = await _allowedUserFilter.GetAllLiveUsersNotHiddenFromReportsIds();

            return await GetOverviewGroupTypeProgressResponseContract(userIds);
        }

        public async Task<OverviewGroupTypeProgressResponseContract> GetOverviewGroupTypeProgressResponseContract(int trainerId)
        {
            var userIds = await _allowedUserFilter.GetFilteredUserIdsNotHiddenFromReports(trainerId);

            return await GetOverviewGroupTypeProgressResponseContract(userIds);
        }
       
        private async Task<OverviewGroupTypeProgressResponseContract> GetOverviewGroupTypeProgressResponseContract(IEnumerable<int> userIds)
        {
            var userIdList = userIds.ToList();

            var dashboardBarChartContracts = new List<DashboardBarChartContract>();

            _groups = await _filterAllowedGroups.GetAllLiveGroupsList();

            var groupIds = _groups.Select(g => g.GroupID).ToList();

            var groupTypes = _groups.OrderBy(g => g.GroupTypeID).Select(g => g.ltl_GroupType).Distinct();

            _certificatesAchieved =
                await _certificatesAchievedCommands.GetCertificatesAchievedForUsersAndGroups(userIdList, groupIds);// certsAchieved.ToListAsync();

            _attempts = await _attemptsCommands.GetAllAttemptsForUserIds(userIdList);

            _userIdsViewedGroupsGroupedByGroupId = await _userPostViewedCommands.GetUserPostsViewedGroupedByGroup(userIdList);

            foreach (var groupType in groupTypes)
            {
                var dashboardBarChartContract = GetDashboardBarChartContract(groupType, userIdList);

                dashboardBarChartContracts.Add(dashboardBarChartContract);
            }

            var overviewGroupTypeProgressResponseContract = new OverviewGroupTypeProgressResponseContract()
            {
                DashboardBarChartContracts = dashboardBarChartContracts
            };

            return overviewGroupTypeProgressResponseContract;

        }
        
        private DashboardBarChartContract GetDashboardBarChartContract(ltl_GroupType groupType, List<int> userIds)
        {
            var totalNumberOfRegisteredUsers = userIds.Count();

            var groupIdsForGroupType = _groups.Where(a => a.GroupTypeID == groupType.ID).Select(g => g.GroupID).ToList();

            var certified = _certificatesAchievedCommands.GetNumberOfUsersCertififedForGroupIds(groupIdsForGroupType, _certificatesAchieved);
            
            var percentageCertified = _commonCalculatorCommands.CalculatePercentages(certified, totalNumberOfRegisteredUsers);

            var selfAssessmentInProgress = _attemptsCommands.GetNumberOfUsersWithSelfAssessmentInProgressForGroupIds(groupIdsForGroupType, _attempts);
            
            var percentageselfAssessmentInProgressInProgress = _commonCalculatorCommands.CalculatePercentages(selfAssessmentInProgress, totalNumberOfRegisteredUsers);

            var userIdsWhoViewedGroup = _userIdsViewedGroupsGroupedByGroupId.Where(a => groupIdsForGroupType.Contains(a.Key)).SelectMany(group => group);

            var userStarted = userIdsWhoViewedGroup.Distinct().Count();
            
            var percentageStarted = _commonCalculatorCommands.CalculatePercentages(userStarted, totalNumberOfRegisteredUsers);

            return new DashboardBarChartContract
            {
                Title = groupType.Name,
                NumberOfUsersInProgress = selfAssessmentInProgress,
                NumberOfUsersCertified = certified,
                PercentageOfUsersCertified = percentageCertified,
                PercentageOfUsersInProgress = percentageselfAssessmentInProgressInProgress,
                NumberOfUsersStarted = userStarted,
                PercentageOfUsersStarted = percentageStarted
            };

        }
    }
}
