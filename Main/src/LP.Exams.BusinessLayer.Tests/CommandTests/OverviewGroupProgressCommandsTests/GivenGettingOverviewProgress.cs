using System.Collections.Generic;
using System.Linq;
using LP.ServiceHost.DataContracts.Common.Exams.Dashboards;
using LP.ServiceHost.DataContracts.Response.Exams;
using Moq;
using NUnit.Framework;

namespace LP.Exams.BusinessLayer.Tests.CommandTests.OverviewGroupProgressCommandsTests
{
    public class GivenGettingOverviewProgress : BaseGiven
    {
        
        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenMakingACorrectRequestAllTheDependenciesAreCalledCorrectly : GivenGettingOverviewProgress
        {
            private OverviewGroupTypeProgressResponseContract _overviewGroupTypeProgressResponseContract;

            protected override async void When()
            {
                _overviewGroupTypeProgressResponseContract = await SUT.GetOverviewGroupTypeProgressResponseContract();
            }

            [Test]
            public void ThenAllowedUserFilterIsCalledOnce()
            {
                AllowedUserFilterMock.Verify(m => m.GetAllLiveUsersNotHiddenFromReportsIds(), Times.Once());
            }

            [Test]
            public void TheFilterAllowedGroupsGetAllLiveGroupsListIsCalledOnce()
            {
                FilterAllowedGroupsMock.Verify(m => m.GetAllLiveGroupsList(), Times.Once());
            }

            [Test]
            public void ThenGetCertificatesAchievedForUsersAndGroupsIsCalledOnce()
            {
                CertificatesAchievedCommandsMock.Verify(m => m.GetCertificatesAchievedForUsersAndGroups(It.IsAny<IEnumerable<int>>(), It.IsAny<IEnumerable<int>>()), Times.Once());
            }

            [Test]
            public void ThenGetUserPostsViewedGroupedByGroupIsCalledOnce()
            {
                UserPostViewedCommandsMock.Verify(m => m.GetUserPostsViewedGroupedByGroup(It.IsAny<IEnumerable<int>>()), Times.Once());
            }

            [Test]
            public void ThenCalculatePercentagesIsCalledTheCorrectAmountOfTimes()
            {
                const int expected = 9;

                CommonCalculatorCommandsMock.Verify(m => m.CalculatePercentages(It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(expected));
            }
           
            [Test]
            public void ThenOverviewGroupTypeProgressResponseContractIsNotNull()
            {
                Assert.IsNotNull(_overviewGroupTypeProgressResponseContract);
            }

            [Test]
            public void ThenTheCorrectNumberOfDashboardBarChartViewModelsAreReturned()
            {
                const int expected = 3;

                Assert.AreEqual(expected, _overviewGroupTypeProgressResponseContract.DashboardBarChartContracts.Count);
            }

            [Test]
            public void ThenAllBarChartContractsAreUnique()
            {
                CollectionAssert.AllItemsAreUnique(_overviewGroupTypeProgressResponseContract.DashboardBarChartContracts);
            }
            
        }

        public class WhenACorrectCallIsMadeTheFirstDashboardBarChartContractIsCorrect : GivenGettingOverviewProgress
        {
            private OverviewGroupTypeProgressResponseContract _overviewGroupTypeProgressResponseContract;
            private DashboardBarChartContract _dashboardBarChartContract;
            protected override async void When()
            {
                _overviewGroupTypeProgressResponseContract = await SUT.GetOverviewGroupTypeProgressResponseContract();

                _dashboardBarChartContract =
                    _overviewGroupTypeProgressResponseContract.DashboardBarChartContracts.First(x => x.Title == "GT 1");
            }

            [Test]
            public void ThenOverviewGroupTypeProgressResponseContractIsNotNull()
            {
                Assert.IsNotNull(_overviewGroupTypeProgressResponseContract);
            }

            [Test]
            public void ThenFirstGroupTypeNameIsCorrect()
            {
                const string expected = "GT 1";

                Assert.AreEqual(expected, _dashboardBarChartContract.Title);
            }

            [Test]
            public void ThenNumberOfUsersStartedIsCorrect()
            {
                const int expected = 16;

                Assert.AreEqual(expected, _dashboardBarChartContract.NumberOfUsersStarted);
            }

            [Test]
            public void ThenPercentageOfUsersStartedIsCorrect()
            {
                const int expected = 51;

                Assert.AreEqual(expected, _dashboardBarChartContract.PercentageOfUsersStarted);
            }
        }
    }
}
