using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using LP.Api.Shared.Interfaces.BusinessLayer.Common;
using LP.Api.Shared.Interfaces.BusinessLayer.Common.Filters;
using LP.Api.Shared.Interfaces.Data;
using LP.EntityModels;
using LP.ServiceHost.DataContracts.Enums;

namespace LP.ServiceHost.Common.BusinessLayer.Commands
{
    public class TrainingAreaCommands : ITrainingAreaCommands
    {
        private readonly IBaseCommands _baseCommands;
        private readonly ITrainingGroupPermissionFilter _trainingGroupPermissionFilter;
        public TrainingAreaCommands(IBaseCommands baseCommands, ITrainingGroupPermissionFilter trainingGroupPermissionFilter)
        {
            _baseCommands = baseCommands;
            _trainingGroupPermissionFilter = trainingGroupPermissionFilter;
        }

        public async Task<IEnumerable<TrainingArea>> GetLiveTrainingAreas()
        {
            var trainingAreas = await _baseCommands.GetAllAsync<TrainingArea>();

            return trainingAreas.Where(a => a.StatusBankID == (int)Status.Live);
        }

        public async Task<IEnumerable<TrainingArea>> GetLiveTrainingAreasWithIncludes()
        {
            var trainingAreas = await _baseCommands.GetWithIncludesAsync<TrainingArea>(x => x.ltl_Groups, x => x.ltl_Groups.Select(a => a.ltl_GroupPermissions), x => x.ltl_Groups.Select(a => a.ltl_GroupType), x => x.TrainingAreaPermissions);

            return trainingAreas.Where(a => a.StatusBankID == (int)Status.Live);
        }

        public async Task<IGrouping<TrainingArea, IGrouping<ltl_GroupType, Group>>> GetTrainingAreaWithAllGroupInfo(int trainingAreaId, List<int> userRoleIds, AccessType accessType)
        {
            var trainingAreasWithAllGroupInfo = await GetTrainingAreasWithAllGroupInfo(userRoleIds, accessType);

            var groupsAndGroupTypesForTrainingArea = trainingAreasWithAllGroupInfo.FirstOrDefault(y => y.Key.TrainingAreaID == trainingAreaId);

            return groupsAndGroupTypesForTrainingArea;
        }

        public async Task<IQueryable<IGrouping<TrainingArea, IGrouping<ltl_GroupType, Group>>>> GetTrainingAreasWithAllGroupInfo(List<int> userRoleIds, AccessType accessType)
        {
            var allGroups =
                await
                    _baseCommands.GetWithIncludesAsync<Group>(g => g.ltl_GroupPermissions,
                        g => g.TrainingArea.TrainingAreaPermissions, g => g.ltl_Sections);

            var availableGroups = await _trainingGroupPermissionFilter.GetAvailableByStatusOfAsync<TrainingArea>(allGroups, userRoleIds, accessType);
            availableGroups = _trainingGroupPermissionFilter.GetAvailableByPermissionsOf<TrainingArea>(availableGroups, userRoleIds);

            availableGroups = await _trainingGroupPermissionFilter.GetAvailableByStatusOfAsync<Group>(availableGroups, userRoleIds, accessType);
            availableGroups = _trainingGroupPermissionFilter.GetAvailableByPermissionsOf<Group>(availableGroups, userRoleIds);

            return availableGroups
                .Include(g => g.ltl_GroupType.ltl_UsersFavouriteGroup)
                
                .GroupBy(gr => gr.TrainingArea)
                .SelectMany(
                    trainingAreaGrouping => (trainingAreaGrouping.GroupBy(gr => gr.ltl_GroupType)),
                    (trainingAreaGrouping, groupTypeGrouping) => new { trainingAreaGrouping, groupTypeGrouping })
                .GroupBy(t => t.trainingAreaGrouping.Key, t => t.groupTypeGrouping);

        }

        public async Task<IQueryable<IGrouping<TrainingArea, IGrouping<ltl_GroupType, Group>>>> GetTrainingAreasWithAllGroupInfo(bool isLive = false)
        {
            IQueryable<Group> groups;

            if (isLive)
            {
                groups = await _baseCommands.GetConditionalWithIncludesAsync<Group>(y => y.StatusBankID == (int)Status.Live, x => x.ltl_GroupPermissions, x => x.TrainingArea.TrainingAreaPermissions,
               x => x.ltl_Sections, x => x.ltl_GroupType.ltl_UsersFavouriteGroup);

            }
            else
            {
                groups = await _baseCommands.GetWithIncludesAsync<Group>(x => x.ltl_GroupPermissions, x => x.TrainingArea.TrainingAreaPermissions,
               x => x.ltl_Sections, x => x.ltl_GroupType.ltl_UsersFavouriteGroup);

            }

            //totest 
           
            return groups
                .GroupBy(gr => gr.TrainingArea)
                .SelectMany(
                    trainingAreaGrouping => (trainingAreaGrouping.GroupBy(gr => gr.ltl_GroupType)),
                    (trainingAreaGrouping, groupTypeGrouping) => new { trainingAreaGrouping, groupTypeGrouping })
                .GroupBy(t => t.trainingAreaGrouping.Key, t => t.groupTypeGrouping);

        }
    }
}
