using System.Collections.Generic;
using LP.ServiceHost.DataContracts.Common.Content;

namespace LP.ServiceHost.DataContracts.Response.Content
{
    public class PostTopicResponseContract
    {
        public PostTopicResponseContract()
        {
            CategoryTopics = new List<CategoryTopic>();
        }
        public List<CategoryTopic> CategoryTopics { get; set; }
    }
}
