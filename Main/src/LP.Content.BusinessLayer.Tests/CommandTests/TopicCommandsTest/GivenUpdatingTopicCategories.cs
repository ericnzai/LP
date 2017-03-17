using LP.ServiceHost.DataContracts.Response.Content;
using Moq;
using NUnit.Framework;

namespace LP.Content.BusinessLayer.Tests.CommandTests.TopicCommandsTest
{
    public class GivenUpdatingTopicCategories : BaseGiven
    {
        private TopicCategoryUpdateResponseContract _topicCategoryUpdateResponseContract;
        
        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenTheTopicCategoryUpdatedExists : GivenUpdatingTopicCategories
        {
            protected async override void When()
            {
                _topicCategoryUpdateResponseContract = await SUT.UpdateTopicCategory("en", 1, "test 1");
            }

            [Test]
            public void ThenSaveChangesIsCalledOnlyOnce()
            {
                BaseCommandsMock.Verify(m => m.SaveChanges(), Times.Once());
            }

            [Test]
            public void ThenAddTopicCategoriesResponseIsTrue()
            {
                Assert.IsTrue(_topicCategoryUpdateResponseContract.Result);
            }
        }

        public class WhenTheTopicCategoryUpdatedDoesExists : GivenUpdatingTopicCategories
        {
            protected async override void When()
            {
                _topicCategoryUpdateResponseContract = await SUT.UpdateTopicCategory("en", -1, "test 1");
            }

            [Test]
            public void ThenAddTopicCategoriesResponseIsFalse()
            {
                Assert.IsFalse(_topicCategoryUpdateResponseContract.Result);
            }
        }


        public class WhenTheTopicCategoryNameAlreadyExists : GivenUpdatingTopicCategories
        {
            protected async override void When()
            {
                _topicCategoryUpdateResponseContract = await SUT.UpdateTopicCategory("en", 1, "test 1");
            }

            [Test]
            public void ThenSaveChangesIsCalledOnlyOnce()
            {
                BaseCommandsMock.Verify(m => m.SaveChanges(), Times.Once());
            }

            [Test]
            public void ThenAddTopicCategoriesResponseIsTrue()
            {
                Assert.IsTrue(_topicCategoryUpdateResponseContract.Result);
            }
        }
    }
}
