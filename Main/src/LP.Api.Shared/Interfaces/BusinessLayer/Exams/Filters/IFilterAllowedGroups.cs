using LP.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Exams.Filters
{
    public interface IFilterAllowedGroups
    {
        Task<IQueryable<Group>> GetAllLiveGroups();
        Task<List<Group>> GetAllLiveGroupsList();
        Task<IEnumerable<int>> GetAllLiveGroupIdsByGroupType(int groupTypeId);
    }
}
