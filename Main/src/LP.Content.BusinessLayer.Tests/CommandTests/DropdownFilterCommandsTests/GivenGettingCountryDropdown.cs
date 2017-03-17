using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LP.EntityModels;
using LP.ServiceHost.DataContracts.Response.Content;
using Moq;
using NUnit.Framework;

namespace LP.Content.BusinessLayer.Tests.CommandTests.DropdownFilterCommandsTests
{
    public class GivenGettingCountryDropdown : BaseGiven
    {
        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenGettingCountryDropdownForGlobal : GivenGettingCountryDropdown
        {
            private DashboardFilterDropdownResponseContract _dashboardFilterDropdownResponseContract;

            protected override async void When()
            {
                _dashboardFilterDropdownResponseContract = await SUT.Country();
            }

            [Test]
            public void ThenGetAllCountriesAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<Country>(), Times.Once());
            }

            [Test]
            public void ThenGetConditionalCountriesAsyncIsNeverCalled()
            {
                BaseCommandsMock.Verify(m => m.GetConditionalAsync<Country>(It.IsAny<Expression<Func<Country, bool>>>()), Times.Never());
            }

            [Test]
            public void ThenDashboardFilterDropdownResponseContractIsNotNull()
            {
                Assert.IsNotNull(_dashboardFilterDropdownResponseContract);
            }

            [Test]
            public void ThenDropdownItemContractsIsNotEmpty()
            {
                CollectionAssert.IsNotEmpty(_dashboardFilterDropdownResponseContract.DropdownItemContracts);
            }

            [Test]
            public void ThenTheCorrectAmountOfCountriesAreReturned()
            {
                const int expected = 14;

                Assert.AreEqual(expected, _dashboardFilterDropdownResponseContract.DropdownItemContracts.Count);
            }

            [Test]
            public void ThenTheFirstValueItemIsDefault()
            {
                const string expected = "All Countries";

                Assert.AreEqual(expected, _dashboardFilterDropdownResponseContract.DropdownItemContracts.First().Text);
            }

            [Test]
            public void ThenTheFirstKeyItemIsDefault()
            {
                const int expected = 0;

                Assert.AreEqual(expected, _dashboardFilterDropdownResponseContract.DropdownItemContracts.First().Value);
            }

