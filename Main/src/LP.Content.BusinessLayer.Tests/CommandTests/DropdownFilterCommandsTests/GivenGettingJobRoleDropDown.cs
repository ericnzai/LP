using System.Collections.Generic;
using System.Linq;
using LP.ServiceHost.DataContracts.Response.Content;
using Moq;
using NUnit.Framework;

namespace LP.Content.BusinessLayer.Tests.CommandTests.DropdownFilterCommandsTests
{
    public class GivenGettingJobRoleDropDown : BaseGiven
    {
        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenACorrectRequestIsMade : GivenGettingJobRoleDropDown
        {
            private DashboardFilterDropdownResponseContract _dashboardFilterDropdownResponseContract;

            protected override async void When()
            {
                _dashboardFilterDropdownResponseContract = await SUT.JobRole();
            }

            [Test]
            public void TheGetUserJobFunctionRolesAsyncIsCalledOnce()
            {
                RoleProviderMock.Verify(m => m.GetUserJobFunctionRolesAsync(), Times.Once());
            }


            [Test]
            public void ThenDashboardFilterDropdownResponseContractIsNotNull()
            {
                Assert.IsNotNull(_dashboardFilterDropdownResponseContract);
            }

            [Test]
            public void ThenTheCorrectAmountOfDropDownsAreReturned()
            {
                const int expected = 8;

                Assert.AreEqual(expected, _dashboardFilterDropdownResponseContract.DropdownItemContracts.Count);
            }

            [Test]
            public void ThenTheCorrectTrainerIdsAreReturned()
            {
                var expected = new List<int> { 0,1,2,3,4,5,6,7 };

                CollectionAssert.AreEquivalent(expected, _dashboardFilterDropdownResponseContract.DropdownItemContracts.Select(a => a.Value));
            }

            [Test]
            public void ThenTheCorrectTrainerNamesAreReturned()
            {
                var expected = new List<string> { "All Job Functions", "Role 1", "Role 2", "Role 3", "Role 4", "Role 5", "Role 6", "Role 7" };

                CollectionAssert.AreEquivalent(expected, _dashboardFilterDropdownResponseContract.DropdownItemContracts.Select(a => a.Text));
            }
        }
    }
}
