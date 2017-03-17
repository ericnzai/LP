using System.Collections.Generic;
using System.Threading.Tasks;
using LP.EntityModels;
using LP.ServiceHost.DataContracts.Response.Content;


namespace LP.Api.Shared.Interfaces.BusinessLayer.Common
{
    public interface IGroupCommands
    {
        Task<Group> GetByIdAsync(int id);
        Task<Dictionary<int, bool>> AreLiveByIds(IEnumerable<int> groupIds);
        Task<GroupResponseContract> GetAllLiveGroupResponseContractsForGlossaryDropDown(string culture);
        Task<GroupItemResponseContract> GetGroupByFeatureAttachmentIdGroups(int featureAttachmentId);
    }
}
