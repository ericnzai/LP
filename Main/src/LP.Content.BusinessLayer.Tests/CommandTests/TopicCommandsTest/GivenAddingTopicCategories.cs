using LP.EntityModels;
using LP.ServiceHost.DataContracts.Response.Content;
using Moq;
using NUnit.Framework;

namespace LP.Content.BusinessLayer.Tests.CommandTests.TopicCommandsTest
{
    public class GivenAddingTopicCategories : BaseGiven
    {
        private TopicCategoryResponseContract _topicCategoryResponseContract;
        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenTheAddTopicCategoryIsAdded : GivenAddingTopicCategories
        {
            protected override async void When()
            {
                _topicCategoryResponseContract = await SUT.AddTopicCategory("en", "Category 1");
            }

            [Test]
            public void ThenGetAllAsyncIsCalledOnce()
            {
                var expected = 2;
                BaseCommandsMock.Verify(m => m.GetAllAsync<TopicCategoryTranslation>(), Times.Exactly(expected));
            }

            [Test]
            public void ThenSaveChangesIsCalled()
            {
                var expected = 2;
                BaseCommandsMock.Verify(m => m.SaveChanges(), Times.Exactly(expected));
            }

            [Test]
            public void ThenAddTopicCategoriesResponseIsTrue()
            {
                Assert.IsTrue(_topicCategoryResponseContract.Result);
            }
        }
    }
}
