using System.Linq;
using LP.EntityModels;
using LP.Model.Authentication;
using LP.Model.ViewModels.Dashboards.Country;
using Moq;
using NUnit.Framework;

namespace LP.Exams.BusinessLayer.Tests.CommandTests.DashboardsDropdownListsCommandsTests
{
    public class GivenGettingCountryDropdownLists : BaseGiven
    {
        private CountryDropdownListsViewModel _countryDropdownListsViewModel;

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenMakingACorrectRequestWithNullParameter : GivenGettingCountryDropdownLists
        {
            protected override async void When()
            {
                _countryDropdownListsViewModel = await SUT.GetCountryDropdownLists(null);
            }

            [Test]
            public void ThenGetUserByIdAsyncIsNeverCalled()
            {
                BaseCommandsMock.Verify(m => m.GetByIdAsync<User>(It.IsAny<int>()), Times.Never());
            }

            [Test]
            public void ThenGetAllRolesAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<Role>(), Times.Once());
            }

            [Test]
            public void ThenGetAllUserRolesAsyncIsCalledOnce()
            {
                TrainerCommandsMock.Verify(m => m.GetAllTrainersAsync(), Times.Once());
            }


            [Test]
            public void ThenTrainerListIsNotEmpty()
            {
                Assert.IsTrue(_countryDropdownListsViewModel.TrainerList.DropdownItems.Any());
            }

            [Test]
            public void ThenUserFunctionListIsNotEmpty()
            {
                Assert.IsTrue(_countryDropdownListsViewModel.UserFunctionList.DropdownItems.Any());
            }

            [Test]
            public void ThenTrainersListHasExpectedNumberOfItems()
            {
                const int expected = 6;
                Assert.AreEqual(expected, _countryDropdownListsViewModel.TrainerList.DropdownItems.Count);
            }

            [Test]
            public void ThenUserFunctionListHasFiveItems()
            {
                const int expected = 5;
                Assert.AreEqual(expected, _countryDropdownListsViewModel.UserFunctionList.DropdownItems.Count);
            }
        }

        public class WhenMakingACorrectRequestAndThereAreTrainers : GivenGettingCountryDropdownLists
        {
            private readonly UserDetails _existingUserDetails = new UserDetails
            {
                UserId = ExistingUserId
            };
            protected override async void When()
            {
                _countryDropdownListsViewModel = await SUT.GetCountryDropdownLists(_existingUserDetails);
            
            }

            [Test]
            public void ThenGetUserByIdAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetByIdAsync<User>(ExistingUserId), Times.Once());
            }

            [Test]
            public void ThenGetAllRolesAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<Role>(), Times.Once());
            }

            [Test]
            public void ThenGetAllTrainersAsyncIsCalledOnceWitchCorrectParameter()
            {
                TrainerCommandsMock.Verify(m => m.GetTrainersByCountryIdAsync(ExistingCountryId), Times.Once());
            }
            
            [Test]
            public void ThenTrainerListIsNotEmpty()
            {
                Assert.IsTrue(_countryDropdownListsViewModel.TrainerList.DropdownItems.Any());
            }

            [Test]
            public void ThenUserFunctionListIsNotEmpty()
            {
                Assert.IsTrue(_countryDropdownListsViewModel.UserFunctionList.DropdownItems.Any());
            }

            [Test]
            public void ThenTrainersListHasThreeItems()
            {
                const int expected = 3;
                Assert.AreEqual(expected, _countryDropdownListsViewModel.TrainerList.DropdownItems.Count);
            }

            [Test]
            public void ThenUserFunctionListHasFiveItems()
            {
                const int expected = 5;
                Assert.AreEqual(expected, _countryDropdownListsViewModel.UserFunctionList.DropdownItems.Count);
            }
        }

        public class WhenMakingACorrectRequestAndThereIsNoTrainer : GivenGettingCountryDropdownLists
        {
            private readonly UserDetails _existingUserDetails = new UserDetails
            {
                UserId = 5
            };

            protected override async void When()
            {
                _countryDropdownListsViewModel = await SUT.GetCountryDropdownLists(_existingUserDetails);
            }

            [Test]
            public void ThenGetUserByIdAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetByIdAsync<User>(5), Times.Once());
            }

            [Test]
            public void ThenGetAllRolesAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<Role>(), Times.Once());
            }

            [Test]
            public void ThenGetAllUserRolesAsyncIsCalledOnce()
            {
                TrainerCommandsMock.Verify(m => m.GetTrainersByCountryIdAsync(CountryWithNoTrainersId), Times.Once());
            }
            
            [Test]
            public void ThenTrainerListIsEmpty()
            {
                Assert.IsFalse(_countryDropdownListsViewModel.TrainerList.DropdownItems.Any());
            }

            [Test]
            public void ThenUserFunctionListIsNotEmpty()
            {
                Assert.IsTrue(_countryDropdownListsViewModel.UserFunctionList.DropdownItems.Any());
            }

            [Test]
            public void ThenTrainersListHasNoItem()
            {
                const int expected = 0;
                Assert.AreEqual(expected, _countryDropdownListsViewModel.TrainerList.DropdownItems.Count);
            }

            [Test]
            public void ThenUserFunctionListHasFiveItems()
            {
                const int expected = 5;
                Assert.AreEqual(expected, _countryDropdownListsViewModel.UserFunctionList.DropdownItems.Count);
            }
        }
    }
}
