using System;
using System.Collections.Generic;
using System.Linq;
using LP.Api.Shared.Interfaces.Data;
using LP.Content.BusinessLayer.Commands;
using LP.EntityModels;
using LP.ServiceHost.DataContracts.Enums;
using Moq;
using SpecsFor;

namespace LP.Content.BusinessLayer.Tests.CommandTests.TopicCommandsTest
{
    public class BaseGiven : SpecsFor<TopicCommands>
    {
        protected readonly Mock<IBaseCommands> BaseCommandsMock = new Mock<IBaseCommands>();
        protected List<TopicCategory> TopicCategories = new List<TopicCategory>()
        {
            new TopicCategory()
            {
                TopicCategoryId = 1,
                SortOrder = 1
            }
        };

        public static readonly DateTime LastUpdated = DateTime.UtcNow;
        
        protected List<TopicCategoryTranslation> TopicCategoryTranslations = new List<TopicCategoryTranslation>()
        {
            new TopicCategoryTranslation()
            {
                TopicCategoryId = 1,
                Culture = "en",
                Name = "test",
                LastUpdated = LastUpdated,
                Status = Status.Live,
                TopicCategoryTranslationId = 1,
                TopicCategory = new TopicCategory()
                {
                    TopicCategoryId = 1,
                    SortOrder = 1,
                    Status = Status.Live,
                    CreatedByUserId = 1,
                    DateCreated = DateTime.UtcNow
                }
            }
        };
        protected List<TopicTranslation> Topics = new List<TopicTranslation>();
        protected TopicCategory TopicCategory = new TopicCategory()
        {
            TopicCategoryId = 1,
            SortOrder = 1
        };
        public TopicCategoryTranslation TopicCategoryTranslation = new TopicCategoryTranslation()
        {
            TopicCategoryId = 1,
            Culture = "en",
            Name = "test",
            LastUpdated = LastUpdated,
            Status = Status.Live,
            TopicCategoryTranslationId = 1
        };
        public TopicCategoryTranslation DeletedTopicCategoryTranslation = new TopicCategoryTranslation()
        {
            TopicCategoryId = 1,
            Culture = "en",
            Name = "test",
            LastUpdated = LastUpdated,
            Status = Status.Deleted,
            TopicCategoryTranslationId = 1
        };
        public Topic TopicDeleted = new Topic()
        {
            TopicCategoryId = 1,
            Status = Status.Live
        };

        protected void PrepareSut()
        {
            BaseCommandsMock.Setup(m => m.GetAllAsync<TopicCategory>()).ReturnsAsync(TopicCategories.AsQueryable());
            BaseCommandsMock.Setup(m => m.GetAllAsync<TopicCategoryTranslation>()).ReturnsAsync(TopicCategoryTranslations.AsQueryable());
            BaseCommandsMock.Setup(m => m.GetByIdAsync<TopicCategory>(TopicCategory.TopicCategoryId)).ReturnsAsync(TopicCategory);
            BaseCommandsMock.Setup(m => m.GetAllAsync<TopicTranslation>()).ReturnsAsync(Topics.AsQueryable());
            BaseCommandsMock.Setup(m => m.GetByIdAsync<Topic>(It.IsAny<int>())).ReturnsAsync(TopicDeleted);
            
            SUT = new TopicCommands(BaseCommandsMock.Object);
        }
    }
}
