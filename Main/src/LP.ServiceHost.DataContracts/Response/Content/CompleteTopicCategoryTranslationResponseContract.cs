using System.Collections.Generic;
using LP.ServiceHost.DataContracts.Common.Content;

namespace LP.ServiceHost.DataContracts.Response.Content
{
    public class CompleteTopicCategoryTranslationResponseContract
    {
        public CompleteTopicCategoryTranslationResponseContract()
        {
            TopicCategoryTranslations = new List<TopicCategoryTranslationContract>();
        }
        public List<TopicCategoryTranslationContract> TopicCategoryTranslations { get; set; }
    }
}
