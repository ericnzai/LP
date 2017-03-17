using System.Collections.Generic;
using System.Linq;
using LP.EntityModels;
using LP.EntityModels.Views;
using Moq;
using NUnit.Framework;

namespace LP.ServiceHost.Common.BusinessLayer.Tests.Commands.TrainerCommandsTests
{
    public class GivenGettingAllTrainersAsync : BaseGiven
    {
        private IEnumerable<TrainersWithStudentsCountries> _trainers; 

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenCorrectCallIsMadeAndTrainersExist : GivenGettingAllTrainersAsync
        {
            protected override async void When()
            {
                _trainers = await SUT.GetAllTrainersAsync();
            }

            [Test]
            public void ThenGetAllAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<TrainersWithStudentsCountries>(), Times.Once());
            }

            [Test]
            public void ThenTraininersAreNotNull()
            {
                Assert.IsTrue(_trainers.Any());
            }

            [Test]
            public void ThenTheCorrectNumberOfTraininersAreReturned()
            {
                const int expected = 6;

                Assert.AreEqual(expected, _trainers.Count());
            }
        }

        public class WhenCorrectCallIsMadeAndThereIsNoTrainer : GivenGettingAllTrainersAsync
        {
            protected override async void When()
            {
                Trainers = new List<TrainersWithStudentsCountries>();
                
                PrepareSut();
                _trainers = await SUT.GetAllTrainersAsync();
            }

            [Test]
            public void ThenGetAllAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<TrainersWithStudentsCountries>(), Times.Once());
            }

            [Test]
            public void ThenTraininersAreEmpty()
            {
                Assert.IsFalse(_trainers.Any());
            }

            [Test]
            public void ThenThereIsNoTrainers()
            {
                const int expected = 0;

                Assert.AreEqual(expected, _trainers.Count());
            }
        }
    }
}
