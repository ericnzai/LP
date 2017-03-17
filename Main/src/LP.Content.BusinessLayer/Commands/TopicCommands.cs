using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iTextSharp.text;
using LP.Api.Shared.Interfaces.BusinessLayer.Content;
using LP.Api.Shared.Interfaces.Data;
using LP.EntityModels;
using LP.ServiceHost.DataContracts.Common.Content;
using LP.ServiceHost.DataContracts.Enums;
using LP.ServiceHost.DataContracts.Response.Content;
using Topic = LP.EntityModels.Topic;
using TopicCategory = LP.EntityModels.TopicCategory;
using TopicCategoryTranslation = LP.EntityModels.TopicCategoryTranslation;
using TopicTranslation = LP.EntityModels.TopicTranslation;

namespace LP.Content.BusinessLayer.Commands
{
    public class TopicCommands : ITopicCommands
    {
        private readonly IBaseCommands _baseCommands;

        public TopicCommands(IBaseCommands baseCommands)
        {
            _baseCommands = baseCommands;
        }

        public async Task<TopicCategoriesResponseContract> GetAllTopicCategories(string culture)
        {
            var topicCategories = await _baseCommands.GetAllAsync<TopicCategoryTranslation>();

            var result = new TopicCategoriesResponseContract();
            
            result.TopicCategories.AddRange(topicCategories.Where(t => t.Status != Status.Deleted && t.Culture == culture).Select(t => new LP.ServiceHost.DataContracts.Common.Content.TopicCategory
            {
                CategoryName = t.Name,
                CategoryId = t.TopicCategoryId,
                SortOrder = t.TopicCategory.SortOrder
            }));

            return result;
        }

        public async Task<TopicCategoryResponseContract> AddTopicCategory(string culture, string name)
        {
            if (CategoryNameAlreadyExists(0, name, culture).Result) return new TopicCategoryResponseContract { Result = false };
            
            var topicCategories = await _baseCommands.GetAllAsync<TopicCategoryTranslation>();

            var sortOrder = topicCategories.Count(t => t.Culture == culture);

            // add record to TopicCategory
            var entity = new TopicCategory() { SortOrder = sortOrder + 1, CreatedByUserId = 1, Status = Status.Live};

            _baseCommands.Add(entity);

            _baseCommands.SaveChanges();

            var topicCategoryId = entity.TopicCategoryId;

            // add record to TopicCategoryTranslation
            var entityTranslation = new TopicCategoryTranslation() { Name = name, Culture = culture, LastUpdatedByUserId = 1, Status = Status.Live, TopicCategoryId = topicCategoryId, LastUpdated = DateTime.Now};
            
            _baseCommands.Add(entityTranslation);
            
            _baseCommands.SaveChanges();

            var result = new TopicCategoryResponseContract {Result = true};

            return result;
        }

        public async Task<TopicCategoryDeleteResponseContract> DeleteTopicCategory(string culture, int id)
        {
            var topicCategory = await _baseCommands.GetByIdAsync<TopicCategory>(id);

            var topicCategoryTranslations = await _baseCommands.GetAllAsync<TopicCategoryTranslation>();
            var topicCategoryTranslationsList = topicCategoryTranslations.Where(t => t.TopicCategoryId == id);

            if (topicCategory == null) return new TopicCategoryDeleteResponseContract { Result = false };
            topicCategory.Status = Status.Deleted;

            _baseCommands.Update(topicCategory);
            _baseCommands.SaveChanges();

            // update topic translations
            foreach (var topicCategoryTranslation in topicCategoryTranslationsList)
            {
                topicCategoryTranslation.Status = Status.Deleted;

                _baseCommands.Update(topicCategoryTranslation);
            }

            _baseCommands.SaveChanges();

            var result = new TopicCategoryDeleteResponseContract {Result = true};

            return result;
        }

        public async Task<TopicCategoryUpdateResponseContract> UpdateTopicCategory(string culture, int id, string name)
        {
            if (CategoryNameAlreadyExists(id, name, culture).Result) return new TopicCategoryUpdateResponseContract { Result = false };
            var topicCategories = await _baseCommands.GetAllAsync<TopicCategoryTranslation>();
            var topicCategory = topicCategories.FirstOrDefault(t => t.Culture == culture && t.TopicCategoryId == id);

            if (topicCategory == null) return new TopicCategoryUpdateResponseContract { Result = false };
            topicCategory.Name = name;
            topicCategory.LastUpdated = DateTime.UtcNow;

            _baseCommands.Update(topicCategory);
            _baseCommands.SaveChanges();

            var result = new TopicCategoryUpdateResponseContract {Result = true};

            return result;
        }

        public async Task<bool> CategoryNameAlreadyExists(int id, string name, string culture)
        {
            var topicCategories = await _baseCommands.GetAllAsync<TopicCategoryTranslation>();

            var result = topicCategories.Any(t => t.Status != Status.Deleted && t.Name == name && t.Culture == culture && t.TopicCategoryId != id);

            return result;
        } 

