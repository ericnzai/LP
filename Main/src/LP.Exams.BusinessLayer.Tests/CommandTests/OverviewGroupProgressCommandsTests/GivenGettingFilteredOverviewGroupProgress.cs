using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LP.ServiceHost.DataContracts.Response.Exams;
using Moq;
using NUnit.Framework;

namespace LP.Exams.BusinessLayer.Tests.CommandTests.OverviewGroupProgressCommandsTests
{
    public class GivenGettingFilteredOverviewGroupProgress : BaseGiven
    {
        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenARequestIsMade : GivenGettingFilteredOverviewGroupProgress
        {
            private const int RegionId = 45;
            private const int CountryId = 76;
            private const int JobRoleId = 32;
            private OverviewGroupTypeProgressResponseContract _overviewGroupTypeProgressResponseContract;
            protected override async void When()
            {
                _overviewGroupTypeProgressResponseContract = await SUT.GetOverviewGroupTypeProgressResponseContract(RegionId, CountryId, JobRoleId,0);
            }

            [Test]
            public void ThenGetUnfilteredUsersIsNeverCalled()
            {
                AllowedUserFilterMock.Verify(m => m.GetAllLiveUsersNotHiddenFromReports(), Times.Never());
            }

            [Test]
            public void ThenGetUnfilteredUserIdsIsNeverCalled()
            {
                AllowedUserFilterMock.Verify(m => m.GetAllLiveUsersNotHiddenFromReportsIds(), Times.Never());
            }

            [Test]
            public void ThenGetFilteredUserIdsNotHiddenFromReportsIsCalledOnce()
            {
                AllowedUserFilterMock.Verify(m => m.GetFilteredUserIdsNotHiddenFromReports(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()));
            }

            [Test]
            public void ThenGetFilteredUserIdsNotHiddenFromReportsIsCalledOnceWithTheCorrectParameters()
            {
                AllowedUserFilterMock.Verify(m => m.GetFilteredUserIdsNotHiddenFromReports(It.Is<int>(x => x == RegionId), It.Is<int>(x => x == CountryId), It.Is<int>(x => x == JobRoleId), It.IsAny<int>()));
            }

            [Test]
            public void ThenOverviewGroupTypeProgressResponseContractIsNotNull()
            {
                Assert.IsNotNull(_overviewGroupTypeProgressResponseContract);
            }
        }
    }
}
