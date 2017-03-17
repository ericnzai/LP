using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LP.Api.Shared.Interfaces.BusinessLayer.Common;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams;
using LP.Api.Shared.Interfaces.Data;
using LP.EntityModels.Exam;
using LP.Model.Authentication;
using LP.ServiceHost.DataContracts.Common.Exams;
using LP.ServiceHost.DataContracts.Enums;

namespace LP.Exams.BusinessLayer.Commands
{
    public class NumberAchievedCommands : INumberAchievedCommands
    {
        private readonly IGroupPermissionCommands _groupPermissionCommands;
        private readonly ITrainingAreaCommands _trainingAreaCommands;
        private readonly IPercentageCompletionCommands _percentageCompletionCommands;
        public NumberAchievedCommands(IGroupPermissionCommands groupPermissionCommands, ITrainingAreaCommands trainingAreaCommands, 
                                      IPercentageCompletionCommands percentageCompletionCommands)
        {
            _groupPermissionCommands = groupPermissionCommands;
            _trainingAreaCommands = trainingAreaCommands;
            _percentageCompletionCommands = percentageCompletionCommands;
        }

        public async Task<List<TrainingAreaCompletion>> NumberOfModulesCompletedForAllTrainingAreasAsync(UserDetails userDetails)
        {
            var trainingAreas = await _trainingAreaCommands.GetLiveTrainingAreas();

            var trainingAreaCompletions = new List<TrainingAreaCompletion>();

            foreach (var trainingArea in trainingAreas)
            {
                var trainingAreaCompletion =
                    await NumberOfModulesCompletedForTrainingArea(userDetails, trainingArea.TrainingAreaID);

                trainingAreaCompletions.Add(trainingAreaCompletion);
            }

            return trainingAreaCompletions;
        }

        private async Task<TrainingAreaCompletion> NumberOfModulesCompletedForTrainingArea(UserDetails userDetails, int trainingAreaId)
        {
            var groups = await  _groupPermissionCommands.GroupsWithPermissionsForRolesForTrainingArea(userDetails.RoleIds,
                trainingAreaId, userDetails.AvailableStatuses);

            var groupIds = groups.Where(a => a.StatusBankID == (int)Status.Live).Select(g => g.GroupID);

            var totalNumberOfGroups = groupIds.Count();

            var groupsPercentageCompletes = await _percentageCompletionCommands.PercentageAchievedForGroups(userDetails.UserId, groupIds);
            var groupsPercentageComplete = groupsPercentageCompletes.Where(g => g.PercentageComplete == 100);

            var trainingAreaCompletion = new TrainingAreaCompletion
            {
                TrainingAreaId = trainingAreaId,
                TotalNumberOfModules = totalNumberOfGroups,
                NumberOfModulesComplete = groupsPercentageComplete.Count()
            };

            return trainingAreaCompletion;
        }
    }
}
