using System.Collections.Generic;
using LP.ServiceHost.DataContracts.Common.Content;

namespace LP.Model.ViewModels.Topic
{
    public class TopicCategoriesViewModel
    {
        public TopicCategoriesViewModel()
        {
            TopicCategories = new List<TopicCategory>();
        }

        public List<TopicCategory> TopicCategories { get; set; }
    }
}
