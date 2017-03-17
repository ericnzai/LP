using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LP.Api.Shared.Providers;
using LP.EntityModels;
using LP.ServiceHost.DataContracts.Common.Content;
using LP.ServiceHost.DataContracts.Enums;
using LP.ServiceHost.DataContracts.Response.Content;
using Moq;
using NUnit.Framework;
using Topic = LP.EntityModels.Topic;
using TopicCategory = LP.EntityModels.TopicCategory;

namespace LP.Content.BusinessLayer.Tests.CommandTests.TopicTranslationCommandsTests
{
    public class GivenGettingAllTranslatedTopicCategories : BaseGiven
    {
        private CompleteTopicCategoryTranslationResponseContract _completeTopicCategoryTranslationResponseContract;

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenCorrectMethodsAndIncludesShouldBeReturned : GivenGettingAllTranslatedTopicCategories
        {
            protected override async void When()
            {
                TopicCategoryTranslations = new List<TopicCategoryTranslation>();

                PrepareSut();

                _completeTopicCategoryTranslationResponseContract = await SUT.GetAllTopicCategories(Culture);
            }

            [Test]
            public void ThenGetAllTopicsAsyncIsNeverCalled()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<Topic>(), Times.Never());
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
            public void ThenCompleteTopicCategoryTranslationResponseContractIsNotNull()
            {
                Assert.IsNotNull(_completeTopicCategoryTranslationResponseContract);
            }

            [Test]
            public void ThenNoTopicCategoryTranslationsAreReturned()
            {
                CollectionAssert.IsEmpty(_completeTopicCategoryTranslationResponseContract.TopicCategoryTranslations);
            }
        }

        public class WhenOneTranslatedItemAndOneNotTranslatedItemShouldBeReturned : GivenGettingAllTranslatedTopicCategories
        {
            private TopicCategoryTranslationContract _turkishTopicCategoryTranslationContract;
            private TopicCategoryTranslationContract _globalEnglishTopicCategoryTranslationContract;

            protected override async void When()
            {
                TopicCategoryTranslations = new List<TopicCategoryTranslation>
                {
                    new TopicCategoryTranslation {Culture = "en", Name = "topicCat1EnCategoryName",TopicCategoryId = 1, TopicCategory = new TopicCategory{TopicCategoryId = 1}, User = new User {DisplayName = "", askCore_UserDetails = new askCore_UserDetails{ FirstName = "topicCat1", LastName = "EnName"}}},
                    new TopicCategoryTranslation {Culture = "en", Name = "topicCat2EnCategoryName", TopicCategoryId = 2, TopicCategory = new TopicCategory{TopicCategoryId = 2}, User = new User{DisplayName = "topicCat2EnName"}},
                    new TopicCategoryTranslation {Culture = "tr", Name = "topicCat2TrCategoryName", TopicCategoryId = 2, TopicCategory = new TopicCategory{TopicCategoryId = 2}, User = new User{DisplayName = "topicCat2TrName"}}
                };

                Culture = "tr";

                PrepareSut();

                _completeTopicCategoryTranslationResponseContract = await SUT.GetAllTopicCategories(Culture);

                _turkishTopicCategoryTranslationContract =
                  _completeTopicCategoryTranslationResponseContract.TopicCategoryTranslations.FirstOrDefault(
                      a => a.Culture == "tr");

                _globalEnglishTopicCategoryTranslationContract =
                 _completeTopicCategoryTranslationResponseContract.TopicCategoryTranslations.FirstOrDefault(
                     a => a.Culture == "en");

            }

            [Test]
            public void ThenGetAllTopicsAsyncIsNeverCalled()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<Topic>(), Times.Never);
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
            public void ThenEncryptionHandlerDecryptStringIsCalledTwice()
            {
                const int expected = 4;
                EncryptionHandlerMock.Verify(m => m.DecryptString(It.IsAny<string>()), Times.Exactly(expected));
            }

            [Test]
            public void ThenEncryptionHandlerDecryptStringIsCalledOnceForTheGlobalEnglishTransltionFirstName()
            {
                EncryptionHandlerMock.Verify(m => m.DecryptString(It.Is<string>(x => x == "topicCat1")), Times.Once());
            }