        public async Task<TopicsResponseContract> GetAllTopics(string culture)
        {
            var topics = await _baseCommands.GetAllAsync<TopicTranslation>();
            var topicCategories = await _baseCommands.GetAllAsync<TopicCategoryTranslation>();

            var result = new TopicsResponseContract();

            var topicsForCulture = topics.Where(t => t.Culture == culture && t.Status != Status.Deleted);
            
            var topicContracts =
                topicsForCulture
                    .Select(t => new LP.ServiceHost.DataContracts.Common.Content.Topic
                    {
                        TopicId = t.TopicId,
                        TopicName = t.Name,
                        CategoryId = t.Topic.TopicCategoryId,
                        TopicCategoryName = topicCategories.FirstOrDefault(c => c.TopicCategoryId == t.Topic.TopicCategoryId && c.Culture == culture).Name,
                        SortOrder = t.Topic.SortOrder,
                    });

            var topicCategoriesForCulture = topicCategories.Where(t => t.Culture == culture && t.Status != Status.Deleted);
            var topicCategoriesContracts =
                topicCategoriesForCulture.Select(c => new LP.ServiceHost.DataContracts.Common.Content.TopicCategory()
                {
                    CategoryId = c.TopicCategoryId,
                    CategoryName = c.Name,
                    SortOrder = c.TopicCategory.SortOrder
                }).ToList();

            result.Topics.AddRange(topicContracts);
            result.TopicCategories.AddRange(topicCategoriesContracts);

            return result;
        }

        public async Task<TopicResponseContract> AddTopic(string culture, string name, int categoryId, DateTime lastUpdated, int userId)
        {
            if (TopicNameAndCategoryCombinationAlreadyExists(0, name, categoryId, culture).Result) return new TopicResponseContract { Result = false };
            var topics = await _baseCommands.GetAllAsync<TopicTranslation>();

            // add topic first
            var sortOrder = topics.Count(t => t.Culture == culture && t.Topic.TopicCategoryId == categoryId);

            var entity = new Topic()
            {
                SortOrder = sortOrder,
                TopicCategoryId = categoryId,
                DateCreated = DateTime.UtcNow,
                CreatedByUserId = 1,
                Status = Status.Live,
            };

            _baseCommands.Add(entity);
            _baseCommands.SaveChanges();

            var topicId = entity.TopicId;

            var entityTranslation = new TopicTranslation()
            {
                Name = name,
                Culture = culture,
                LastUpdated = lastUpdated,
                Status = Status.Live,
                LastUpdatedByUserId = 1,
                TopicId = topicId
                
            };

            _baseCommands.Add(entityTranslation);
            _baseCommands.SaveChanges();

            var result = new TopicResponseContract { Result = true };

            return result;
        }

        public async Task<TopicDeleteResponseContract> DeleteTopic(string culture, int id)
        {
            var topic = await _baseCommands.GetByIdAsync<Topic>(id);

            var topicTranslations = await _baseCommands.GetAllAsync<TopicTranslation>();
            var topicTranslationsList = topicTranslations.Where(t => t.TopicId == id);

            // update topic
            if (topic == null) return new TopicDeleteResponseContract { Result = false };
            topic.Status = Status.Deleted;

            _baseCommands.Update(topic);
            _baseCommands.SaveChanges();

            // update topic translations
            foreach (var topicTranslation in topicTranslationsList)
            {
                topicTranslation.Status = Status.Deleted;

                _baseCommands.Update(topicTranslation);
            }

            _baseCommands.SaveChanges();

            var result = new TopicDeleteResponseContract { Result = true };

            return result;
        }

        public async Task<TopicUpdateResponseContract> UpdateTopic(string culture, int id, string name, int categoryId)
        {
            if (TopicNameAndCategoryCombinationAlreadyExists(id, name, categoryId, culture).Result) return new TopicUpdateResponseContract { Result = false };
            var topic = await _baseCommands.GetByIdAsync<Topic>(id);

            // update topic translation
            var topicTranslations = await _baseCommands.GetAllAsync<TopicTranslation>();
            var topicTranslation = topicTranslations.FirstOrDefault(t => t.Culture == culture && t.TopicId == id);

            if (topic == null || topicTranslation == null) return new TopicUpdateResponseContract { Result = false };
            topicTranslation.Name = name;
            topicTranslation.LastUpdated = DateTime.UtcNow;

            _baseCommands.Update(topicTranslation);
            _baseCommands.SaveChanges();

            // update topic
            topic.TopicCategoryId = categoryId;
            
            _baseCommands.Update(topic);
            _baseCommands.SaveChanges();

            var result = new TopicUpdateResponseContract { Result = true };

            return result;
        }

