using System.Collections.Generic;
using LP.ServiceHost.DataContracts.Common.Content;

namespace LP.ServiceHost.DataContracts.Response.Content
{
    public class SearchItemsResponseContract
    {
        public SearchItemsResponseContract()
        {
            SearchItems = new List<SearchItem>();
        }
        public List<SearchItem> SearchItems { get; set; }
    }
}
