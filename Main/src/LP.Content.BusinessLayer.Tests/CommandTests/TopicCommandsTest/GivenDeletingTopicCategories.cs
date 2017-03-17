using LP.ServiceHost.DataContracts.Response.Content;
using Moq;
using NUnit.Framework;

namespace LP.Content.BusinessLayer.Tests.CommandTests.TopicCommandsTest
{
    public class GivenDeletingTopicCategories : BaseGiven
    {
        private TopicCategoryDeleteResponseContract _topicCategoryDeleteResponseContract;
        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenTheTopicCategoryDeletedExists : GivenDeletingTopicCategories
        {
            protected async override void When()
            {
                _topicCategoryDeleteResponseContract = await SUT.DeleteTopicCategory("en", 1);
            }

            [Test]
            public void ThenSaveChangesIsCalled()
            {
                var expected = 2;
                BaseCommandsMock.Verify(m => m.SaveChanges(), Times.Exactly(expected));
            }


            [Test]
            public void ThenUpdateTopicCategoryIsCalledOnlyOnce()
            {
                BaseCommandsMock.Verify(m => m.Update(TopicCategory), Times.Once());
            }

            //[Test]
            //public void ThenUpdateTopicCategoryTranslationIsCalledAtLeastOnce()
            //{
            //    BaseCommandsMock.Verify(m => m.Update(DeletedTopicCategoryTranslation), Times.AtLeastOnce);
            //}
            
            [Test]
            public void ThenAddTopicCategoriesResponseIsTrue()
            {
                Assert.IsTrue(_topicCategoryDeleteResponseContract.Result);
            }
        }
        public class WhenTheTopicCategoryDeletedDoesNotExists : GivenDeletingTopicCategories
        {
            protected async override void When()
            {
                _topicCategoryDeleteResponseContract = await SUT.DeleteTopicCategory("en", -1);
            }
            
            [Test]
            public void ThenAddTopicCategoriesResponseIsFalse()
            {
                Assert.IsFalse(_topicCategoryDeleteResponseContract.Result);
            }
        }
    }
}
