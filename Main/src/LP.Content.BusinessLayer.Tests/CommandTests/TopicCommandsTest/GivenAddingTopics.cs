using System;
using LP.EntityModels;
using LP.ServiceHost.DataContracts.Response.Content;
using Moq;
using NUnit.Framework;

namespace LP.Content.BusinessLayer.Tests.CommandTests.TopicCommandsTest
{
    public class GivenAddingTopics : BaseGiven
    {
        private TopicResponseContract _topicResponseContract;
        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenTheAddTopicIsAdded : GivenAddingTopics
        {
            protected override async void When()
            {
                _topicResponseContract = await SUT.AddTopic("en", "Topic 1", 1, new DateTime(), 1);
            }

            //[Test]
            //public void ThenGetAllAsyncIsCalledOnce()
            //{
            //    BaseCommandsMock.Verify(m => m.GetAllAsync<Topic>(), Times.Once());
            //}

            [Test]
            public void ThenSaveChangesIsCalledOnce()
            {
                const int expected = 2;
                BaseCommandsMock.Verify(m => m.SaveChanges(), Times.Exactly(expected));
            }

            [Test]
            public void ThenAddTopicsResponseIsTrue()
            {
                Assert.IsTrue(_topicResponseContract.Result);
            }
        }
    }
}
