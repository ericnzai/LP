using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LP.EntityModels;
using LP.EntityModels.Exam;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Exams
{
    public interface IAttemptsCommands
    {
        Task<List<short>> GetPassedAttemptsForUser(int userId);
        Task<List<short>> GetPassedAttemptsForUser(int userId, IEnumerable<short> examIds);
        Task<int> GetNumberOfUsersWithSelfAssessmentInProgressForGroupType(int groupTypeId);
        Task<int> GetNumberOfUsersWithSelfAssessmentInProgressForGroupTypeByRegion(int groupTypeId, List<int> regionUserIds);
        Task<List<short>> GetAttemptsForUser(int userId);
        Task<List<short>> GetAttemptsForUser(int userId, IEnumerable<short> examIds);
        Task<IQueryable<IGrouping<int, short>>> GetAttempedExamIdsGroupedByUserId(IEnumerable<int> userIds);
        Task<List<IGrouping<int, short>>> GetAttempedExamIdsGroupedByUserIdList(IEnumerable<int> userIds);
        Task<IQueryable<IGrouping<int, short>>> GetPassedExamIdsGroupedByUserId(IEnumerable<int> userIds);
        Task<List<IGrouping<int, short>>> GetPassedExamIdsGroupedByUserIdList(IEnumerable<int> userIds);
        Task<List<Attempt>> GetAllAttemptsForUserIds(IEnumerable<int> userIds);
        int GetNumberOfUsersWithSelfAssessmentInProgressForGroupIds(IEnumerable<int> groupIds, List<Attempt> attempts);
    }
}