            [Test]
            public void ThenAllItemsInTheDropdownItemContractsAreUnique()
            {
                CollectionAssert.AllItemsAreUnique(_dashboardFilterDropdownResponseContract.DropdownItemContracts);
            }
        }

        public class WhenGettingCountryDropdownForRegionOne : GivenGettingCountryDropdown
        {
            private DashboardFilterDropdownResponseContract _dashboardFilterDropdownResponseContract;

            protected override void Given()
            {
                Countries = Countries.Where(r => r.RegionId == 1).ToList();

                PrepareSut();
            }

            protected override async void When()
            {
                _dashboardFilterDropdownResponseContract = await SUT.Country(1);
            }

            [Test]
            public void ThenGetAllCountriesAsyncIsNeverCalled()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<Country>(), Times.Once);
            }

            [Test]
            public void ThenDashboardFilterDropdownResponseContractIsNotNull()
            {
                Assert.IsNotNull(_dashboardFilterDropdownResponseContract);
            }

            [Test]
            public void ThenDropdownItemContractsIsNotEmpty()
            {
                CollectionAssert.IsNotEmpty(_dashboardFilterDropdownResponseContract.DropdownItemContracts);
            }

            [Test]
            public void ThenTheCorrectAmountOfCountriesAreReturned()
            {
                const int expected = 7;

                Assert.AreEqual(expected, _dashboardFilterDropdownResponseContract.DropdownItemContracts.Count);
            }

            [Test]
            public void ThenTheFirstValueItemIsDefault()
            {
                const string expected = "All Countries";

                Assert.AreEqual(expected, _dashboardFilterDropdownResponseContract.DropdownItemContracts.First().Text);
            }

            [Test]
            public void ThenTheFirstKeyItemIsDefault()
            {
                const int expected = 0;

                Assert.AreEqual(expected, _dashboardFilterDropdownResponseContract.DropdownItemContracts.First().Value);
            }

            [Test]
            public void ThenAllItemsInTheDropdownItemContractsAreUnique()
            {
                CollectionAssert.AllItemsAreUnique(_dashboardFilterDropdownResponseContract.DropdownItemContracts);
            }

            [Test]
            public void ThenTheCorrectCountryIdsAreReturned()
            {
                var expected = new List<int> {0, 1, 2, 3, 4, 5, 6};

                CollectionAssert.AreEquivalent(expected, _dashboardFilterDropdownResponseContract.DropdownItemContracts.Select(a => a.Value));
            }
        }

        public class WhenGettingCountryDropdownForRegionTwo : GivenGettingCountryDropdown
        {
            private DashboardFilterDropdownResponseContract _dashboardFilterDropdownResponseContract;
            private const int RegionId = 2;
            protected override void Given()
            {
                Countries = Countries.Where(r => r.RegionId == RegionId).ToList();

                PrepareSut();
            }

            protected override async void When()
            {
                _dashboardFilterDropdownResponseContract = await SUT.Country(RegionId);
            }

            [Test]
            public void ThenGetAllCountriesAsyncIsNeverCalled()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<Country>(), Times.Once);
            }

            [Test]
            public void ThenDashboardFilterDropdownResponseContractIsNotNull()
            {
                Assert.IsNotNull(_dashboardFilterDropdownResponseContract);
            }

            [Test]
            public void ThenDropdownItemContractsIsNotEmpty()
            {
                CollectionAssert.IsNotEmpty(_dashboardFilterDropdownResponseContract.DropdownItemContracts);
            }

            [Test]
            public void ThenTheCorrectAmountOfCountriesAreReturned()
            {
                const int expected = 5;

                Assert.AreEqual(expected, _dashboardFilterDropdownResponseContract.DropdownItemContracts.Count);
            }

            [Test]
            public void ThenTheFirstValueItemIsDefault()
            {
                const string expected = "All Countries";

                Assert.AreEqual(expected, _dashboardFilterDropdownResponseContract.DropdownItemContracts.First().Text);
            }

            [Test]
            public void ThenTheFirstKeyItemIsDefault()
            {
                const int expected = 0;

                Assert.AreEqual(expected, _dashboardFilterDropdownResponseContract.DropdownItemContracts.First().Value);
            }

            [Test]
            public void ThenAllItemsInTheDropdownItemContractsAreUnique()
            {
                CollectionAssert.AllItemsAreUnique(_dashboardFilterDropdownResponseContract.DropdownItemContracts);
            }

            [Test]
            public void ThenTheCorrectCountryIdsAreReturned()
            {
                var expected = new List<int> { 0, 7, 8, 9, 10 };

                CollectionAssert.AreEquivalent(expected, _dashboardFilterDropdownResponseContract.DropdownItemContracts.Select(a => a.Value));
            }
        }

        public class WhenGettingCountryDropdownForRegionThree : GivenGettingCountryDropdown
        {
            private DashboardFilterDropdownResponseContract _dashboardFilterDropdownResponseContract;
            private const int RegionId = 3;
            protected override void Given()
            {
                Countries = Countries.Where(r => r.RegionId == RegionId).ToList();

                PrepareSut();
            }

            protected override async void When()
            {
                _dashboardFilterDropdownResponseContract = await SUT.Country(RegionId);
            }

            [Test]
            public void ThenGetAllCountriesAsyncIsNeverCalled()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<Country>(), Times.Once());
            }

            [Test]
            public void ThenDashboardFilterDropdownResponseContractIsNotNull()
            {
                Assert.IsNotNull(_dashboardFilterDropdownResponseContract);
            }

            [Test]
            public void ThenDropdownItemContractsIsNotEmpty()
            {
                CollectionAssert.IsNotEmpty(_dashboardFilterDropdownResponseContract.DropdownItemContracts);
            }

            [Test]
            public void ThenTheCorrectAmountOfCountriesAreReturned()
            {
                const int expected = 4;

                Assert.AreEqual(expected, _dashboardFilterDropdownResponseContract.DropdownItemContracts.Count);
            }

            [Test]
            public void ThenTheFirstValueItemIsDefault()
            {
                const string expected = "All Countries";

                Assert.AreEqual(expected, _dashboardFilterDropdownResponseContract.DropdownItemContracts.First().Text);
            }

            [Test]
            public void ThenTheFirstKeyItemIsDefault()
            {
                const int expected = 0;

                Assert.AreEqual(expected, _dashboardFilterDropdownResponseContract.DropdownItemContracts.First().Value);
            }

            [Test]
            public void ThenAllItemsInTheDropdownItemContractsAreUnique()
            {
                CollectionAssert.AllItemsAreUnique(_dashboardFilterDropdownResponseContract.DropdownItemContracts);
            }

            [Test]
            public void ThenTheCorrectCountryIdsAreReturned()
            {
                var expected = new List<int> { 0, 11, 12, 13 };

                CollectionAssert.AreEquivalent(expected, _dashboardFilterDropdownResponseContract.DropdownItemContracts.Select(a => a.Value));
            }
        }
    }
}
