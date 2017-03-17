using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using LP.Api.Shared.Interfaces.BusinessLayer.Common;
using LP.Api.Shared.Interfaces.Data;
using LP.EntityModels;
using LP.ServiceHost.DataContracts.Enums;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.ServiceHost.Common.BusinessLayer.Commands
{
    public class GroupCommands : IGroupCommands
    {
        private readonly IBaseCommands _baseCommands;

        public GroupCommands(IBaseCommands baseCommands)
        {
            _baseCommands = baseCommands;
        }

        public async Task<Group> GetByIdAsync(int id)
        {
            return await _baseCommands.GetByIdAsync<Group>(id);
        }

        public async Task<Dictionary<int, bool>> AreLiveByIds(IEnumerable<int> groupIds)
        {
            var groups = await _baseCommands.GetAllAsync<Group>();

            var queriedGroups = groups.Where(g => groupIds.Contains(g.GroupID));
                
            return await  queriedGroups.ToDictionaryAsync(g => g.GroupID, g => g.StatusBankID == (int)Status.Live);
        }

        public async Task<GroupResponseContract> GetAllLiveGroupResponseContractsForGlossaryDropDown(string culture)
        {
            var groups = await _baseCommands.GetConditionalAsync<Group>(g => g.Culture == culture && g.StatusBankID == (int)Status.Live);

            var result = new GroupResponseContract();
            result.Groups.AddRange(groups.ToList().Select(g => new ServiceHost.DataContracts.Common.Content.Group()
            {
                Id = g.GroupID,
                Name = g.Name
            }));

            return result;
        }

        public async Task<GroupItemResponseContract> GetGroupByFeatureAttachmentIdGroups(int featureAttachmentId)
        {
            var featureAttachment = await _baseCommands.GetByIdWithIncludesAsync<ltl_FeatureAttachment>(f => f.FeatureAttachmentID == featureAttachmentId, f => f.ltl_Posts.ltl_Sections.ltl_Groups);

            var group = featureAttachment.ltl_Posts.ltl_Sections.ltl_Groups;

            //var result = new GroupItemResponseContract();
            //result.Groups.AddRange(groups.ToList().Where(g => g.Culture == culture && g.StatusBankID == 2).Select(g => new ServiceHost.DataContracts.Common.Content.Group()
            //{
            //    Id = g.GroupID,
            //    Name = g.Name
            //}));

            return new GroupItemResponseContract()
            {
                Name = group.Name,
                Description = group.Description,
                FriendlyUrl = group.FriendlyUrl,
                GroupId = group.GroupID,
                NewsgroupName = group.NewsgroupName,
                SortOrder = group.SortOrder,
                StatusBankID = group.StatusBankID,
                TrainingAreaID = group.TrainingAreaID
            };
        }
    }
}
