using System;
using System.Threading.Tasks;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.Api.Shared.Interfaces.BusinessLayer.Content
{
    public interface ITopicCommands
    {
        Task<TopicCategoriesResponseContract> GetAllTopicCategories(string culture);
        Task<TopicCategoryResponseContract> AddTopicCategory(string culture, string name);
        Task<TopicCategoryDeleteResponseContract> DeleteTopicCategory(string culture, int id);
        Task<TopicCategoryUpdateResponseContract> UpdateTopicCategory(string culture, int id, string name);
        Task<TopicsResponseContract> GetAllTopics(string culture);
        Task<TopicResponseContract> AddTopic(string culture, string name, int categoryId, DateTime lastUpdated, int userId);
        Task<TopicDeleteResponseContract> DeleteTopic(string culture, int id);
        Task<TopicUpdateResponseContract> UpdateTopic(string culture, int id, string name, int categoryId);
        Task<TopicUpdateResponseContract> SavePostTopics(string topics, string postId);
        Task<PostTopicResponseContract> GetPostTopics(string culture, int postId);
        Task<PostTopicResponseContract> GetSearchTopics(string culture);
    }
}
