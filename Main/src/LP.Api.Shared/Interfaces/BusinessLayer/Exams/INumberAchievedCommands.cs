using System.Collections.Generic;
using System.Threading.Tasks;
using LP.Model.Authentication;
using LP.ServiceHost.DataContracts.Common.Exams;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Exams
{
    public interface INumberAchievedCommands
    {
        Task<List<TrainingAreaCompletion>> NumberOfModulesCompletedForAllTrainingAreasAsync(UserDetails userDetails);
    }
}
