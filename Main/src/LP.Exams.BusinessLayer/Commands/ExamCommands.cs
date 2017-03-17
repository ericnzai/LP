using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams;
using LP.Api.Shared.Interfaces.Data;
using LP.EntityModels.Exam;
using LP.ServiceHost.DataContracts.Enums;

namespace LP.Exams.BusinessLayer.Commands
{
    public class ExamCommands : IExamCommands
    {
        private readonly IBaseCommands _baseCommands;

        public ExamCommands(IBaseCommands baseCommands)
        {
            _baseCommands = baseCommands;
        }

        public async Task<List<short>> GetExamIdsForGroup(int groupId, bool onlyLive = false)
        {
            var trainingsExam = await _baseCommands.GetConditionalWithIncludesAsync<TrainingsExam>(a => a.GroupId == groupId, ex => ex.Exam);

            if (onlyLive)
            {
                return await trainingsExam.Where(a => a.Exam.StatusId == (int)Status.Live).Select(a => a.ExamId).Distinct().ToListAsync();
            }

            return await trainingsExam.Select(a => a.ExamId).Distinct().ToListAsync();
        }

        public async Task<List<short>> GetExamIdsForGroups(IEnumerable<int> groupIds, bool onlyLive = false)
        {
            var trainingsExamForGroups = await _baseCommands.GetConditionalWithIncludesAsync<TrainingsExam>(x => groupIds.Contains(x.GroupId), inc => inc.Exam);

            if (onlyLive)
            {
                return await trainingsExamForGroups.Where(a => a.Exam.StatusId == (int)Status.Live).Select(a => a.ExamId).Distinct().ToListAsync();
            }
            
            return await trainingsExamForGroups.Select(a => a.ExamId).Distinct().ToListAsync();
        }
    }
}
