using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using LP.Api.Shared.Interfaces.Api;
using LP.Api.Shared.Interfaces.BusinessLayer.Content;
using LP.Api.Shared.Interfaces.Data;
using LP.Api.Shared.Providers;
using LP.ServiceHost.DataContracts.Common.Content;
using LP.ServiceHost.DataContracts.Enums;
using LP.ServiceHost.DataContracts.Response.Content;
using Topic = LP.EntityModels.Topic;
using TopicCategoryTranslation = LP.EntityModels.TopicCategoryTranslation;
using LP.Api.Shared.Interfaces.Core.Encryption;
using LP.EntityModels;

namespace LP.Content.BusinessLayer.Commands
{
    public class TopicTranslationCommands : ITopicTranslationCommands
    {
        private readonly IBaseCommands _baseCommands;
        private readonly IEncryptionHandler _encryptionHandler;
        private readonly ICultureProvider _cultureProvider;
        
        public TopicTranslationCommands(IBaseCommands baseCommands, IEncryptionHandler encryptionHandler, ICultureProvider cultureProvider)
        {
            _baseCommands = baseCommands;
            _encryptionHandler = encryptionHandler;
            _cultureProvider = cultureProvider;
        }

        public async Task<CompleteTopicCategoryTranslationResponseContract> GetAllTopicCategories(string culture)
        {
            var topicCategories = await _baseCommands.GetWithIncludesAsync<TopicCategoryTranslation>(inc => inc.TopicCategory, inc => inc.User, inc => inc.User.askCore_UserDetails);

            var result = new CompleteTopicCategoryTranslationResponseContract();

            var globalFilteredTopicCategoryTranslations =
                topicCategories.Where(t => t.Status != Status.Deleted && t.Culture == ConstantProvider.GlobalCulture);

            var localFilteredTopicCategoryTranslations =
                topicCategories.Where(t => t.Status != Status.Deleted && t.Culture == culture);

            if (!globalFilteredTopicCategoryTranslations.Any()) return result;

            foreach (var topicCategoryGlobalTranslation in globalFilteredTopicCategoryTranslations)
            {
                var topicCategoryLocalTranslation =
                    localFilteredTopicCategoryTranslations.FirstOrDefault(
                        a => a.TopicCategoryId == topicCategoryGlobalTranslation.TopicCategoryId);

                
                if (topicCategoryLocalTranslation != null)
                {
                    var listTopicTranslations = await GetAllTopicTranslationContractsByCategory(topicCategoryLocalTranslation.TopicCategoryId, culture);
                    var topicCategoryTranslationContract = await 
                        ConvertTopicCategoryTranslationToContract(topicCategoryLocalTranslation, listTopicTranslations, true);

                    result.TopicCategoryTranslations.Add(topicCategoryTranslationContract);
                }
                else
                {
                    var listTopicTranslations = await GetAllTopicTranslationContractsByCategory(topicCategoryGlobalTranslation.TopicCategoryId, culture);
                    var topicCategoryTranslationContract = await ConvertTopicCategoryTranslationToContract(topicCategoryGlobalTranslation, listTopicTranslations, false);
                   
                    result.TopicCategoryTranslations.Add(topicCategoryTranslationContract);
                }
            }

            return result;
        }

        public async Task<CompleteTopicTranslationResponseContract> GetAllTopics(string culture)
        {
            var result = new CompleteTopicTranslationResponseContract();

            var allTopicTranslations = await _baseCommands.GetWithIncludesAsync<TopicTranslation>(inc => inc.Topic, inc => inc.User, inc => inc.User.askCore_UserDetails);
            var globalFilteredTopicTranslations =
                allTopicTranslations.Where(
                    tt =>
                        tt.Culture == ConstantProvider.GlobalCulture && tt.Status != Status.Deleted);
            var localFilteredTopicTranslations =
                allTopicTranslations.Where(
                    tt =>
                        tt.Culture == culture && tt.Status != Status.Deleted);

            if (!globalFilteredTopicTranslations.Any()) return result;

            foreach (var topicGlobalTranslation in globalFilteredTopicTranslations)
            {
                var topicLocalTranslation =
                    localFilteredTopicTranslations.FirstOrDefault(
                        a => a.TopicId == topicGlobalTranslation.TopicId);


                if (topicLocalTranslation != null)
                {
                    var topicTranslationContract = await ConvertTopicTranslationToContract(topicLocalTranslation, true);

                    result.TopicTranslations.Add(topicTranslationContract);
                }
                else
                {
                    var topicTranslationContract = await ConvertTopicTranslationToContract(topicGlobalTranslation, false);

                    result.TopicTranslations.Add(topicTranslationContract);
                }
            }

            return result;
        }

