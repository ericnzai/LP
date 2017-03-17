using System.Collections.Generic;
using LP.ServiceHost.DataContracts.Common.Content;

namespace LP.ServiceHost.DataContracts.Response.Content
{
    public class TopicCategoriesResponseContract
    {
        public TopicCategoriesResponseContract()
        {
            TopicCategories = new List<TopicCategory>();
        }
        public List<TopicCategory> TopicCategories { get; set; }
    }
}