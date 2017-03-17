using System.Collections.Generic;

namespace LP.Model.ViewModels.TopicTranslation
{
    public class CompleteTopicTranslationViewModel
    {
        public CompleteTopicTranslationViewModel()
        {
            TopicTranslations = new List<TopicTranslationViewModel>();
        }

        public List<TopicTranslationViewModel> TopicTranslations { get; set; }
    }
}
