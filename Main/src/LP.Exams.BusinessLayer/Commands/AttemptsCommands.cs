using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams;
using LP.Api.Shared.Interfaces.Data;
using LP.EntityModels.Exam;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams.Filters;

namespace LP.Exams.BusinessLayer.Commands
{
    public class AttemptsCommands : IAttemptsCommands
    {
        private readonly IBaseCommands _baseCommands;
        private readonly IFilterAllowedUser _filterAllowedUser;
        private readonly IFilterAllowedGroups _filterAllowedGroups;

        public AttemptsCommands(IBaseCommands baseCommands, IFilterAllowedUser allowedUserFilter, IFilterAllowedGroups filterAllowedGroups)
        {
            _baseCommands = baseCommands;
            _filterAllowedUser = allowedUserFilter;
            _filterAllowedGroups = filterAllowedGroups;
        }

        private async Task<IQueryable<Attempt>> GetAllAttemptsPassedForUser(int userId)
        {
            var attempts = await _baseCommands.GetAllAsync<Attempt>();

            return attempts.Where(u => u.UserId == userId && u.AttemptPassed);
        }

        public async Task<List<short>> GetPassedAttemptsForUser(int userId)
        {
            var attemptsPassed = await GetAllAttemptsPassedForUser(userId);

            return attemptsPassed.Select(a => a.ExamId).Distinct().ToList();
        }

        public async Task<List<short>> GetPassedAttemptsForUser(int userId, IEnumerable<short> examIds)
        {
            var attemptsPassed = await GetAllAttemptsPassedForUser(userId);

            return attemptsPassed.Where(a => examIds.Contains(a.ExamId)).Select(a => a.ExamId).Distinct().ToList();
        }

        public async Task<int> GetNumberOfUsersWithSelfAssessmentInProgressForGroupType(int groupTypeId)
        {
            var queriedGroupIds = await _filterAllowedGroups.GetAllLiveGroupIdsByGroupType(groupTypeId);
            var userIds = await _filterAllowedUser.GetAllLiveUsersNotHiddenFromReportsIds();
            var attempts = await _baseCommands.GetAllAsync<Attempt>();

            return attempts.Where(g => queriedGroupIds.Contains(g.GroupId) && userIds.Contains(g.UserId)).Select(a => a.UserId).Distinct().Count();
        }

        //totest
        public async Task<int> GetNumberOfUsersWithSelfAssessmentInProgressForGroupTypeByRegion(int groupTypeId, List<int> regionUserIds)
        {
            var queriedGroupIds = await _filterAllowedGroups.GetAllLiveGroupIdsByGroupType(groupTypeId);
            var attempts = await _baseCommands.GetAllAsync<Attempt>();

            return attempts.Where(g => queriedGroupIds.Contains(g.GroupId) && regionUserIds.Contains(g.UserId)).Select(a => a.UserId).Distinct().Count();
        }

        public async Task<List<Attempt>> GetAllAttemptsForUserIds(IEnumerable<int> userIds)
        {
            var attempts = await _baseCommands.GetConditionalAsync<Attempt>(x => userIds.Contains(x.UserId));

            return await attempts.ToListAsync();
        }

        public int GetNumberOfUsersWithSelfAssessmentInProgressForGroupIds(IEnumerable<int> groupIds, List<Attempt> attempts)
        {
            //var queriedGroupIds = await _filterAllowedGroups.GetAllLiveGroupIdsByGroupType(groupTypeId);
            //var attempts = await _baseCommands.GetAllAsync<Attempt>();

            return attempts.Where(g => groupIds.Contains(g.GroupId)).Select(a => a.UserId).Distinct().Count();
        }

        private async Task<IQueryable<Attempt>> GetAllAttemptsForUser(int userId)
        {
            var attempts = await _baseCommands.GetAllAsync<Attempt>();

            return attempts.Where(u => u.UserId == userId);
        }

        public async Task<List<short>> GetAttemptsForUser(int userId)
        {
            var attempts = await GetAllAttemptsForUser(userId);

            return attempts.Select(a => a.ExamId).Distinct().ToList();
        }

        public async Task<List<short>> GetAttemptsForUser(int userId, IEnumerable<short> examIds)
        {
            var attempts = await GetAllAttemptsForUser(userId);

            return await attempts.Where(a => examIds.Contains(a.ExamId)).Select(a => a.ExamId).Distinct().ToListAsync();
        }

        public async Task<IQueryable<IGrouping<int, short>>> GetAttempedExamIdsGroupedByUserId(IEnumerable<int> userIds)
        {
            var allAttempts = await _baseCommands.GetConditionalAsync<Attempt>(a => userIds.Contains(a.UserId));

            return allAttempts.Select(m => new { m.UserId, m.ExamId })
                .Distinct().GroupBy(x => x.UserId, x => x.ExamId);
        }

        public async Task<List<IGrouping<int, short>>> GetAttempedExamIdsGroupedByUserIdList(IEnumerable<int> userIds)
        {
            var groupedExamAttempts = await GetAttempedExamIdsGroupedByUserId(userIds);

            return await groupedExamAttempts.ToListAsync();
        }

        public async Task<IQueryable<IGrouping<int, short>>> GetPassedExamIdsGroupedByUserId(IEnumerable<int> userIds)
        {
            var allAttempts = await _baseCommands.GetConditionalAsync<Attempt>(a => userIds.Contains(a.UserId) && a.AttemptPassed);

            return allAttempts.Select(m => new { m.UserId, m.ExamId })
                .Distinct().GroupBy(x => x.UserId, x => x.ExamId);
        }

        public async Task<List<IGrouping<int, short>>> GetPassedExamIdsGroupedByUserIdList(IEnumerable<int> userIds)
        {
            var allAttempts = await GetPassedExamIdsGroupedByUserId(userIds);
            return await allAttempts.ToListAsync();
        }
    }
}
