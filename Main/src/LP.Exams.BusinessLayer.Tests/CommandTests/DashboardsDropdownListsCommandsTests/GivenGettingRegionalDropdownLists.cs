using System.Linq;
using LP.EntityModels;
using LP.Model.Authentication;
using LP.Model.ViewModels.Dashboards.Regional;
using Moq;
using NUnit.Framework;

namespace LP.Exams.BusinessLayer.Tests.CommandTests.DashboardsDropdownListsCommandsTests
{
    public class GivenGettingRegionalDropdownLists : BaseGiven
    {
        private RegionalDropdownListsViewModel _regionalDropdownListsViewModel;

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenMakingACorrectRequestAndTheParameterIsNull : GivenGettingRegionalDropdownLists
        {
            protected override async void When()
            {
                _regionalDropdownListsViewModel = await SUT.GetRegionalDropdownLists(null);
            }

            [Test]
            public void ThenGetUserByIdAsyncIsNeverCalled()
            {
                BaseCommandsMock.Verify(m => m.GetByIdAsync<User>(It.IsAny<int>()), Times.Never());
            }

            [Test]
            public void ThenGetCountryByIdAsyncIsNeverCalled()
            {
                BaseCommandsMock.Verify(m => m.GetByIdAsync<Country>(It.IsAny<int>()), Times.Never());
            }


            [Test]
            public void ThenGetAllCountriesAsyncIsCallecOnce()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<Country>(), Times.Once());
            }

            [Test]
            public void ThenGetAllRolesAsyncIsCallecOnce()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<Role>(), Times.Once());
            }

            [Test]
            public void ThenCOuntryListIsNotEmpty()
            {
                Assert.IsTrue(_regionalDropdownListsViewModel.CountryList.DropdownItems.Any());
            }

            [Test]
            public void ThenUserFunctionListIsNotEmpty()
            {
                Assert.IsTrue(_regionalDropdownListsViewModel.UserFunctionList.DropdownItems.Any());
            }

            [Test]
            public void ThenCountriesListHasExpectedNumberOfItems()
            {
                const int expected = 6;
                Assert.AreEqual(expected, _regionalDropdownListsViewModel.CountryList.DropdownItems.Count);
            }

            [Test]
            public void ThenUserFunctionListHasExpectedNumberOfItems()
            {
                const int expected = 5;
                Assert.AreEqual(expected, _regionalDropdownListsViewModel.UserFunctionList.DropdownItems.Count);
            }
        }

        public class WhenMakingACorrectRequestAndTheParameterIsExistingRegionId : GivenGettingRegionalDropdownLists
        {
            private readonly UserDetails _existingUserDetails = new UserDetails
            {
                UserId = ExistingUserId
            };
            protected override async void When()
            {
                _regionalDropdownListsViewModel = await SUT.GetRegionalDropdownLists(_existingUserDetails);
            }

            [Test]
            public void ThenGetUserByIdAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetByIdAsync<User>(ExistingUserId), Times.Once());
            }

            [Test]
            public void ThenGetCountryByIdAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetByIdAsync<Country>(ExistingCountryId), Times.Once());
            }

            [Test]
            public void ThenGetAllCountriesAsyncIsCallecOnce()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<Country>(), Times.Once());
            }

            [Test]
            public void ThenGetAllRolesAsyncIsCallecOnce()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<Role>(), Times.Once());
            }

            [Test]
            public void ThenCountryListIsNotEmpty()
            {
                Assert.IsTrue(_regionalDropdownListsViewModel.CountryList.DropdownItems.Any());
            }

            [Test]
            public void ThenUserFunctionListIsNotEmpty()
            {
                Assert.IsTrue(_regionalDropdownListsViewModel.UserFunctionList.DropdownItems.Any());
            }

            [Test]
            public void ThenCountriesListHasExpectedNumberOfItems()
            {
                const int expected = 5;
                Assert.AreEqual(expected, _regionalDropdownListsViewModel.CountryList.DropdownItems.Count);
            }

            [Test]
            public void ThenUserFunctionListHasExpectedNumberOfItems()
            {
                const int expected = 5;
                Assert.AreEqual(expected, _regionalDropdownListsViewModel.UserFunctionList.DropdownItems.Count);
            }
        }
    }
}
