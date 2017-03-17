using System.Collections.Generic;
using System.Linq;
using LP.Model.Authentication;
using LP.ServiceHost.DataContracts.Response.Content;
using Moq;
using NUnit.Framework;

namespace LP.Content.BusinessLayer.Tests.CommandTests.DropdownFilterCommandsTests
{
    public class GivenGettingTrainerDropDown : BaseGiven
    {
        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenACorrectRequestIsMadeForAnExistingCountry : GivenGettingTrainerDropDown
        {
            private DashboardFilterDropdownResponseContract _dashboardFilterDropdownResponseContract;
            private const int CountryId = 1;
            protected override async void When()
            {
                _dashboardFilterDropdownResponseContract = await SUT.Trainer(CountryId);
            }

            [Test]
            public void TheGetTrainersIdsByCountryIdIsCalledOnce()
            {
                TrainerCommandsMock.Verify(m => m.GetTrainersIdsByCountryId(It.IsAny<int>()), Times.Once());
            }

            [Test]
            public void TheGetTrainersIdsByCountryIdIsCalledOnceWithTheCorrectCountryId()
            {
                TrainerCommandsMock.Verify(m => m.GetTrainersIdsByCountryId(It.Is<int>(x => x == CountryId)), Times.Once());
            }

            [Test]
            public void TheGetDecryptedUsersIsCalledOnce()
            {
                UserCommandsMock.Verify(m => m.GetDecryptedUsers(It.IsAny<IEnumerable<int>>()), Times.Once());
            }

            [Test]
            public void ThenDashboardFilterDropdownResponseContractIsNotNull()
            {
                Assert.IsNotNull(_dashboardFilterDropdownResponseContract);
            }

            [Test]
            public void ThenTheCorrectAmountOfDropDownsAreReturned()
            {
                const int expected = 7;

                Assert.AreEqual(expected, _dashboardFilterDropdownResponseContract.DropdownItemContracts.Count);
            }

            [Test]
            public void ThenTheCorrectTrainerIdsAreReturned()
            {
                var expected = new List<int> {0, 17, 12, 23, 82, 47, 65};

                CollectionAssert.AreEquivalent(expected, _dashboardFilterDropdownResponseContract.DropdownItemContracts.Select(a => a.Value));
            }

            [Test]
            public void ThenTheCorrectTrainerNamesAreReturned()
            {
                var expected = new List<string> { "All Trainers", "Display name 17", "Display name 12", "Display name 23", "Display name 82", "Display name 47", "Display name 65" };

                CollectionAssert.AreEquivalent(expected, _dashboardFilterDropdownResponseContract.DropdownItemContracts.Select(a => a.Text));
            }
        }

        public class WhenACorrectRequestIsMadeForAnCountryThatHasNoTrainers : GivenGettingTrainerDropDown
        {
            private DashboardFilterDropdownResponseContract _dashboardFilterDropdownResponseContract;
            
            private const int CountryId = 2;
            
            protected override async void When()
            {
                TrainersWithStudentsCountries = new List<int>();
                DecryptedUsers = new List<DecryptedUser>();

                PrepareSut();
                _dashboardFilterDropdownResponseContract = await SUT.Trainer(CountryId);
            }

            [Test]
            public void TheGetTrainersIdsByCountryIdIsCalledOnce()
            {
                TrainerCommandsMock.Verify(m => m.GetTrainersIdsByCountryId(It.IsAny<int>()), Times.Once());
            }

            [Test]
            public void TheGetTrainersIdsByCountryIdIsCalledOnceWithTheCorrectCountryId()
            {
                TrainerCommandsMock.Verify(m => m.GetTrainersIdsByCountryId(It.Is<int>(x => x == CountryId)), Times.Once());
            }

            [Test]
            public void TheGetDecryptedUsersIsCalledOnce()
            {
                UserCommandsMock.Verify(m => m.GetDecryptedUsers(It.IsAny<IEnumerable<int>>()), Times.Once());
            }

            [Test]
            public void ThenDashboardFilterDropdownResponseContractIsNotNull()
            {
                Assert.IsNotNull(_dashboardFilterDropdownResponseContract);
            }

            [Test]
            public void ThenTheCorrectAmountOfDropDownsAreReturned()
            {
                const int expected = 1;

                Assert.AreEqual(expected, _dashboardFilterDropdownResponseContract.DropdownItemContracts.Count);
            }

            [Test]
            public void ThenTheCorrectTrainerIdsAreReturned()
            {
                var expected = new List<int> { 0 };

                CollectionAssert.AreEquivalent(expected, _dashboardFilterDropdownResponseContract.DropdownItemContracts.Select(a => a.Value));
            }

            [Test]
            public void ThenTheCorrectTrainerNamesAreReturned()
            {
                var expected = new List<string> { "All Trainers" };

                CollectionAssert.AreEquivalent(expected, _dashboardFilterDropdownResponseContract.DropdownItemContracts.Select(a => a.Text));
            }
        }
    }
}
