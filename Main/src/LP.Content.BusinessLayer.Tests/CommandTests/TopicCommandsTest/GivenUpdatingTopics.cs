using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LP.EntityModels;
using LP.ServiceHost.DataContracts.Enums;
using LP.ServiceHost.DataContracts.Response.Content;
using Moq;
using NUnit.Framework;

namespace LP.Content.BusinessLayer.Tests.CommandTests.TopicCommandsTest
{
    public class GivenUpdatingTopics : BaseGiven
    {
        private TopicUpdateResponseContract _topicUpdateResponseContract;

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

        public class WhenTheTopicUpdatedExists : GivenUpdatingTopics
        {
            protected async override void When()
            {
                _topicUpdateResponseContract = await SUT.UpdateTopic("en", 1, "test 1", 1);
            }

            [Test]
            public void ThenSaveChangesIsCalledExpectedTimes()
            {
                const int expected = 2;
                BaseCommandsMock.Verify(m => m.SaveChanges(), Times.Exactly(expected));
            }

            [Test]
            public void ThenAddTopicCategoriesResponseIsTrue()
            {
                Assert.IsTrue(_topicUpdateResponseContract.Result);
            }
        }

        public class WhenTheTopicUpdatedDoesExists : GivenUpdatingTopics
        {
            protected async override void When()
            {
                _topicUpdateResponseContract = await SUT.UpdateTopic("en", -1, "test 1", 1);
            }

            [Test]
            public void ThenAddTopicResponseIsFalse()
            {
                Assert.IsFalse(_topicUpdateResponseContract.Result);
            }
        }


        public class WhenTheTopicCategoryNameAlreadyExists : GivenUpdatingTopics
        {
            protected async override void When()
            {
                _topicUpdateResponseContract = await SUT.UpdateTopic("en", 1, "test 1",1);
            }

            [Test]
            public void ThenSaveChangesIsCalledExpectedTimes()
            {
                const int expected = 2;
                BaseCommandsMock.Verify(m => m.SaveChanges(), Times.Exactly(expected));
            }

            [Test]
            public void ThenAddTopicCategoriesResponseIsTrue()
            {
                Assert.IsTrue(_topicUpdateResponseContract.Result);
            }
        }
    }
}
