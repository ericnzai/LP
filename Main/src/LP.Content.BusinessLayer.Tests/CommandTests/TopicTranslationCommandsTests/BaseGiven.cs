using System.Collections.Generic;
using System.Linq;
using LP.Api.Shared.Interfaces.Api;
using LP.Api.Shared.Interfaces.Core.Encryption;
using LP.Api.Shared.Interfaces.Data;
using LP.Api.Shared.Tests.AsyncDb;
using LP.Content.BusinessLayer.Commands;
using LP.EntityModels;
using LP.ServiceHost.DataContracts.Enums;
using Moq;
using SpecsFor;
using System.Linq.Expressions;
using System;
using LP.ServiceHost.DataContracts.Response.Content;

namespace LP.Content.BusinessLayer.Tests.CommandTests.TopicTranslationCommandsTests
{
    public class BaseGiven :SpecsFor<TopicTranslationCommands>
    {
        protected readonly Mock<IBaseCommands> BaseCommandsMock = new Mock<IBaseCommands>();
        protected readonly Mock<IEncryptionHandler> EncryptionHandlerMock = new Mock<IEncryptionHandler>();
        protected readonly Mock<ICultureProvider> CultureProviderMock = new Mock<ICultureProvider>();

        protected string Culture = "en";
        protected List<TopicCategoryTranslation> TopicCategoryTranslations = new List<TopicCategoryTranslation>();
        protected List<TopicTranslation> TopicTranslations = new List<TopicTranslation>(); 
        protected List<Topic> Topics = new List<Topic>
        {
            new Topic(){ TopicId = 1, TopicCategoryId = TopicCategoryId, Status = Status.Live},
            new Topic(){ TopicId = 2, TopicCategoryId = TopicCategoryId, Status = Status.Live},
            new Topic(){ TopicId = 3, TopicCategoryId = TopicCategoryId, Status = Status.Deleted},
            new Topic(){ TopicId = 4, TopicCategoryId = 2, Status = Status.Live},
            new Topic(){ TopicId = 5, TopicCategoryId = 3, Status = Status.Live}
        };

        protected TopicCategory TopicCategorySingle = new TopicCategory()
        {
            TopicCategoryId = TopicCategoryId,
            Status = Status.Live,
            SortOrder = 1
        };

        protected Topic TopicSingle = new Topic()
        {
            TopicId = TopicId,
            Status = Status.Live,
            SortOrder = 1
        };

        protected TopicCategoryTranslation TopicCategoryTranslationSingle = new TopicCategoryTranslation
        {
            TopicCategoryId = TopicCategoryId,
            Name = "Category 1 En",
            Culture = "en",
            Status = Status.Live,
            LastUpdated = DateTime.Now,
            LastUpdatedByUserId = UserId
        };

        protected TopicTranslation TopicTranslationSingle = new TopicTranslation
        {

        };
        
        protected const int TopicCategoryId = 386;
        protected const int TopicId = 1;
        protected const int UserId = 1;

        protected const string DisplayName1FirstDecrypted = "topicCat1EnFirstNameDecrypted";
        protected const string DisplayName1LastDecrypted = "topicCat1EnLastNameDecrypted";
        protected const string DisplayName1Decrypted = "topicCat1EnFirstNameDecrypted topicCat1EnLastNameDecrypted";

        protected const string DisplayName2Decrypted = "topicCat2EnNameDecrypted";
        protected const string DisplayName3Decrypted = "topicCat2TrNameDecrypted";
        protected const string DisplayName4Decrypted = "topicCat3EnNameDecrypted";
        protected const string DisplayName5Decrypted = "topicCat4EnNameDecrypted";
        protected const string DisplayName6Decrypted = "topicCat4TrNameDecrypted";

        protected string CultureTr = "tr";
        protected const string CultureDisplayNameTr = "Turkish - Turkey";

        protected void PrepareSut()
        {
            CultureProviderMock.Setup(m => m.GetCultureDisplayName(Culture)).ReturnsAsync(CultureDisplayNameTr);

            EncryptionHandlerMock.Setup(m => m.DecryptString("topicCat1")).Returns(DisplayName1FirstDecrypted);
            EncryptionHandlerMock.Setup(m => m.DecryptString("EnName")).Returns(DisplayName1LastDecrypted);

            EncryptionHandlerMock.Setup(m => m.DecryptString("topicCat2EnName")).Returns(DisplayName2Decrypted);
            EncryptionHandlerMock.Setup(m => m.DecryptString("topicCat2TrName")).Returns(DisplayName3Decrypted);
            EncryptionHandlerMock.Setup(m => m.DecryptString("topicCat3EnName")).Returns(DisplayName4Decrypted);
            EncryptionHandlerMock.Setup(m => m.DecryptString("topicCat4EnName")).Returns(DisplayName5Decrypted);
            EncryptionHandlerMock.Setup(m => m.DecryptString("topicCat4TrName")).Returns(DisplayName6Decrypted);

            var topicCategoryTranslationMoqDbSetProvider = new MoqDbSetProvider<TopicCategoryTranslation>();

            var topicCategoryTranslationMoqDbSet = topicCategoryTranslationMoqDbSetProvider.DbSet(TopicCategoryTranslations);

            var topicTranslationMoqDbSetProvider = new MoqDbSetProvider<TopicTranslation>();

            var topicTranslationMoqDbSet = topicTranslationMoqDbSetProvider.DbSet(TopicTranslations);


            BaseCommandsMock.Setup(m => m.GetWithIncludesAsync<TopicCategoryTranslation>(inc => inc.TopicCategory, inc => inc.User, inc => inc.User.askCore_UserDetails)).ReturnsAsync(topicCategoryTranslationMoqDbSet.Object);
            BaseCommandsMock.Setup(m => m.GetAllAsync<TopicCategoryTranslation>()).ReturnsAsync(topicCategoryTranslationMoqDbSet.Object);
            BaseCommandsMock.Setup(m => m.GetWithIncludesAsync<TopicTranslation>(inc => inc.Topic, inc => inc.User, inc => inc.User.askCore_UserDetails)).ReturnsAsync(topicTranslationMoqDbSet.Object);
            BaseCommandsMock.Setup(m => m.GetAllAsync<TopicTranslation>()).ReturnsAsync(topicTranslationMoqDbSet.Object);

            SUT = new TopicTranslationCommands(BaseCommandsMock.Object, EncryptionHandlerMock.Object, CultureProviderMock.Object);
        }
    }
}
