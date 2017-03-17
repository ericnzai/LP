using System.Collections.Generic;
using System.Linq;
using LP.Model.ViewModels.TopicTranslation;
using LP.ServiceHost.DataContracts.Common.Content;

namespace LP.Model.Mappers
{
    public static class TopicCategoryTranslationContractEx
    {
        public static TopicCategoryTranslationViewModel ToViewModel(
            this TopicCategoryTranslationContract topicCategoryTranslation)
        {

            return new TopicCategoryTranslationViewModel
            {
                TopicCategoryId = topicCategoryTranslation.TopicCategoryId,
                Status = topicCategoryTranslation.Status,
                Culture = topicCategoryTranslation.Culture,
                IsTranslated = topicCategoryTranslation.IsTranslated,
                TopicCategoryName = topicCategoryTranslation.TopicCategoryName,
                LastUpdated = topicCategoryTranslation.LastUpdated,
                UpdatedByUserName = topicCategoryTranslation.UpdatedByUserName,
                SortOrder = topicCategoryTranslation.SortOrder,
                TopicTranslations = topicCategoryTranslation.TopicTranslations.ToViewModels()
            };
        }

        public static List<TopicCategoryTranslationViewModel> ToViewModels(
           this IEnumerable<TopicCategoryTranslationContract> topicCategoryTranslations)
        {
            return topicCategoryTranslations.Select(topicCategoryTranslation => topicCategoryTranslation.ToViewModel()).ToList();
        }
    }
}
