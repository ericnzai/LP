using System.Linq;
using LP.EntityModels;
using LP.Model.ViewModels.Dashboards.Global;
using Moq;
using NUnit.Framework;

namespace LP.Exams.BusinessLayer.Tests.CommandTests.DashboardsDropdownListsCommandsTests
{
    public class GivenGettingGlobalDropdownLists : BaseGiven
    {
        private GlobalDropdownListsViewModel _globalDropdownListsViewModel;

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenMakingACorrectRequestAndTheParameterIsNull : GivenGettingGlobalDropdownLists
        {
            protected override async void When()
            {
                _globalDropdownListsViewModel = await SUT.GetGlobalDropdownLists();
            }

            [Test]
            public void ThenGetAllCountriesAsyncIsCallecOnce()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<Country>(), Times.Once());
            }

            [Test]
            public void ThenGetAllRegionsAsyncIsCallecOnce()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<Region>(), Times.Once());
            }

            [Test]
            public void ThenGetAllRolesAsyncIsCallecOnce()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<Role>(), Times.Once());
            }

            [Test]
            public void ThenRegionListIsNotEmpty()
            {
                Assert.IsTrue(_globalDropdownListsViewModel.RegionList.DropdownItems.Any());
            }

            [Test]
            public void ThenCountryListIsNotEmpty()
            {
                Assert.IsTrue(_globalDropdownListsViewModel.CountryList.DropdownItems.Any());
            }

            [Test]
            public void ThenUserFunctionListIsNotEmpty()
            {
                Assert.IsTrue(_globalDropdownListsViewModel.UserFunctionList.DropdownItems.Any());
            }

            [Test]
            public void ThenRegionsListHasExpectedNumberOfItems()
            {
                const int expected = 3;
                Assert.AreEqual(expected, _globalDropdownListsViewModel.RegionList.DropdownItems.Count);
            }

            [Test]
            public void ThenCountriesListHasExpectedNumberOfItems()
            {
                const int expected = 6;
                Assert.AreEqual(expected, _globalDropdownListsViewModel.CountryList.DropdownItems.Count);
            }

            [Test]
            public void ThenUserFunctionListHasExpectedNumberOfItems()
            {
                const int expected = 5;
                Assert.AreEqual(expected, _globalDropdownListsViewModel.UserFunctionList.DropdownItems.Count);
            }
        }
    }
}
