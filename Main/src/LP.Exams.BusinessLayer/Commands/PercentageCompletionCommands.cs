using System.Collections.Generic;
using System.Threading.Tasks;
using LP.Api.Shared.Interfaces.BusinessLayer.Common;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams;
using LP.Api.Shared.Interfaces.Data;
using LP.EntityModels;
using LP.ServiceHost.DataContracts.Common.Exams;

namespace LP.Exams.BusinessLayer.Commands
{
    public class PercentageCompletionCommands : IPercentageCompletionCommands
    {
        private readonly IExamCommands _examCommands;
        private readonly IAttemptsCommands _attemptsCommands;
        private readonly IBaseCommands _baseCommands;
        private readonly ICommonCalculatorCommands _commonCalculatorCommands;
        public PercentageCompletionCommands(IExamCommands examCommands, IAttemptsCommands attemptsCommands, IBaseCommands baseCommands, ICommonCalculatorCommands commonCalculatorCommands)
        {
            _examCommands = examCommands;
            _attemptsCommands = attemptsCommands;
            _baseCommands = baseCommands;
            _commonCalculatorCommands = commonCalculatorCommands;
        }

        public async Task<GroupPercentageComplete> PercentageAchievedForGroup(int userId, int groupId)
        {
            var totalExams = await _examCommands.GetExamIdsForGroup(groupId, true);

           

            var examsForUser = await _attemptsCommands.GetPassedAttemptsForUser(userId, totalExams);

            var group = await _baseCommands.GetByIdAsync<Group>(groupId);

            var percentageComplete = _commonCalculatorCommands.CalculatePercentages(examsForUser.Count, totalExams.Count);

            var groupPercentageComplete = new GroupPercentageComplete
            {
                GroupName = group.Name,
                PercentageComplete = percentageComplete,
                GroupId = groupId
            };

            return groupPercentageComplete;
        }

        public async Task<List<GroupPercentageComplete>> PercentageAchievedForGroups(int userId, IEnumerable<int> groupIds)
        {
            var groupsPercentageComplete = new List<GroupPercentageComplete>();

            foreach (var groupId in groupIds)
            {
                var groupPercentageComplete = await PercentageAchievedForGroup(userId, groupId);

                groupsPercentageComplete.Add(groupPercentageComplete);
            }

            return groupsPercentageComplete;
        }

        
    }
}