        public async Task<CompleteTopicTranslationResponseContract> GetAllTopicTranslationContractsByCategory(int topicCategoryId, string culture)
        {
            var result = new CompleteTopicTranslationResponseContract();

            var allTopicTranslations = await _baseCommands.GetWithIncludesAsync<TopicTranslation>(inc => inc.Topic, inc => inc.User, inc => inc.User.askCore_UserDetails);
            var globalFilteredTopicTranslations =
                allTopicTranslations.Where(
                    tt =>
                        tt.Culture == ConstantProvider.GlobalCulture && tt.Status != Status.Deleted &&
                        tt.Topic.TopicCategoryId == topicCategoryId);
            var localFilteredTopicTranslations =
                allTopicTranslations.Where(
                    tt =>
                        tt.Culture == culture && tt.Status != Status.Deleted &&
                        tt.Topic.TopicCategoryId == topicCategoryId);

            if (!globalFilteredTopicTranslations.Any()) return result;

            foreach (var topicGlobalTranslation in globalFilteredTopicTranslations)
            {
                var topicLocalTranslation =
                    localFilteredTopicTranslations.FirstOrDefault(
                        a => a.TopicId == topicGlobalTranslation.TopicId);


                if (topicLocalTranslation != null)
                {
                    var topicTranslationContract = await ConvertTopicTranslationToContract(topicLocalTranslation, true);

                    result.TopicTranslations.Add(topicTranslationContract);
                }
                else
                {
                    var topicTranslationContract = await ConvertTopicTranslationToContract(topicGlobalTranslation, false);

                    result.TopicTranslations.Add(topicTranslationContract);
                }
            }

            return result;
        }

        public async Task<TopicCategoryTranslationFormResponseContract> GetTopicCategoryByIdAndCulture(int topicCategoryId, string culture)
        {
            var result = new TopicCategoryTranslationFormResponseContract();
            var topicCategories = await _baseCommands.GetWithIncludesAsync<TopicCategoryTranslation>(inc => inc.TopicCategory, inc => inc.User, inc => inc.User.askCore_UserDetails);
            
            var globalTopicCategoryTranslation = await 
                topicCategories.FirstOrDefaultAsync(t => t.TopicCategoryId == topicCategoryId && t.Status != Status.Deleted && t.Culture == ConstantProvider.GlobalCulture);

            if (globalTopicCategoryTranslation == null) return result;
            var globalcontrac  = await ConvertTopicCategoryTranslationToContract(globalTopicCategoryTranslation, null, true);
            result.GlobalTopicCategoryTranslation = globalcontrac;


            var localTopicCategoryTranslation = await
                topicCategories.FirstOrDefaultAsync(t => t.TopicCategoryId == topicCategoryId && t.Status != Status.Deleted && t.Culture == culture);
            if (localTopicCategoryTranslation == null)
            {
                result.LocalCategoryTranslation = new TopicCategoryTranslationContract()
                {
                    TopicCategoryId = topicCategoryId,
                    TopicCategoryName = string.Empty,
                    Status = Status.TranslationInProgress,
                    Culture = culture,
                    CultureDisplayName = await _cultureProvider.GetCultureDisplayName(culture),
                    IsTranslated = false,
                    SortOrder = 1,
                    LastUpdated = DateTime.Now,
                    UpdatedByUserName = string.Empty,
                    TopicTranslations = null
                };
            }
            else
            {
                var localcontrac = await ConvertTopicCategoryTranslationToContract(localTopicCategoryTranslation, null, true);
                result.LocalCategoryTranslation = localcontrac;
            }
            return result;
        }

