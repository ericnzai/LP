using System.Collections.Generic;
using System.Linq;
using LP.Model.ViewModels.TopicTranslation;
using LP.ServiceHost.DataContracts.Common.Content;

namespace LP.Model.Mappers
{
    public static class TopicTranslationContractEx
    {
        public static TopicTranslationViewModel ToViewModel(this TopicTranslationContract topicTranslation)
        {
            if (topicTranslation == null) return null;

            return new TopicTranslationViewModel()
            {
                TopicId = topicTranslation.TopicId,
                Culture = topicTranslation.Culture,
                IsTranslated = topicTranslation.IsTranslated,
                TopicName = topicTranslation.TopicName,
                LastUpdated = topicTranslation.LastUpdated,
                UpdatedByUserName = topicTranslation.UpdatedByUserName
            };
        }

        public static List<TopicTranslationViewModel> ToViewModels(
           this IEnumerable<TopicTranslationContract> topicTranslations)
        {
            var topicTranslationViewModels = new List<TopicTranslationViewModel>();

            foreach (var topicTranslation in topicTranslations)
            {
                topicTranslationViewModels.Add(topicTranslation.ToViewModel());
            }

            return topicTranslationViewModels;
        }
    }
}
