using LP.EntityModels;
using LP.ServiceHost.DataContracts.Enums;
using LP.ServiceHost.DataContracts.Response.Content;
using Moq;
using NUnit.Framework;

namespace LP.Content.BusinessLayer.Tests.CommandTests.TopicTranslationCommandsTests
{
    public class GivenAddingTopicTranslation : BaseGiven
    {
        private TopicTranslationResponseContract _topicTranslationResponseContract;

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenTheTopicTranslationIsIsAdded : GivenAddingTopicTranslation
        {
            protected override async void When()
            {
                _topicTranslationResponseContract = await SUT.AddTopicTranslation("en", "Topic 1", TopicId, UserId, Status.TranslationInProgress);
            }

            [Test]
            public void ThenGetAllAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<TopicTranslation>(), Times.Once());
            }

            [Test]
            public void ThenSaveChangesIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.SaveChangesAsync(), Times.Once());
            }

            [Test]
            public void ThenAddTopicTranslationResponseIsTrue()
            {
                Assert.IsTrue(_topicTranslationResponseContract.Result);
            }
        }
    }
}
