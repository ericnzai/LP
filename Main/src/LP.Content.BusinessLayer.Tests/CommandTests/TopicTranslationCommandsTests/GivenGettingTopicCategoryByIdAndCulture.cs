using LP.EntityModels;
using LP.ServiceHost.DataContracts.Enums;
using LP.ServiceHost.DataContracts.Response.Content;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LP.Content.BusinessLayer.Tests.CommandTests.TopicTranslationCommandsTests
{
    public class GivenGettingTopicCategoryByIdAndCulture : BaseGiven
    {
        private TopicCategoryTranslationFormResponseContract _topicCategoryTranslationFormResponseContract;

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenCorrectMethodsAndIncludesShouldBeReturned : GivenGettingTopicCategoryByIdAndCulture
        {
            protected override async void When()
            {
                PrepareSut();

                _topicCategoryTranslationFormResponseContract =
                    await SUT.GetTopicCategoryByIdAndCulture(TopicCategoryId, Culture);

            }

            [Test]
            public void ThenGetWithIncludesAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetWithIncludesAsync(It.IsAny<Expression<Func<TopicCategoryTranslation, object>>[]>()), Times.Once());

            }

            [Test]
            public void ThenGetWithIncludesAsyncIsCalledOnceWithTheCorrectParameters()
            {
                BaseCommandsMock.Verify(m => m.GetWithIncludesAsync<TopicCategoryTranslation>(inc => inc.TopicCategory, inc => inc.User, inc => inc.User.askCore_UserDetails), Times.Once());

            }

            [Test]
            public void ThenEncryptionHandlerDecryptStringIsNeverCalled()
            {
                EncryptionHandlerMock.Verify(m => m.DecryptString(It.IsAny<string>()), Times.Never());
            }

            [Test]
            public void ThenTopicCategoryTranslationFormResponseContractIsNotNull()
            {
                Assert.IsNotNull(_topicCategoryTranslationFormResponseContract);
            }

        }

        public class WhenOneTranslatedItemShouldBeReturned :
            GivenGettingTopicCategoryByIdAndCulture
        {
            protected override async void When()
            {
                TopicCategoryTranslations = new List<TopicCategoryTranslation>
                {
                    new TopicCategoryTranslation {Culture = Culture, Name = "topicCat1EnCategoryName", TopicCategoryId = TopicCategoryId, TopicCategory = TopicCategorySingle, User = new User{DisplayName = "topicCat1EnName"}}
                };

                PrepareSut();

                _topicCategoryTranslationFormResponseContract =
                    await SUT.GetTopicCategoryByIdAndCulture(TopicCategoryId, Culture);

            }

            [Test]
            public void ThenGetAllTopicCategoriesAsyncIsNeverCalled()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<TopicCategory>(), Times.Never);
            }


            [Test]
            public void ThenGetWithIncludesAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetWithIncludesAsync(It.IsAny<Expression<Func<TopicCategoryTranslation, object>>[]>()), Times.Once());

            }

            [Test]
            public void ThenGetWithIncludesAsyncIsCalledOnceWithTheCorrectParameters()
            {
                BaseCommandsMock.Verify(m => m.GetWithIncludesAsync<TopicCategoryTranslation>(inc => inc.TopicCategory, inc => inc.User, inc => inc.User.askCore_UserDetails), Times.Once());

            }

            [Test]
            public void ThenEncryptionHandlerDecryptStringIsCalledTwiceForGlobalAndLocalTranslation()
            {
                const int expected = 2;
                EncryptionHandlerMock.Verify(m => m.DecryptString(It.IsAny<string>()), Times.Exactly(expected));
            }

        }

        public class WhenEmptyTranslatedItemShouldBeReturned :
            GivenGettingTopicCategoryByIdAndCulture
        {
            protected override async void When()
            {
                TopicCategoryTranslations = new List<TopicCategoryTranslation>
                {
                    new TopicCategoryTranslation {Culture = "en", Name = "topicCat1EnCategoryName", TopicCategoryId = TopicCategoryId, TopicCategory = TopicCategorySingle, User = new User{DisplayName = "topicCat1EnName"}}
                };

                PrepareSut();

                Culture = "tr";
                _topicCategoryTranslationFormResponseContract =
                    await SUT.GetTopicCategoryByIdAndCulture(TopicCategoryId, Culture);

            }

            [Test]
            public void ThenGetAllTopicCategoriesAsyncIsNeverCalled()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<TopicCategory>(), Times.Never);
            }


            [Test]
            public void ThenGetWithIncludesAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetWithIncludesAsync(It.IsAny<Expression<Func<TopicCategoryTranslation, object>>[]>()), Times.Once());

            }

            [Test]
            public void ThenGetWithIncludesAsyncIsCalledOnceWithTheCorrectParameters()
            {
                BaseCommandsMock.Verify(m => m.GetWithIncludesAsync<TopicCategoryTranslation>(inc => inc.TopicCategory, inc => inc.User, inc => inc.User.askCore_UserDetails), Times.Once());

            }

            [Test]
            public void ThenEncryptionHandlerDecryptStringIsCalledOnceOnlyForGlobalTranslation()
            {
                EncryptionHandlerMock.Verify(m => m.DecryptString(It.IsAny<string>()), Times.Once);
            }

            [Test]
            public void ThenTheTopicCategoryTranslationNameIsEmpryString()
            {
                const string expected = "";
                Assert.AreEqual(expected, _topicCategoryTranslationFormResponseContract.LocalCategoryTranslation.TopicCategoryName);
            }

            [Test]
            public void ThenTheTopicCategoryTranslationStatusIsTranslationInProgress()
            {
                const Status expected = Status.TranslationInProgress;
                Assert.AreEqual(expected, _topicCategoryTranslationFormResponseContract.LocalCategoryTranslation.Status);
            }

            [Test]
            public void ThenTheTopicCategoryTranslationCultureIsExpectedLocalCulture()
            {
                Assert.AreEqual(Culture, _topicCategoryTranslationFormResponseContract.LocalCategoryTranslation.Culture);
            }

            [Test]
            public void ThenTheTopicCategoryTranslationUserUpdatedByNameIsCorrect()
            {
                const string expected = "";
                Assert.AreEqual(expected, _topicCategoryTranslationFormResponseContract.LocalCategoryTranslation.UpdatedByUserName);
            }

            [Test]
            public void ThenTheTopicCategoryTranslationIsTranslatedIsFalse()
            {
                Assert.AreEqual(false, _topicCategoryTranslationFormResponseContract.LocalCategoryTranslation.IsTranslated);
            }

            [Test]
            public void ThenTheTopicTranslationCultureDisplayNameIsCorrect()
            {
                CultureProviderMock.Verify(m => m.GetCultureDisplayName(CultureTr), CultureDisplayNameTr);
            }

        }
    }
}
