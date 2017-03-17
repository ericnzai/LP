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
    public class GivenGettingRegionDropDown : BaseGiven
    {
        protected override void Given()
        {
            PrepareSut();
        }


        public class WhenACorrectRequestIsMade : GivenGettingTrainerDropDown
        {
            private DashboardFilterDropdownResponseContract _dashboardFilterDropdownResponseContract;

            protected override async void When()
            {
                _dashboardFilterDropdownResponseContract = await SUT.Region();
            }


            [Test]
            public void TheGetConditionalCountriesAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetConditionalWithIncludesAsync<Country>(It.IsAny<Expression<Func<Country, bool>>>(), It.IsAny<Expression<Func<Country, Object>>>()), Times.Once());
            }

            [Test]
            public void TheGetConditionalRegionsAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetConditionalAsync<Region>(It.IsAny<Expression<Func<Region, bool>>>()), Times.Once());
            }


            [Test]
            public void ThenDashboardFilterDropdownResponseContractIsNotNull()
            {
                Assert.IsNotNull(_dashboardFilterDropdownResponseContract);
            }

            [Test]
            public void ThenTheCorrectAmountOfDropDownsAreReturned()
            {
                const int expected = 9;

                Assert.AreEqual(expected, _dashboardFilterDropdownResponseContract.DropdownItemContracts.Count);
            }

            [Test]
            public void ThenTheCorrectTrainerIdsAreReturned()
            {
                var expected = new List<int> { 0, 1,2,3,4,5,6,7,8 };

                CollectionAssert.AreEquivalent(expected, _dashboardFilterDropdownResponseContract.DropdownItemContracts.Select(a => a.Value));
            }

            [Test]
            public void ThenTheCorrectTrainerNamesAreReturned()
            {
                var expected = new List<string> { "All Regions", "Europe", "Africa", "Canada", "USA", "Australia", "China", "Asia", "Antarctica"};

                CollectionAssert.AreEquivalent(expected, _dashboardFilterDropdownResponseContract.DropdownItemContracts.Select(a => a.Text));
            }
        }
    }
}
