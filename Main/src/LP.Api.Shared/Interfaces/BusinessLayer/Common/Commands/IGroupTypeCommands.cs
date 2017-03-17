using System.Collections.Generic;
using System.Threading.Tasks;
using LP.EntityModels;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Common.Commands
{
    public interface IGroupTypeCommands
    {
        Task<List<int>> GetGroupTypeIdsForUser(int userId);
        Task<IEnumerable<int>> GetAllAvailableGroupTypeIds();
        Task<List<ltl_GroupType>> GetAllAvailableGroupTypes();
    }
}
