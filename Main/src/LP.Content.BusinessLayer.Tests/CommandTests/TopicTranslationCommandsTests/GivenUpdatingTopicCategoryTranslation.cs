using System.Collections.Generic;
using LP.EntityModels;
using LP.ServiceHost.DataContracts.Enums;
using LP.ServiceHost.DataContracts.Response.Content;
using Moq;
using NUnit.Framework;

namespace LP.Content.BusinessLayer.Tests.CommandTests.TopicTranslationCommandsTests
{
    public class GivenUpdatingTopicCategoryTranslation : BaseGiven
    {
        private TopicCategoryTranslationUpdateResponseContract _topicCategoryTranslationUpdateResponseContract;

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenTheTopicCategoryTranslationIsIsUpdated : GivenUpdatingTopicCategoryTranslation
        {
            protected override async void When()
            {

                TopicCategoryTranslations = new List<TopicCategoryTranslation>
                {
                    TopicCategoryTranslationSingle
                }; 

                PrepareSut();

                _topicCategoryTranslationUpdateResponseContract = await SUT.UpdateTopicCategoryTranslation(TopicCategoryTranslationSingle.Culture, TopicCategoryTranslationSingle.Name, TopicCategoryTranslationSingle.TopicCategoryId, TopicCategoryTranslationSingle.LastUpdatedByUserId, TopicCategoryTranslationSingle.Status);
            }

            [Test]
            public void ThenGetAllAsyncIsCalledTwoTimes()
            {
                const int expected = 2;
                BaseCommandsMock.Verify(m => m.GetAllAsync<TopicCategoryTranslation>(), Times.Exactly(expected));
            }

            [Test]
            public void ThenTopicCategoryTranslationUpdateResponseContractIsNotNull()
            {
                Assert.IsNotNull(_topicCategoryTranslationUpdateResponseContract);
            }

            [Test]
            public void ThenUpdateIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.Update(It.IsAny<TopicCategoryTranslation>()), Times.Once());
            }

            [Test]
            public void ThenSaveChangesIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.SaveChanges(), Times.Once());
            }

            [Test]
            public void ThenAddTopicCategoryTranslationResponseIsTrue()
            {
                Assert.IsTrue(_topicCategoryTranslationUpdateResponseContract.Result);
            }
        }
    }
}