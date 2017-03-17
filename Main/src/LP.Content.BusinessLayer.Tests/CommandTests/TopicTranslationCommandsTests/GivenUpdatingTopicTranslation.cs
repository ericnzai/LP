using LP.EntityModels;
using LP.ServiceHost.DataContracts.Response.Content;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace LP.Content.BusinessLayer.Tests.CommandTests.TopicTranslationCommandsTests
{
    public class GivenUpdatingTopicTranslation : BaseGiven
    {
        private TopicTranslationUpdateResponseContract _topicTranslationUpdateResponseContract;

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenTheTopicTranslationIsIsUpdated : GivenUpdatingTopicTranslation
        {
            protected override async void When()
            {

                TopicTranslations = new List<TopicTranslation>
                {
                    TopicTranslationSingle
                };

                PrepareSut();

                _topicTranslationUpdateResponseContract = await SUT.UpdateTopicTranslation(TopicTranslationSingle.Culture, TopicTranslationSingle.Name, TopicTranslationSingle.TopicId, TopicTranslationSingle.LastUpdatedByUserId, TopicTranslationSingle.Status);
            }

            [Test]
            public void ThenGetAllAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<TopicTranslation>(), Times.Once);
            }

            [Test]
            public void ThenTopicTranslationUpdateResponseContractIsNotNull()
            {
                Assert.IsNotNull(_topicTranslationUpdateResponseContract);
            }

            [Test]
            public void ThenUpdateIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.Update(It.IsAny<TopicTranslation>()), Times.Once());
            }

            [Test]
            public void ThenSaveChangesIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.SaveChanges(), Times.Once());
            }

            [Test]
            public void ThenAddTopicTranslationResponseIsTrue()
            {
                Assert.IsTrue(_topicTranslationUpdateResponseContract.Result);
            }
        }
    }
}
