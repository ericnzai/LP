using LP.ServiceHost.DataContracts.Common.Content;
using System.Collections.Generic;

namespace LP.ServiceHost.DataContracts.Response.Content
{
    public class CompleteTopicTranslationResponseContract
    {
        public CompleteTopicTranslationResponseContract()
        {
            TopicTranslations = new List<TopicTranslationContract>();
        }

        public List<TopicTranslationContract> TopicTranslations { get; set; }
    }
}
