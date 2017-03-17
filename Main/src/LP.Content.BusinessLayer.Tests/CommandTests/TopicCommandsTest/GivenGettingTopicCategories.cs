using System.Collections.Generic;
using System.Linq;
using LP.EntityModels;
using LP.ServiceHost.DataContracts.Response.Content;
using Moq;
using NUnit.Framework;

namespace LP.Content.BusinessLayer.Tests.CommandTests.TopicCommandsTest
{
    public class GivenGettingTopicCategories : BaseGiven
    {
        private TopicCategoriesResponseContract _topicCategoriesResponseContract;
        protected override void Given()
        {
            TopicCategories = new List<TopicCategory>()
            {
                new TopicCategory()
                {
                    TopicCategoryId = 1,
                    SortOrder = 1
                },
                new TopicCategory()
                {
                    TopicCategoryId = 2,
                    SortOrder = 2
                },
                new TopicCategory()
                {
                    TopicCategoryId = 3,
                    SortOrder = 3
                },
                new TopicCategory()
                {
                    TopicCategoryId = 4,
                    SortOrder = 4
                },
                new TopicCategory()
                {
                    TopicCategoryId = 5,
                    SortOrder = 5
                },
            };
            PrepareSut();
        }

        public class WhenTheTopicCategoriesAreRequested : GivenGettingTopicCategories
        {
            protected override async void When()
            {
                _topicCategoriesResponseContract = await SUT.GetAllTopicCategories("en");
            }

            [Test]
            public void ThenGetAllAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<TopicCategoryTranslation>(), Times.Once());
            }

            [Test]
            public void ThenTopicCategoriesResponseContractIsNotNull()
            {
                Assert.IsNotNull(_topicCategoriesResponseContract);
            }

            [Test]
            public void ThenTheCorrectAmountOfTopicCategoriesAreReturned()
            {
                const int expected = 1;

                Assert.AreEqual(expected, _topicCategoriesResponseContract.TopicCategories.Count);
            }

            [Test]
            public void ThenTheCorrectTopicCategoriesAreReturned()
            {
                var expected = new List<string> { "test" };

                var actual = _topicCategoriesResponseContract.TopicCategories.Select(x => x.CategoryName).ToList();

                CollectionAssert.AreEquivalent(expected, actual);
            }
        }
    }
}
