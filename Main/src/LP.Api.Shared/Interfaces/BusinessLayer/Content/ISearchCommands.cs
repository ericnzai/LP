using System.Collections.Generic;
using System.Threading.Tasks;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Content
{
    public interface ISearchCommands
    {
        Task<SearchItemsResponseContract> GetAllSearchItems(string culture, string search, List<int> roleIds, string groupTypeId, string topicIds);
    }
}
