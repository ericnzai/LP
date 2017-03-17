using System.Collections.Generic;
using LP.ServiceHost.DataContracts.Common.Content;

namespace LP.Model.ViewModels.Topic
{
    public class CategoryTopicsViewModel
    {
        public CategoryTopicsViewModel()
        {
            Items = new List<CategoryTopic>();
        }
        public List<CategoryTopic> Items { get; set; }
    }
}
