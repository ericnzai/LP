using LP.ServiceHost.DataContracts.Enums;
using LP.ServiceHost.DataContracts.Response.Content;
using System.Threading.Tasks;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Content
{
    public interface ITopicTranslationCommands
    {
        Task<CompleteTopicCategoryTranslationResponseContract> GetAllTopicCategories(string culture);
        Task<CompleteTopicTranslationResponseContract> GetAllTopics(string culture);
        Task<TopicCategoryTranslationFormResponseContract> GetTopicCategoryByIdAndCulture(int topicCategoryId, string culture);
        Task<TopicTranslationFormResponseContract> GetTopicByIdAndCulture(int topicId, string culture);

        Task<TopicCategoryTranslationResponseContract> AddTopicCategoryTranslation(string culture, string name, int topicCategoryId,
            int userId, Status status);

        Task<TopicCategoryTranslationUpdateResponseContract> UpdateTopicCategoryTranslation(string culture, string name,
            int topicCategoryId, int userId, Status status);

        Task<TopicTranslationResponseContract> AddTopicTranslation(string culture, string name, int topicId, int userId,
            Status status);

        Task<TopicTranslationUpdateResponseContract> UpdateTopicTranslation(string culture, string name, int topicId, int userId,
            Status status);
    }
}
