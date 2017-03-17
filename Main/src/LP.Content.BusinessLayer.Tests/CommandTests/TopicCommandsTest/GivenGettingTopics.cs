using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LP.EntityModels;
using LP.ServiceHost.DataContracts.Enums;
using LP.ServiceHost.DataContracts.Response.Content;
using Moq;
using NUnit.Framework;

namespace LP.Content.BusinessLayer.Tests.CommandTests.TopicCommandsTest
{
    public class GivenGettingTopics : BaseGiven
    {
        private TopicsResponseContract _topicsResponseContract;
        protected override void Given()
        {
            Topics = new List<TopicTranslation>()
            {
                new TopicTranslation()
                {
                    Name = "Topic 1",
                    Culture = "en",
                    Status = Status.Live,
                    TopicId = 1,
                    Topic = new Topic()
                    {
                        TopicCategoryId = 1
                    }
                },
                new TopicTranslation()
                {
                    Name = "Topic 2",
                    Culture = "en",
                    Status = Status.Live,
                    TopicId = 2,
                    Topic = new Topic()
                    {
                        TopicCategoryId = 1
                    }
                },
                new TopicTranslation()
                {
                    Name = "Topic 3",
                    Culture = "en",
                    Status = Status.Live,
                    TopicId = 3,
                    Topic = new Topic()
                    {
                        TopicCategoryId = 1
                    }
                },
            };

            PrepareSut();
        }

        public class WhenTheATopicCategoryIsAdded : GivenGettingTopics
        {
            protected override async void When()
            {
                _topicsResponseContract = await SUT.GetAllTopics("en");
            }

            //[Test]
            //public void ThenGetConditionalWithIncludesIsCalledOnce()
            //{
            //    BaseCommandsMock.Verify(m => m.GetWithIncludesAsync<Topic>(It.IsAny<Expression<Func<Topic, object>>[]>()), Times.Once());
            //}
            [Test]
            public void ThenGetAllAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<TopicTranslation>(), Times.Once());
            }

            [Test]
            public void ThenTopicsResponseContractIsNotNull()
            {
                Assert.IsNotNull(_topicsResponseContract);
            }

            [Test]
            public void ThenTheCorrectAmountOfTopicsAreReturned()
            {
                const int expected = 3;

                Assert.AreEqual(expected, _topicsResponseContract.Topics.Count);
            }

            [Test]
            public void ThenTheCorrectTopicsAreReturned()
            {
                var expected = new List<string> { "Topic 1", "Topic 2", "Topic 3" };

                var actual = _topicsResponseContract.Topics.Select(x => x.TopicName).ToList();

                CollectionAssert.AreEquivalent(expected, actual);
            }
        }
    }
}