        public async Task<TopicTranslationFormResponseContract> GetTopicByIdAndCulture(int topicId, string culture)
        {
            var result = new TopicTranslationFormResponseContract();

            var allTopicTranslations = await _baseCommands.GetWithIncludesAsync<TopicTranslation>(inc => inc.Topic, inc => inc.User, inc => inc.User.askCore_UserDetails);
            var globalTopicTranslation = await 
                allTopicTranslations.FirstOrDefaultAsync(
                    tt => tt.TopicId == topicId && 
                        tt.Culture == ConstantProvider.GlobalCulture && tt.Status != Status.Deleted);
            
            if (globalTopicTranslation == null) return result;
            result.GlobalTopicTranslation = await ConvertTopicTranslationToContract(globalTopicTranslation, true);

            var localTopicTranslation = await
                allTopicTranslations.FirstOrDefaultAsync(
                    tt => tt.TopicId == topicId &&
                        tt.Culture == culture && tt.Status != Status.Deleted);
            if (localTopicTranslation == null)
            {
                result.LocalTopicTranslation = new TopicTranslationContract
                {
                    TopicId = topicId,
                    Culture = culture,
                    CultureDisplayName = await _cultureProvider.GetCultureDisplayName(culture),
                    IsTranslated = false,
                    TopicName = string.Empty,
                    LastUpdated = DateTime.Now,
                    Status = Status.TranslationInProgress,
                    UpdatedByUserName = string.Empty
                };
            }
            else
            {
                result.LocalTopicTranslation = await ConvertTopicTranslationToContract(localTopicTranslation, true);
            }

            return result;
        }

        public async Task<TopicCategoryTranslationResponseContract> AddTopicCategoryTranslation(string culture, string name, int topicCategoryId, int userId, Status status)
        {
            if (CategoryNameAlreadyExists(0, name, culture).Result) return new TopicCategoryTranslationResponseContract { Result = false };

            var entityTranslation = new TopicCategoryTranslation() { Name = name, Culture = culture, LastUpdatedByUserId = userId, Status = status, TopicCategoryId = topicCategoryId, LastUpdated = DateTime.Now };

            _baseCommands.Add(entityTranslation);

            await _baseCommands.SaveChangesAsync();

            var result = new TopicCategoryTranslationResponseContract { Result = true };

            return result;
        }

        public async Task<TopicCategoryTranslationUpdateResponseContract> UpdateTopicCategoryTranslation(string culture, string name, int topicCategoryId, int userId, Status status)
        {
            if (CategoryNameAlreadyExists(topicCategoryId, name, culture).Result) return new TopicCategoryTranslationUpdateResponseContract { Result = false };
            var topicCategories = await _baseCommands.GetAllAsync<TopicCategoryTranslation>();
            var topicCategory = topicCategories.FirstOrDefault(t => t.Culture == culture && t.TopicCategoryId == topicCategoryId && t.Status != Status.Deleted);

            if (topicCategory == null) return new TopicCategoryTranslationUpdateResponseContract { Result = false };
            topicCategory.Name = name;
            topicCategory.LastUpdated = DateTime.UtcNow;
            topicCategory.LastUpdatedByUserId = userId;
            topicCategory.Status = status;

            _baseCommands.Update(topicCategory);
            _baseCommands.SaveChanges();

            var result = new TopicCategoryTranslationUpdateResponseContract { Result = true };

            return result;
        }

        public async Task<TopicTranslationResponseContract> AddTopicTranslation(string culture, string name, int topicId, int userId, Status status)
        {
            if (TopicNameAlreadyExists(0, name, culture).Result) return new TopicTranslationResponseContract { Result = false };

            var entityTranslation = new TopicTranslation()
            {
                Name = name,
                Culture = culture,
                LastUpdated = DateTime.Now,
                Status = status,
                LastUpdatedByUserId = userId,
                TopicId = topicId

            };

            _baseCommands.Add(entityTranslation);
            await _baseCommands.SaveChangesAsync();

            var result = new TopicTranslationResponseContract { Result = true };

            return result;
        }

