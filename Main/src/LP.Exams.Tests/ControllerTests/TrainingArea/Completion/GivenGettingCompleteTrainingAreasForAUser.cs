using System.Web.Http;
using LP.Api.Shared.Tests.TestHelpers;
using LP.Model.Authentication;
using LP.ServiceHost.DataContracts.Response.Exams;
using Moq;
using NUnit.Framework;

namespace LP.Exams.Tests.ControllerTests.TrainingArea.Completion
{
    public class GivenGettingCompleteTrainingAreasForAUser : BaseGiven
    {
        private IHttpActionResult _httpActionResult;
        private TrainingAreasCompleteResponseContract _trainingAreasCompleteResponseContract;
        protected override void Given()
        {
            PrepareSut();
        }

        protected class WhenMakingACorrectRequest : GivenGettingCompleteTrainingAreasForAUser
        {
            protected override async void When()
            {
                _httpActionResult = await SUT.Get();

                _trainingAreasCompleteResponseContract = await DeserializationHelper.GetDeserializedResponseContent<TrainingAreasCompleteResponseContract>(_httpActionResult);
            }

            //[Test]
            //public void ThenNumberOfModulesCompletedForAllTrainingAreasAsyncIsCalledOnce()
            //{
            //     NumberAchievedCommandsMock.Verify(m => m.NumberOfModulesCompletedForAllTrainingAreasAsync(It.IsAny<UserDetails>()), Times.Once());
            //}

            //[Test]
            //public void ThenTrainingAreasCompleteResponseContractIsNotNull()
            //{
            //    Assert.IsNotNull(_trainingAreasCompleteResponseContract);
            //}
        }
    }
}