        public async Task<bool> TopicNameAndCategoryCombinationAlreadyExists(int id, string name, int categoryId, string culture)
        {
            var topics = await _baseCommands.GetAllAsync<TopicTranslation>();

            var result = topics.Any(t => t.Status != Status.Deleted && t.Name == name && t.Culture == culture && t.TopicId != id);

            return result;
        }

        public async Task<TopicUpdateResponseContract> SavePostTopics(string topics, string postId)
        {
            int postID = 0;
            
            if (!int.TryParse(postId, out postID) || string.IsNullOrEmpty(topics)) return new TopicUpdateResponseContract { Result = false };
            var topicIds = topics.Split(',').ToList();
            var post = await _baseCommands.GetByIdWithIncludesAsync<ltl_Posts>(y=>y.PostID == postID, x => x.ltl_Topics);

            post.ltl_Topics.Clear();
            
            _baseCommands.SaveChanges();

            foreach (var topicId in topicIds)
            {
                int id = 0;
                if (int.TryParse(topicId, out id))
                {
                    var topic = _baseCommands.GetById<Topic>(id);
                    if (topic != null)
                    {
                        post.ltl_Topics.Add(topic);
                    }
                }
            }

            _baseCommands.SaveChanges();

            var result = new TopicUpdateResponseContract { Result = true };

            return result;
        }

        public async Task<PostTopicResponseContract> GetPostTopics(string culture, int postId)
        {
            var topics = await _baseCommands.GetAllAsync<TopicTranslation>();
            var topicCategories = await _baseCommands.GetAllAsync<TopicCategoryTranslation>();
            var post = await _baseCommands.GetByIdWithIncludesAsync<ltl_Posts>(y=>y.PostID == postId, x => x.ltl_Topics);

            var result = new PostTopicResponseContract();
            if (post == null)
            {
                return result;
            }

            var topicsForCulture = topics.Where(t => t.Culture == culture && t.Status != Status.Deleted);

            var topicContracts =
                topicsForCulture
                    .Select(t => new LP.ServiceHost.DataContracts.Common.Content.Topic
                    {
                        TopicId = t.TopicId,
                        TopicName = t.Name,
                        CategoryId = t.Topic.TopicCategoryId,
                        TopicCategoryName = topicCategories.FirstOrDefault(c => c.TopicCategoryId == t.Topic.TopicCategoryId && c.Culture == culture).Name,
                        SortOrder = t.Topic.SortOrder,
                        //Checked = post.ltl_Topics.Any(p=>p.TopicId == t.TopicId)
                        Checked = false
                    });

            var topicList = new List<LP.ServiceHost.DataContracts.Common.Content.Topic>();
            foreach (var topic in topicContracts)
            {
                if(post.ltl_Topics.Any(p=>p.TopicId == topic.TopicId))
                {
                    topic.Checked = true;
                }
                topicList.Add(topic);
            }

            var topicCategoriesForCulture = topicCategories.Where(t => t.Culture == culture && t.Status != Status.Deleted);
            
            foreach (var category in topicCategoriesForCulture)
            {
                result.CategoryTopics.Add(new CategoryTopic()
                {
                    Id = category.TopicCategoryId.ToString(),
                    Name = category.Name,
                    Checked = false,
                    IsCategory = true
                });

                var categoryTopics = topicList.Where(t => t.CategoryId == category.TopicCategoryId);

                foreach (var topic in categoryTopics)
                {
                    result.CategoryTopics.Add(new CategoryTopic()
                    {
                        Id = string.Format("{0}_{1}", category.TopicCategoryId, topic.TopicId),
                        Name = topic.TopicName,
                        Checked = topic.Checked,
                        IsCategory = false
                    });
                }
            }
            

            return result;
        }

        public async Task<PostTopicResponseContract> GetSearchTopics(string culture)
        {
            var topics = await _baseCommands.GetAllAsync<TopicTranslation>();
            var topicCategories = await _baseCommands.GetAllAsync<TopicCategoryTranslation>();
            
            var result = new PostTopicResponseContract();
            
            var topicsForCulture = topics.Where(t => t.Culture == culture && t.Status != Status.Deleted);

            var topicCategoriesForCulture = topicCategories.Where(t => t.Culture == culture && t.Status != Status.Deleted);
            
            foreach (var category in topicCategoriesForCulture)
            {
                result.CategoryTopics.Add(new CategoryTopic()
                {
                    Id = category.TopicCategoryId.ToString(),
                    Name = category.Name,
                    IsCategory = true
                });

                var categoryTopics = topicsForCulture.Where(t => t.Topic.TopicCategoryId == category.TopicCategoryId);

                foreach (var topic in categoryTopics)
                {
                    result.CategoryTopics.Add(new CategoryTopic()
                    {
                        //Id = string.Format("{0}_{1}", category.TopicCategoryId, topic.TopicId),
                        Id = topic.TopicId.ToString(),
                        Name = topic.Name,
                        IsCategory = false
                    });
                }
            }
            

            return result;
        }

    }
}
