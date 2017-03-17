using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using LP.Model.Authentication;
using LP.ServiceHost.DataContracts.Common.Exams;
using LP.ServiceHost.DataContracts.Response.Exams;
using Moq;
using NUnit.Framework;

namespace LP.Exams.Tests.ControllerTests.TrainingArea.Progress
{
    public class GivenGettingProgressForATrainingArea : BaseGiven
    {
        private IHttpActionResult _httpActionResult;
        private OkNegotiatedContentResult<TrainingAreaProgressResponseContract> _negotiatedContentResult;
        private const int TrainingAreaId = 45;
        private const string TrainingAreaName = "TrainingArea45";
        
        protected override void Given()
        {
            TrainingAreaProgressResponseContract = new TrainingAreaProgressResponseContract
            {
                GroupProgressContracts = new List<GroupProgressContract>
                {
                    new GroupProgressContract(),
                    new GroupProgressContract(),
                    new GroupProgressContract(),
                },
                TrainingAreaId = TrainingAreaId,
                TrainingAreaName = TrainingAreaName
            };

            PrepareSut();
        }

        public class WhenTheTrainingAreExists : GivenGettingProgressForATrainingArea
        {
            protected override async void When()
            {
                _httpActionResult = await SUT.Get(1);

                _negotiatedContentResult = _httpActionResult as OkNegotiatedContentResult<TrainingAreaProgressResponseContract>;
            }

            [Test]
            public void ThenGetAllCompleteGroupsForTrainingAreaIsCalledOnce()
            {
                GroupCompletionCommandsMock.Verify(m => m.GetAllCompleteGroupsForTrainingAreaAsync(It.IsAny<int>(), It.IsAny<UserDetails>()), Times.Once);
            }

            [Test]
            public void ThenOkNegotiatedContentResultIsNotNull()
            {
                Assert.IsNotNull(_negotiatedContentResult);
            }

            [Test]
            public void ThenTheCorrectAmountOfGroupProgressContractsAreReturned()
            {
                const int expected = 3;

                Assert.AreEqual(expected, _negotiatedContentResult.Content.GroupProgressContracts.Count);
            }

            [Test]
            public void ThenTrainingAreaIdIsCorrect()
            {
                Assert.AreEqual(TrainingAreaId, _negotiatedContentResult.Content.TrainingAreaId);
            }

            [Test]
            public void ThenTrainingAreaNameIsCorrect()
            {
                Assert.AreEqual(TrainingAreaName, _negotiatedContentResult.Content.TrainingAreaName);
            }
        }
    }
}