            [Test]
            public void ThenEncryptionHandlerDecryptStringIsCalledOnceForTheGlobalEnglishTransltionLastName()
            {
                EncryptionHandlerMock.Verify(m => m.DecryptString(It.Is<string>(x => x == "EnName")), Times.Once());
            }

            [Test]
            public void ThenEncryptionHandlerDecryptStringIsCalledOnceForTheTurkishTransltion()
            {
                EncryptionHandlerMock.Verify(m => m.DecryptString(It.Is<string>(x => x == "topicCat2TrName")), Times.Once());
            }

            [Test]
            public void ThenCompleteTopicCategoryTranslationResponseContractIsNotNull()
            {
                Assert.IsNotNull(_completeTopicCategoryTranslationResponseContract);
            }

            [Test]
            public void ThenTheCorrectAmountOfTopicCategoryTranslationsAreReturned()
            {
                const int expected = 2;

                Assert.AreEqual(expected, _completeTopicCategoryTranslationResponseContract.TopicCategoryTranslations.Count);
            }

            [Test]
            public void ThenTheResponseContainsOneTurkishTopicCategoryTranslationContract()
            {
                const int expected = 1;

                var topicCategoryTranslationContract =
                    _completeTopicCategoryTranslationResponseContract.TopicCategoryTranslations.Count(
                        a => a.Culture == "tr");

                Assert.AreEqual(expected, topicCategoryTranslationContract);
            }

            [Test]
            public void ThenTheResponseContainsOneTurkishTopicCategoryTranslationContractWithTheIsTranslatedFlagSetToTrue()
            {
                const int expected = 1;

                var topicCategoryTranslationContract =
                    _completeTopicCategoryTranslationResponseContract.TopicCategoryTranslations.Count(
                        a => a.Culture == "tr" && a.IsTranslated);

                Assert.AreEqual(expected, topicCategoryTranslationContract);
            }

            [Test]
            public void ThenTheResponseContainsOneGlobalEnglishTopicCategoryTranslationContract()
            {
                const int expected = 1;

                var topicCategoryTranslationContract =
                    _completeTopicCategoryTranslationResponseContract.TopicCategoryTranslations.Count(
                        a => a.Culture == ConstantProvider.GlobalCulture);

                Assert.AreEqual(expected, topicCategoryTranslationContract);
            }

            [Test]
            public void ThenTheResponseContainsOneGlobalEnglishTopicCategoryTranslationContractWithTheIsTranslatedFlagSetToFalse()
            {
                const int expected = 1;

                var topicCategoryTranslationContract =
                    _completeTopicCategoryTranslationResponseContract.TopicCategoryTranslations.Count(
                        a => a.Culture == ConstantProvider.GlobalCulture && !a.IsTranslated);

                Assert.AreEqual(expected, topicCategoryTranslationContract);
            }

            [Test]
            public void ThenEnglishDisplayNameIsReturnedCorrectly()
            {
                Assert.AreEqual(DisplayName1Decrypted, _globalEnglishTopicCategoryTranslationContract.UpdatedByUserName);
            }

            [Test]
            public void ThenTurkishDisplayNameIsReturnedCorrectly()
            {
                Assert.AreEqual(DisplayName3Decrypted, _turkishTopicCategoryTranslationContract.UpdatedByUserName);
            }

            [Test]
            public void ThenEnglishTopicCategoryNameIsReturnedCorrectly()
            {
                const string expected = "topicCat1EnCategoryName";

                Assert.AreEqual(expected, _globalEnglishTopicCategoryTranslationContract.TopicCategoryName);
            }

