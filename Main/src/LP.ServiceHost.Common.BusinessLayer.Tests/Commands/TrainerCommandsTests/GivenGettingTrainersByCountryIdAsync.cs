using System.Collections.Generic;
using System.Linq;
using LP.EntityModels;
using LP.EntityModels.Views;
using Moq;
using NUnit.Framework;
using SpecsFor.ShouldExtensions;

namespace LP.ServiceHost.Common.BusinessLayer.Tests.Commands.TrainerCommandsTests
{
    public class GivenGettingTrainersByCountryIdAsync : BaseGiven
    {
        private IEnumerable<TrainersWithStudentsCountries> _trainers; 

        protected override void Given()
        {
            PrepareSut();
        }
        
        public class WhenTheCorrectCallIsMadeWithCorrectParameter : GivenGettingTrainersByCountryIdAsync
        {
            protected override async void When()
            {
                _trainers = await SUT.GetTrainersByCountryIdAsync(It.IsAny<int>());
            }

            [Test]
            public void ThenGetAllAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<TrainersWithStudentsCountries>(), Times.Once());
            }
        }

        public class WhenTheCorrectCallIsMadeAndThereAreTrainersForCountry : GivenGettingTrainersByCountryIdAsync
        {
            protected override async void When()
            {
                _trainers = await SUT.GetTrainersByCountryIdAsync(ExistingCountryId);
            }

            [Test]
            public void ThenGetAllAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<TrainersWithStudentsCountries>(), Times.Once());
            }

            [Test]
            public void ThenTrainersAreNotNull()
            {
                Assert.IsTrue(_trainers.Any());
            }

            [Test]
            public void ThenTheCorrectNumberOfTraininersIsReturned()
            {
                const int expected = 3;

                Assert.AreEqual(expected, _trainers.Count());
            }
        }

        public class WhenTheCorrectCallIsMadeAndThereIsNoTrainersForCountry : GivenGettingTrainersByCountryIdAsync
        {
            private const int NonExistingCountryId = 3;
            protected override async void When()
            {
                _trainers = await SUT.GetTrainersByCountryIdAsync(NonExistingCountryId);
            }

            [Test]
            public void ThenGetAllAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<TrainersWithStudentsCountries>(), Times.Once());
            }

            [Test]
            public void ThenTrainersAreEmpty()
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


        public class WhenTheCorrectCallIsMadeAndTheParameterIsIncorrect : GivenGettingTrainersByCountryIdAsync
        {
            private const int IncorrectParameter = -1;
            protected override async void When()
            {
                _trainers = await SUT.GetTrainersByCountryIdAsync(IncorrectParameter);
            }

            [Test]
            public void ThenGetAllAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetAll<TrainingArea>(), Times.Never);
            }

            [Test]
            public void ThenTrainerAreNull()
            {
                Assert.IsNull(_trainers);
            }
        }
    }
}
