using LP.EntityModels;
using LP.ServiceHost.DataContracts.Enums;
using LP.ServiceHost.DataContracts.Response.Content;
using Moq;
using NUnit.Framework;

namespace LP.Content.BusinessLayer.Tests.CommandTests.TopicTranslationCommandsTests
{
    public class GivenAddingTopicCategoryTranslation : BaseGiven
    {
        private TopicCategoryTranslationResponseContract _topicCategoryTranslationResponseContract;

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenTheTopicCategoryTranslationIsIsAdded : GivenAddingTopicCategoryTranslation
        {
            protected override async void When()
            {
                _topicCategoryTranslationResponseContract = await SUT.AddTopicCategoryTranslation("en", "Category 1", TopicCategoryId, UserId, Status.TranslationInProgress);
            }

            [Test]
            public void ThenGetAllAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<TopicCategoryTranslation>(), Times.Once());
            }

            [Test]
            public void ThenSaveChangesIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.SaveChangesAsync(), Times.Once());
            }

            [Test]
            public void ThenAddTopicCategoryTranslationResponseIsTrue()
            {
                Assert.IsTrue(_topicCategoryTranslationResponseContract.Result);
            }
        }
    }
}
