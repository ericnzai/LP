using LP.ServiceHost.DataContracts.Common.Content;

namespace LP.ServiceHost.DataContracts.Response.Content
{
    public class TopicTranslationFormResponseContract
    {
        public TopicTranslationFormResponseContract()
        {
            LocalTopicTranslation = new TopicTranslationContract();
            GlobalTopicTranslation = new TopicTranslationContract();
        }

        public TopicTranslationContract LocalTopicTranslation { get; set; }
        public TopicTranslationContract GlobalTopicTranslation { get; set; }
    }
}
