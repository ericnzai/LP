using System.Web.Http;
using LP.Api.Shared.Tests.TestHelpers;
using LP.ServiceHost.DataContracts.Response.Content;
using Moq;
using NUnit.Framework;

namespace LP.Content.Tests.DashboardFilterCommandsTests
{
    public class GivenGettingCountryDropDowns : BaseGiven
    {
        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenPassingARegionId : GivenGettingCountryDropDowns
        {
            private const int RegionId = 476;
            private IHttpActionResult _httpActionResult;
            private DashboardFilterDropdownResponseContract _dashboardFilterDropdownResponseContract;
            
            protected override async void When()
            {
                _httpActionResult = await SUT.GetCountry(RegionId);

                _dashboardFilterDropdownResponseContract = await DeserializationHelper.GetDeserializedResponseContent<DashboardFilterDropdownResponseContract>(_httpActionResult);
            }

            [Test]
            public void ThenDropdownFilterCommandsCountryIsNeverCalledWithoutARegionId()
            {
                AskContentApiBusinessMock.Verify(m => m.DropdownFilterCommands.Country(), Times.Never());
            }

            //[Test]
            //public void ThenDropdownFilterCommandsCountryIsCalledOnceWithAnyRegionId()
            //{
            //    AskContentApiBusinessMock.Verify(m => m.DropdownFilterCommands.Country(It.IsAny<int>()), Times.Once());
            //}

            //[Test]
            //public void ThenDropdownFilterCommandsCountryIsCalledOnceWithTheCorrectRegionId()
            //{
            //    AskContentApiBusinessMock.Verify(m => m.DropdownFilterCommands.Country(It.Is<int>(x => x == RegionId)), Times.Once());
            //}

            [Test]
            public void ThenDashboardFilterDropdownResponseContractIsNotNull()
            {
                Assert.IsNotNull(_dashboardFilterDropdownResponseContract);
            }
        }

        public class WhenPassingNoRegionId : GivenGettingCountryDropDowns
        {
            private IHttpActionResult _httpActionResult;
            private DashboardFilterDropdownResponseContract _dashboardFilterDropdownResponseContract;

            protected override async void When()
            {
                _httpActionResult = await SUT.GetCountry();

                _dashboardFilterDropdownResponseContract = await DeserializationHelper.GetDeserializedResponseContent<DashboardFilterDropdownResponseContract>(_httpActionResult);
            }

            //[Test]
            //public void ThenDropdownFilterCommandsCountryIsOnceCalledWithoutARegionId()
            //{
            //    AskContentApiBusinessMock.Verify(m => m.DropdownFilterCommands.Country(), Times.Once());
            //}

            [Test]
            public void ThenDropdownFilterCommandsCountryIsNeverCalledWithARegionId()
            {
                AskContentApiBusinessMock.Verify(m => m.DropdownFilterCommands.Country(It.IsAny<int>()), Times.Never());
            }

            [Test]
            public void ThenDashboardFilterDropdownResponseContractIsNotNull()
            {
                Assert.IsNotNull(_dashboardFilterDropdownResponseContract);
            }
        }
    }
}
