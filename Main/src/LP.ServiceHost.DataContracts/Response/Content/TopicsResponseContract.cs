using System.Collections.Generic;
using LP.ServiceHost.DataContracts.Common.Content;

namespace LP.ServiceHost.DataContracts.Response.Content
{
    public class TopicsResponseContract
    {
        public TopicsResponseContract()
        {
            Topics = new List<Topic>();
            TopicCategories = new List<TopicCategory>();
        }
        public List<Topic> Topics { get; set; }
        public List<TopicCategory> TopicCategories { get; set; }
    }
}
