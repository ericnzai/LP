using System.Collections.Generic;
using LP.EntityModels;

namespace LP.Model.ViewModels.TopicTranslation
{
    public class CompleteTopicCategoryTranslationViewModel
    {
        public CompleteTopicCategoryTranslationViewModel()
        {
            TopicCategoryTranslations = new List<TopicCategoryTranslationViewModel>();
        }

        public List<TopicCategoryTranslationViewModel> TopicCategoryTranslations { get; set; }
    }
}
