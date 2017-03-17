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
    public class GivenGettingTopicByIdAndCulture : BaseGiven
    {
        private TopicTranslationFormResponseContract _topicTranslationFormResponseContract;

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenCorrectMethodsAndIncludesShouldBeReturned : GivenGettingTopicByIdAndCulture
        {
            protected override async void When()
            {
                PrepareSut();

                _topicTranslationFormResponseContract =
                    await SUT.GetTopicByIdAndCulture(TopicId, Culture);

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
            public void ThenTopicTranslationFormResponseContractIsNotNull()
            {
                Assert.IsNotNull(_topicTranslationFormResponseContract);
            }

        }

        public class WhenOneTranslatedItemShouldBeReturned :
            GivenGettingTopicByIdAndCulture
        {
            protected override async void When()
            {
                TopicTranslations = new List<TopicTranslation>
                {
                    new TopicTranslation {Culture = Culture, Name = "topic1EnName", TopicId = TopicId, Topic = TopicSingle, User = new User{DisplayName = "topic1EnName"}}
                };

                PrepareSut();

                _topicTranslationFormResponseContract =
                    await SUT.GetTopicByIdAndCulture(TopicId, Culture);

            }

            [Test]
            public void ThenGetAllTopicsAsyncIsNeverCalled()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<Topic>(), Times.Never);
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
            public void ThenEncryptionHandlerDecryptStringIsCalledTwiceForGlobalAndLocalTranslation()
            {
                const int expected = 2;
                EncryptionHandlerMock.Verify(m => m.DecryptString(It.IsAny<string>()), Times.Exactly(expected));
            }

        }

        public class WhenEmptyTranslatedItemShouldBeReturned :
            GivenGettingTopicByIdAndCulture
        {
            protected override async void When()
            {
                TopicTranslations = new List<TopicTranslation>
                {
                    new TopicTranslation {Culture = "en", Name = "topic1EnName", TopicId = TopicId, Topic = TopicSingle, User = new User{DisplayName = "topic1EnName"}}
                };

                PrepareSut();

                Culture = CultureTr;
                _topicTranslationFormResponseContract =
                    await SUT.GetTopicByIdAndCulture(TopicId, Culture);

            }

            [Test]
            public void ThenGetAllTopicsAsyncIsNeverCalled()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<Topic>(), Times.Never);
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
            public void ThenEncryptionHandlerDecryptStringIsCalledOnceOnlyForGlobalTranslation()
            {
                EncryptionHandlerMock.Verify(m => m.DecryptString(It.IsAny<string>()), Times.Once);
            }

            [Test]
            public void ThenTheTopicTranslationNameIsEmpryString()
            {
                const string expected = "";
                Assert.AreEqual(expected, _topicTranslationFormResponseContract.LocalTopicTranslation.TopicName);
            }

            [Test]
            public void ThenTheTopicTranslationStatusIsTranslationInProgress()
            {
                const Status expected = Status.TranslationInProgress;
                Assert.AreEqual(expected, _topicTranslationFormResponseContract.LocalTopicTranslation.Status);
            }

            [Test]
            public void ThenTheTopicTranslationCultureIsExpectedLocalCulture()
            {
                Assert.AreEqual(CultureTr, _topicTranslationFormResponseContract.LocalTopicTranslation.Culture);
            }

            [Test]
            public void ThenTheTopicTranslationUserUpdatedByNameIsCorrect()
            {
                const string expected = "";
                Assert.AreEqual(expected, _topicTranslationFormResponseContract.LocalTopicTranslation.UpdatedByUserName);
            }

            [Test]
            public void ThenTheTopicTranslationIsTranslatedIsFalse()
            {
                Assert.AreEqual(false, _topicTranslationFormResponseContract.LocalTopicTranslation.IsTranslated);
            }

            [Test]
            public void ThenTheTopicTranslationCultureDisplayNameIsCorrect()
            {
                CultureProviderMock.Verify(m => m.GetCultureDisplayName(CultureTr), CultureDisplayNameTr);
            }

        }
    }
}
