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
    public class GivenDeletingTopics : BaseGiven
    {
        private TopicDeleteResponseContract _topicDeleteResponseContract;

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

        public class WhenTheTopicDeletedExists : GivenDeletingTopics
        {
            protected async override void When()
            {
                _topicDeleteResponseContract = await SUT.DeleteTopic("en", 1);
            }

            [Test]
            public void ThenSaveChangesIsCalledExactlyExpectedTimes()
            {
                const int expected = 2;
                BaseCommandsMock.Verify(m => m.SaveChanges(), Times.Exactly(expected));
            }

            [Test]
            public void ThenDeleteTopicResponseIsTrue()
            {
                Assert.IsTrue(_topicDeleteResponseContract.Result);
            }

            [Test]
            public void ThenUpdateIsCalledAtLeastOnce()
            {
                BaseCommandsMock.Verify(m => m.Update(It.IsAny<TopicTranslation>()), Times.AtLeastOnce);
            }
        }
    }
}
