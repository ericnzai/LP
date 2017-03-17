using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LP.Api.Shared.Interfaces.BusinessLayer.Content;
using LP.Api.Shared.Interfaces.Data;
using LP.EntityModels.StoredProcedure.Input;
using LP.EntityModels.StoredProcedure.Output;
using LP.Model.Extensions;
using LP.ServiceHost.DataContracts.Common.Content;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.Content.BusinessLayer.Commands
{
    public class SearchCommands: ISearchCommands
    {
        private readonly IBaseCommands _baseCommands;

        public SearchCommands(IBaseCommands baseCommands)
        {
            _baseCommands = baseCommands;
        }

        public async Task<SearchItemsResponseContract> GetAllSearchItems(string culture, string search, List<int> roleIds, string groupId, string topicIds)
        {
            var userRolesStringBuilder = new StringBuilder();
            foreach (var roleId in roleIds)
            {
                userRolesStringBuilder.Append(string.Format("{0},", roleId));
            }
            if (userRolesStringBuilder.Length > 0)
            {
                userRolesStringBuilder.Remove(userRolesStringBuilder.Length - 1, 1);
            }



            var result = new SearchItemsResponseContract();

            var args = new ltl_SearchWithRowCountArguments { Culture = culture, SearchString = search, UserRoles = userRolesStringBuilder.ToString(), FromGroupID = -1, NotFromGroupID = -1, GroupID = groupId, TopicID = topicIds};
            
            var searchresult = _baseCommands.ExecuteStoredProcedure<SearchWithRowCount, ltl_SearchWithRowCountArguments>(args).ToList();
            result.SearchItems = new List<SearchItem>();
            foreach (var searchRes in searchresult)
            {
                result.SearchItems.Add(new SearchItem()
                {
                    Title = searchRes.Subject,
                    Description = searchRes.Body.StripHtmlTags().TruncateAtWord(250), 
                    GroupName = searchRes.GroupName, 
                    ParentSectionName = searchRes.ParentSectionName, 
                    Name = searchRes.Name, 
                    LastUpdated = searchRes.LastUpdated,
                    TrainingAreaFriendlyName = searchRes.TrainingAreaFriendlyName,
                    GroupFriendlyName = searchRes.GroupFriendlyName,
                    SectionFriendlyName = searchRes.SectionFriendlyName,
                    SortOrder = searchRes.SortOrder
                });
            }
            
            return result;
        }
    }
}
