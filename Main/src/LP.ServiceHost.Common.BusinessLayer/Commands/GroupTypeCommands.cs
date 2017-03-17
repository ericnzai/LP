using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using LP.Api.Shared.Interfaces.BusinessLayer.Common;
using LP.Api.Shared.Interfaces.BusinessLayer.Common.Commands;
using LP.Api.Shared.Interfaces.Data;
using LP.EntityModels;
using LP.EntityModels.Views;
using LP.Model.Extensions;
using LP.ServiceHost.DataContracts.Enums;

namespace LP.ServiceHost.Common.BusinessLayer.Commands
{
    public class GroupTypeCommands : IGroupTypeCommands
    {
        private readonly IBaseCommands _baseCommands;
        private readonly ITrainingAreaCommands _trainingAreaCommands;
        public GroupTypeCommands(IBaseCommands baseCommands, ITrainingAreaCommands trainingAreaCommands)
        {
            _baseCommands = baseCommands;
            _trainingAreaCommands = trainingAreaCommands;
        }

        public async Task<List<int>> GetGroupTypeIdsForUser(int userId)
        {
            var groupTypesWithUsers = await _baseCommands.GetConditionalAsync<GroupTypesWithUsers>(x => x.userId == userId);

            return await groupTypesWithUsers.Select(u => u.GroupTypeId).ToListAsync();
        }

        public async Task<IEnumerable<int>> GetAllAvailableGroupTypeIds()
        {
            var trainingAreas = await _trainingAreaCommands.GetLiveTrainingAreasWithIncludes();

            return trainingAreas.SelectMany(a => a.ltl_Groups.Where(s => s.StatusBankID == (int)Status.Live)
                .Select(gt => gt.GroupTypeID != null ? gt.GroupTypeID.Value : 0).Distinct());
        }

        public async Task<List<ltl_GroupType>> GetAllAvailableGroupTypes()
        {
            var trainingAreas = await _trainingAreaCommands.GetLiveTrainingAreasWithIncludes();

            var liveGroups = trainingAreas.SelectMany(a => a.ltl_Groups.Where(s => s.StatusBankID == (int)Status.Live));

            var groupTypes = liveGroups.Select(a => a.ltl_GroupType).DistinctBy(g => g.ID).OrderBy(g => g.SortOrder);

            return groupTypes.ToList();
        }
    }
}
