using LP.Api.Shared.Providers;
using LP.EntityModels;
using LP.ServiceHost.DataContracts.Common.Content;
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
    public class GivenGettingAllTranslatedTopics : BaseGiven
    {
        private CompleteTopicTranslationResponseContract _completeTopicTranslationResponseContract;

        
        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenCorrectMethodsAndIncludesShouldBeReturned : GivenGettingAllTranslatedTopics
        {
            protected override async void When()
            {
                TopicTranslations = new List<TopicTranslation>();

                PrepareSut();

                _completeTopicTranslationResponseContract = await SUT.GetAllTopics(Culture);
            }

            [Test]
            public void ThenGetAllTopicsAsyncIsNeverCalled()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<LP.EntityModels.Topic>(), Times.Never());
            }

            [Test]
            public void ThenGetWithIncludesAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetWithIncludesAsync(It.IsAny<Expression<Func<TopicTranslation, object>>[]>()), Times.Once());

            }

            [Test]
            public void ThenGetWithIncludesAsyncIsCalledOnceWithTheCorrectParameters()
            {
                BaseCommandsMock.Verify(m => m.GetWithIncludesAsync<TopicTranslation>(inc => inc.Topic, inc => inc.User, inc => inc.User.askCore_UserDetails), Times.Once());

            }

            [Test]
            public void ThenEncryptionHandlerDecryptStringIsNeverCalled()
            {
                EncryptionHandlerMock.Verify(m => m.DecryptString(It.IsAny<string>()), Times.Never());
            }

            [Test]
            public void ThenCompleteTopicCategoryTranslationResponseContractIsNotNull()
            {
                Assert.IsNotNull(_completeTopicTranslationResponseContract);
            }
        }

        public class WhenOneTranslatedItemAndOneNotTranslatedItemShouldBeReturned :
            GivenGettingAllTranslatedTopics
        {
            private TopicTranslationContract _turkishTopicTranslationContract;
            private TopicTranslationContract _globalEnglishTopicTranslationContract;


            protected override async void When()
            {
                TopicTranslations = new List<TopicTranslation>
                {
                    new TopicTranslation
                    {
                        Culture = ConstantProvider.GlobalCulture,
                        Name = "Topic Translation EN (Topic 1)",
                        TopicId = 1,
                        User = new User {DisplayName = "topic1EnName"},
                        Status = Status.Live
                    },
                    new TopicTranslation
                    {
                        Culture = "tr",
                        Name = "Topic Translation TR (Topic 1)",
                        TopicId = 1,
                        User = new User {DisplayName = "topic1TrName"},
                        Status = Status.Live
                    },
                    new TopicTranslation
                    {
                        Culture = "es",
                        Name = "Topic Translation ES (Topic 1)",
                        TopicId = 2,
                        User = new User {DisplayName = "topic1EsName"},
                        Status = Status.Live
                    },
                    new TopicTranslation
                    {
                        Culture = ConstantProvider.GlobalCulture,
                        Name = "Topic Translation EN (Topic 2)",
                        TopicId = 2,
                        User = new User {DisplayName = "topic2EnName"},
                        Status = Status.Live
                    },
                    new TopicTranslation
                    {
                        Culture = ConstantProvider.GlobalCulture,
                        Name = "Topic Translation EN (Topic 3)",
                        TopicId = 3,
                        User = new User {DisplayName = "topic3EnName"},
                        Status = Status.Live
                    }
                };

                Culture = "tr";

                PrepareSut();

                _completeTopicTranslationResponseContract = await SUT.GetAllTopics(Culture);

                _turkishTopicTranslationContract =
                  _completeTopicTranslationResponseContract.TopicTranslations.FirstOrDefault(
                      a => a.Culture == "tr");

                _globalEnglishTopicTranslationContract =
                 _completeTopicTranslationResponseContract.TopicTranslations.FirstOrDefault(
                     a => a.Culture == "en");

            }

            [Test]
            public void ThenGetAllTopicsAsyncIsNeverCalled()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<TopicTranslation>(), Times.Never());
            }


            [Test]
            public void ThenGetWithIncludesAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetWithIncludesAsync(It.IsAny<Expression<Func<TopicTranslation, object>>[]>()), Times.Once());

            }

            [Test]
            public void ThenGetWithIncludesAsyncIsCalledOnceWithTheCorrectParameters()
            {
                BaseCommandsMock.Verify(m => m.GetWithIncludesAsync<TopicTranslation>(inc => inc.Topic, inc => inc.User, inc => inc.User.askCore_UserDetails), Times.Once());

            }

            [Test]
            public void ThenEncryptionHandlerDecryptStringIsCalledTreeTimes()
            {
                const int expected = 3;
                EncryptionHandlerMock.Verify(m => m.DecryptString(It.IsAny<string>()), Times.Exactly(expected));
            }

            [Test]
            public void ThenEncryptionHandlerDecryptStringIsNeverCalledForTheGlobalEnglishTransltionByTopic()
            {
                EncryptionHandlerMock.Verify(m => m.DecryptString(It.Is<string>(x => x == "topic1EnName")), Times.Never());
            }

            [Test]
            public void ThenEncryptionHandlerDecryptStringIsNeverCalledForTheSpanishTransltionByTopic()
            {
                EncryptionHandlerMock.Verify(m => m.DecryptString(It.Is<string>(x => x == "topic1EsName")), Times.Never());
            }

            [Test]
            public void ThenEncryptionHandlerDecryptStringIsCalledOnceForTheTurkishTransltion()
            {
                EncryptionHandlerMock.Verify(m => m.DecryptString(It.Is<string>(x => x == "topic1TrName")), Times.Once());
            }

            [Test]
            public void ThenTheListOfTopicTranslationResponseContractIsNotNull()
            {
                Assert.IsNotNull(_completeTopicTranslationResponseContract);
            }

            [Test]
            public void ThenTheCorrectAmountOfTopicTranslationsAreReturned()
            {
                const int expected = 3;

                Assert.AreEqual(expected, _completeTopicTranslationResponseContract.TopicTranslations.Count);
            }

            [Test]
            public void ThenTheResponseContainsOneTurkishTopicTranslationContract()
            {
                const int expected = 1;

                var topicTranslationContract =
                    _completeTopicTranslationResponseContract.TopicTranslations.Count(
                        a => a.Culture == "tr");

                Assert.AreEqual(expected, topicTranslationContract);
            }

            [Test]
            public void ThenTheResponseContainsOneTurkishTopicTranslationContractWithTheIsTranslatedFlagSetToTrue()
            {
                const int expected = 1;

                var topicTranslationContract =
                    _completeTopicTranslationResponseContract.TopicTranslations.Count(
                        a => a.Culture == "tr" && a.IsTranslated);

                Assert.AreEqual(expected, topicTranslationContract);
            }

            [Test]
            public void ThenTheResponseContainsOneGlobalEnglishTopicTranslationContract()
            {
                const int expected = 2;

                var topicTranslationContract =
                    _completeTopicTranslationResponseContract.TopicTranslations.Count(
                        a => a.Culture == ConstantProvider.GlobalCulture);

                Assert.AreEqual(expected, topicTranslationContract);
            }

            [Test]
            public void ThenTheResponseContainsOneGlobalEnglishTopicTranslationContractWithTheIsTranslatedFlagSetToFalse()
            {
                const int expected = 2;

                var topicTranslationContract =
                    _completeTopicTranslationResponseContract.TopicTranslations.Count(
                        a => a.Culture == ConstantProvider.GlobalCulture && !a.IsTranslated);

                Assert.AreEqual(expected, topicTranslationContract);
            }

            [Test]
            public void ThenTheCorrectEnCultureReturned()
            {
                const string expected = "en";

                Assert.AreEqual(expected, _globalEnglishTopicTranslationContract.Culture);
            }

            [Test]
            public void ThenTheCorrectTrCultureReturned()
            {
                const string expected = "tr";

                Assert.AreEqual(expected, _turkishTopicTranslationContract.Culture);
            }
        }
    }
}