            [Test]
            public void ThenTurkishTopicCategoryNameIsReturnedCorrectly()
            {
                const string expected = "topicCat2TrCategoryName";

                Assert.AreEqual(expected, _turkishTopicCategoryTranslationContract.TopicCategoryName);
            }
        }

        public class WhenTranslationsWithMultipleStatusesAreReturnedFromTheDatabase : GivenGettingAllTranslatedTopicCategories
        {
            protected override async void When()
            {
                TopicCategoryTranslations = new List<TopicCategoryTranslation>
                {
                    new TopicCategoryTranslation {TopicCategoryTranslationId = 1, Status = Status.Deleted, Culture = "en", Name = "topicCat1EnCategoryName",TopicCategoryId = 1, TopicCategory = new TopicCategory{TopicCategoryId = 1}, User = new User {DisplayName = "topicCat1EnName"}},
                    new TopicCategoryTranslation {TopicCategoryTranslationId = 2, Status = Status.Deleted,Culture = "en", Name = "topicCat2EnCategoryName", TopicCategoryId = 2, TopicCategory = new TopicCategory{TopicCategoryId = 2}, User = new User{DisplayName = "topicCat2EnName"}},
                    new TopicCategoryTranslation {TopicCategoryTranslationId = 3, Status = Status.Deleted,Culture = "tr", Name = "topicCat2TrCategoryName", TopicCategoryId = 2, TopicCategory = new TopicCategory{TopicCategoryId = 2}, User = new User{DisplayName = "topicCat2TrName"}},
                    new TopicCategoryTranslation {TopicCategoryTranslationId = 4, Status = Status.Live,Culture = "en", Name = "topicCat3EnCategoryName",TopicCategoryId = 3, TopicCategory = new TopicCategory{TopicCategoryId = 3}, User = new User {DisplayName = "topicCat3EnName"}},
                    new TopicCategoryTranslation {TopicCategoryTranslationId = 5, Status = Status.Live,Culture = "en", Name = "topicCat4EnCategoryName", TopicCategoryId = 4, TopicCategory = new TopicCategory{TopicCategoryId = 4}, User = new User{DisplayName = "topicCat4EnName"}},
                    new TopicCategoryTranslation {TopicCategoryTranslationId = 6, Status = Status.Deleted,Culture = "tr", Name = "topicCat4TrCategoryName", TopicCategoryId = 4, TopicCategory = new TopicCategory{TopicCategoryId = 4}, User = new User{DisplayName = "topicCat4TrName"}}
                };

                Culture = "tr";

                PrepareSut();

                _completeTopicCategoryTranslationResponseContract = await SUT.GetAllTopicCategories(Culture);

            }

            [Test]
            public void ThenTheCorrectAmountOfTopicCategoryTranslationsAreReturned()
            {
                const int expected = 2;

                Assert.AreEqual(expected, _completeTopicCategoryTranslationResponseContract.TopicCategoryTranslations.Count);
            }

            [Test]
            public void ThenTheResponseContainsNoTurkishTopicCategoryTranslationContracts()
            {
                const int expected = 0;

                var topicCategoryTranslationContract =
                    _completeTopicCategoryTranslationResponseContract.TopicCategoryTranslations.Count(
                        a => a.Culture == "tr");

                Assert.AreEqual(expected, topicCategoryTranslationContract);
            }

            [Test]
            public void ThenTheResponseContainsTwoGlobalEnglishTopicCategoryTranslationContract()
            {
                const int expected = 2;

                var topicCategoryTranslationContract =
                    _completeTopicCategoryTranslationResponseContract.TopicCategoryTranslations.Count(
                        a => a.Culture == ConstantProvider.GlobalCulture);

                Assert.AreEqual(expected, topicCategoryTranslationContract);
            }

            [Test]
            public void ThenTheResponseContainsTwoGlobalEnglishTopicCategoryTranslationContractWithTheIsTranslatedFlagSetToFalse()
            {
                const int expected = 2;

                var topicCategoryTranslationContract =
                    _completeTopicCategoryTranslationResponseContract.TopicCategoryTranslations.Count(
                        a => a.Culture == ConstantProvider.GlobalCulture && !a.IsTranslated);

                Assert.AreEqual(expected, topicCategoryTranslationContract);
            }

            [Test]
            public void ThenTheCorrectTopicCategoryNamesAreReturned()
            {
                var actual =
                    _completeTopicCategoryTranslationResponseContract.TopicCategoryTranslations.Select(
                        a => a.TopicCategoryName);

                var expected = new List<string> { "topicCat3EnCategoryName", "topicCat4EnCategoryName" };

                CollectionAssert.AreEquivalent(expected, actual);
            }

        }
    }
}
