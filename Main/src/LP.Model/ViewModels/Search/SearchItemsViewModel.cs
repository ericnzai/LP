using System.Collections.Generic;
using LP.ServiceHost.DataContracts.Common.Content;

namespace LP.Model.ViewModels.Search
{
    public class SearchItemsViewModel
    {
        public SearchItemsViewModel()
        {
            SearchItems = new List<SearchItem>();
        }

        public List<SearchItem> SearchItems { get; set; }
    }
}
