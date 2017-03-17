using System.Collections.Generic;
using System.Linq;
using LP.EntityModels;
using Moq;
using NUnit.Framework;

namespace LP.ServiceHost.Common.BusinessLayer.Tests.Commands.TrainingAreaCommandsTests
{
    public class GivenGettingTrainingAreas : BaseGiven
    {
        private IEnumerable<TrainingArea> _trainingAreas; 

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenOnlyLiveTrainingAreToBeReturned : GivenGettingTrainingAreas
        {
            protected override async void When()
            {
                _trainingAreas = await SUT.GetLiveTrainingAreas();
            }

            [Test]
            public void ThenGetAllAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<TrainingArea>(), Times.Once());
            }

            [Test]
            public void ThenTrainingAreasIsNotNull()
            {
                Assert.IsNotNull(_trainingAreas);
            }

            [Test]
            public void ThenTheCorrectNumberOfTrainingAreasAreReturned()
            {
                const int expected = 3;

                Assert.AreEqual(expected, _trainingAreas.Count());
            }

            [Test]
            public void ThenTheCorrectTrainingAreasAreReturned()
            {
                var expected = new List<int> {2, 4, 6};

                CollectionAssert.AreEquivalent(expected, _trainingAreas.Select(t => t.TrainingAreaID).ToList());
            }
        }
    }
}
