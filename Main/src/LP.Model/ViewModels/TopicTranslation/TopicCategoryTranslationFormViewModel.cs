using LP.ServiceHost.DataContracts.Common.Content;

namespace LP.Model.ViewModels.TopicTranslation
{
    public class TopicCategoryTranslationFormViewModel
    {
        public TopicCategoryTranslationContract GlobalTopicCategoryTranslation { get; set; }
        public TopicCategoryTranslationContract LocalTopicCategoryTranslation { get; set; }
    }
}
