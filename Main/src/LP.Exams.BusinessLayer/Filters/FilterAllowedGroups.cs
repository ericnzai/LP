using System.Data.Entity;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams.Filters;
using LP.Api.Shared.Interfaces.Data;
using LP.EntityModels;
using LP.ServiceHost.DataContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP.Exams.BusinessLayer.Filters
{
    public class FilterAllowedGroups : IFilterAllowedGroups
    {
        private readonly IBaseCommands _baseCommands;

        public FilterAllowedGroups(IBaseCommands baseCommands)
        {
            _baseCommands = baseCommands;
        }

        public async Task<IQueryable<Group>> GetAllLiveGroups()
        {
            var groups = await _baseCommands.GetWithIncludesAsync<Group>(x => x.ltl_GroupType);
            return groups.Where(g => g.StatusBankID == (int)Status.Live);
        }

        public async Task<List<Group>> GetAllLiveGroupsList()
        {
            var groups = await GetAllLiveGroups();
            return await groups.ToListAsync();
        }

        public async Task<IEnumerable<int>> GetAllLiveGroupIdsByGroupType(int groupTypeId)
        {
            var groups = await GetAllLiveGroups();


            var filteredGroups = groups.Where(g => g.GroupTypeID == groupTypeId).Select(g => g.GroupID);

            return filteredGroups;
        }
    }
}
