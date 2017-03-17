using LP.ServiceHost.DataContracts.Common.Content;

namespace LP.Model.ViewModels.TopicTranslation
{
    public class TopicTranslationFormViewModel
    {
        public TopicTranslationContract GlobalTopicTranslation { get; set; }
        public TopicTranslationContract LocalTopicTranslation { get; set; }
    }
}
