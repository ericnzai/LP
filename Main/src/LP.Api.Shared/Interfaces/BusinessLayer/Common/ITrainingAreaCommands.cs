using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LP.EntityModels;
using LP.ServiceHost.DataContracts.Enums;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Common
{
    public interface ITrainingAreaCommands
    {
        Task<IEnumerable<TrainingArea>> GetLiveTrainingAreas();

        Task<IEnumerable<TrainingArea>> GetLiveTrainingAreasWithIncludes();

        Task<IQueryable<IGrouping<TrainingArea, IGrouping<ltl_GroupType, Group>>>> GetTrainingAreasWithAllGroupInfo(
            List<int> userRoleIds, AccessType accessType);

        Task<IGrouping<TrainingArea, IGrouping<ltl_GroupType, Group>>> GetTrainingAreaWithAllGroupInfo(
            int trainingAreaId, List<int> userRoleIds, AccessType accessType);

        Task<IQueryable<IGrouping<TrainingArea, IGrouping<ltl_GroupType, Group>>>> GetTrainingAreasWithAllGroupInfo(bool isLive = false);
    }
}