        public async Task<TopicTranslationUpdateResponseContract> UpdateTopicTranslation(string culture, string name, int topicId, int userId, Status status)
        {
            var topicTranslations = await _baseCommands.GetAllAsync<TopicTranslation>();
            var topicTranslation = topicTranslations.FirstOrDefault(t => t.Culture == culture && t.TopicId == topicId);

            if (topicTranslation == null) return new TopicTranslationUpdateResponseContract { Result = false };
            topicTranslation.Name = name;
            topicTranslation.LastUpdated = DateTime.UtcNow;
            topicTranslation.LastUpdatedByUserId = userId;
            topicTranslation.Status = status;

            _baseCommands.Update(topicTranslation);
            _baseCommands.SaveChanges();

            var result = new TopicTranslationUpdateResponseContract { Result = true };

            return result;
        }

        //Private Mambers
        private async Task<bool> CategoryNameAlreadyExists(int id, string name, string culture)
        {
            var topicCategories = await _baseCommands.GetAllAsync<TopicCategoryTranslation>();

            var result = topicCategories.Any(t => t.Status != Status.Deleted && t.Name == name && t.Culture == culture && t.TopicCategoryId != id);

            return result;
        }

        private async Task<bool> TopicNameAlreadyExists(int id, string name, string culture)
        {
            var topics = await _baseCommands.GetAllAsync<TopicTranslation>();

            var result = topics.Any(t => t.Status != Status.Deleted && t.Name == name && t.Culture == culture && t.TopicId != id);

            return result;
        }

        private async Task<TopicCategoryTranslationContract> ConvertTopicCategoryTranslationToContract(
            TopicCategoryTranslation topicCategoryTranslation, CompleteTopicTranslationResponseContract listTopicTranslations, bool isTranslated)
        {
            var topicTranslations = new List<TopicTranslationContract>();
            if (listTopicTranslations != null) topicTranslations = listTopicTranslations.TopicTranslations;
            var userName = GetUserName(topicCategoryTranslation.User);

            return new TopicCategoryTranslationContract
            {
                TopicCategoryId = topicCategoryTranslation.TopicCategoryId,
                TopicCategoryName = topicCategoryTranslation.Name,
                Status = topicCategoryTranslation.Status,
                Culture = topicCategoryTranslation.Culture,
                CultureDisplayName = await _cultureProvider.GetCultureDisplayName(topicCategoryTranslation.Culture),
                IsTranslated = isTranslated,
                SortOrder = topicCategoryTranslation.TopicCategory.SortOrder,
                LastUpdated = topicCategoryTranslation.LastUpdated,
                UpdatedByUserName = userName,
                TopicTranslations = topicTranslations
            };
        }

        private async Task<TopicTranslationContract> ConvertTopicTranslationToContract(
            TopicTranslation topicTranslation, bool isTranslated)
        {
            var userName = GetUserName(topicTranslation.User);

            return new TopicTranslationContract
            {
                TopicId = topicTranslation.TopicId,
                Culture = topicTranslation.Culture,
                CultureDisplayName = await _cultureProvider.GetCultureDisplayName(topicTranslation.Culture),
                IsTranslated = isTranslated,
                TopicName = topicTranslation.Name,
                LastUpdated = topicTranslation.LastUpdated,
                Status = topicTranslation.Status,
                UpdatedByUserName = userName
            };
        }

        private string GetUserName(User user)
        {
            var userName = _encryptionHandler.DecryptString(user.DisplayName);
            if (String.IsNullOrWhiteSpace(userName) && user.askCore_UserDetails != null)
                userName = _encryptionHandler.DecryptString(user.askCore_UserDetails.FirstName)
                    + " " + _encryptionHandler.DecryptString(user.askCore_UserDetails.LastName);
            return userName;
        }

    }
}
