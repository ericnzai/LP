using LP.ServiceHost.DataContracts.Common.Content;

namespace LP.ServiceHost.DataContracts.Response.Content
{
    public class TopicCategoryTranslationFormResponseContract
    {
        public TopicCategoryTranslationFormResponseContract()
        {
            LocalCategoryTranslation = new TopicCategoryTranslationContract();
            GlobalTopicCategoryTranslation = new TopicCategoryTranslationContract();
        }
        public TopicCategoryTranslationContract LocalCategoryTranslation { get; set; }
        public TopicCategoryTranslationContract GlobalTopicCategoryTranslation { get; set; }

        
    }
}
