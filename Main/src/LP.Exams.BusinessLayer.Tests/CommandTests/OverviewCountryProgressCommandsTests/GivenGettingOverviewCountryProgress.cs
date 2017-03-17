using System.Linq;
using LP.EntityModels;
using LP.EntityModels.Exam;
using LP.ServiceHost.DataContracts.Common.Exams.Dashboards;
using LP.ServiceHost.DataContracts.Response.Exams;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace LP.Exams.BusinessLayer.Tests.CommandTests.OverviewCountryProgressCommandsTests
{
	public class GivenGettingOverviewCountryProgress : BaseGiven
	{
		private OverviewCountryProgressResponseContract _overviewCountryProgressResponseContract;

		protected override void Given()
		{
			PrepareSut();
		}

		public class WhenMakingACorrectRequestAllTheDependenciesAreCalledCorrectly : GivenGettingOverviewCountryProgress
		{
			protected override async void When()
			{
				_overviewCountryProgressResponseContract = await SUT.GetOverviewCountryProgressResponseContract();
			}

			[Test]
			public void ThenOverviewCountryProgressResponseContractIsNotNull()
			{
				Assert.IsNotNull(_overviewCountryProgressResponseContract);
			}

			[Test]
			public void ThenGetCountriesIsCalledOnce()
			{
				BaseCommandsMock.Verify(m => m.GetAllAsync<Country>(), Times.Once());
			}

			[Test]
			public void ThenGetAllAvailableGroupTypesIsCalledOnce()
			{
				GroupTypeCommandsMock.Verify(m => m.GetAllAvailableGroupTypes(), Times.Once());
			}

			[Test]
			public void ThenRoleProviderGetAllCultureRolesIsCalledOnce()
			{
				RoleProviderMock.Verify(m => m.GetAllUserCultureRolesAsync(), Times.Once());
			}

			[Test]
			public void ThenGetAllLiveGroupsIsCalledOnce()
			{
				FilterAllowedGroupsMock.Verify(m => m.GetAllLiveGroups(), Times.Once());
			}

			[Test]
			public void ThenGetAllCertificatesAchievedIsCalledOnce()
			{
				BaseCommandsMock.Verify(m => m.GetAllAsync<CertificatesAchieved>(), Times.Once());
			}

			[Test]
			public void ThenAllowedUserFilterGetAllLiveUsersNotHiddenFromReportsIsCalledOnce()
			{
				AllowedUserFilterMock.Verify(m => m.GetAllLiveUsersNotHiddenFromReports(), Times.Once());
			}

			[Test]
			public void ThenAllowedUserFilterGetUsersFilteredByCountryIsCalledTheCorrectAmountOfTimes()
			{
				int expected = Countries.Count();

				AllowedUserFilterMock.Verify(m => m.GetUsersFilteredByCountry(It.IsAny<int>(), It.IsAny<IEnumerable<User>>()), Times.Exactly(expected));
			}
           

		}

        public class WhenMakingACorrectRequestCountryPerformanceContractsAreReturend : GivenGettingOverviewCountryProgress
		{
			protected override async void When()
			{
				_overviewCountryProgressResponseContract = await SUT.GetOverviewCountryProgressResponseContract();
			}

			[Test]
			public void ThenCountryPerformanceContractIsNotNull()
			{
				Assert.IsNotNull(_overviewCountryProgressResponseContract.CountryPerformanceContracts);
			}

            [Test]
            public void ThenCountryPerformanceContractIsOneOrMore()
            {
                Assert.GreaterOrEqual(_overviewCountryProgressResponseContract.CountryPerformanceContracts.Count(), 1);
            }
		}

		public class WhenCheckingTheResultsForTheFirstCountry : GivenGettingOverviewCountryProgress
		{
			private CountryPerformanceContract _countryPerformanceContract;

			protected override async void When()
			{
				_overviewCountryProgressResponseContract = await SUT.GetOverviewCountryProgressResponseContract();

                _countryPerformanceContract = _overviewCountryProgressResponseContract.CountryPerformanceContracts.First(x => x.CountryId == CountryEn.CountryID);
			}

			[Test]
			public void ThenTheCountryHasTheCorrectName()
			{
				Assert.AreEqual(CountryEn.CountryName, _countryPerformanceContract.CountryName);
			}

            [Test]
            public void ThenTheCountryHasTheCorrectCountryId()
            {
                Assert.AreEqual(CountryEn.CountryID, _countryPerformanceContract.CountryId);
            }

		    [Test]
		    public void ThenTheCorrectAmountOfAreReturned()
		    {
                Assert.AreEqual(CultureRoles.Count(), _countryPerformanceContract.CountryPerformanceCultureContracts.Count);
		    }

            [Test]
            public void ThenTheTotalNumberOfUsersIsCorrect()
            {
                Assert.AreEqual(Users.Count(), _countryPerformanceContract.TotalNumberOfUsers);
            }
		}

        public class WhenCheckingTheCountryPerformanceCultureContractsForTheFirstCountry : GivenGettingOverviewCountryProgress
        {
            private CountryPerformanceContract _countryPerformanceContract;
            private CountryPerformanceCultureContract _countryPerformanceCultureContractEn;

            protected override async void When()
            {
                _overviewCountryProgressResponseContract = await SUT.GetOverviewCountryProgressResponseContract();

                _countryPerformanceContract = _overviewCountryProgressResponseContract.CountryPerformanceContracts.First(x => x.CountryId == CountryEn.CountryID);

                _countryPerformanceCultureContractEn = _countryPerformanceContract.CountryPerformanceCultureContracts.FirstOrDefault(
                        x => x.CultureCode == CultureRoleEn.RoleName);

                
            }

            [Test]
            public void ThenTheFirstCountryPerformanceCultureContractIsNotNull()
            {
                Assert.IsNotNull(_countryPerformanceCultureContractEn);
            }

            [Test]
            public void ThenTheCorrectAmountOfAreReturned()
            {
                Assert.AreEqual(CultureRoles.Count(), _countryPerformanceContract.CountryPerformanceCultureContracts.Count);
            }

            [Test]
            public void ThenTheEnCountryPerformanceCultureContractCultureCodeIsCorrect()
            {
                Assert.AreEqual(CultureRoleEn.RoleName, _countryPerformanceCultureContractEn.CultureCode);
            }

            [Test]
            public void ThenTheEnCountryPerformanceCultureContractCultureDescriptionIsCorrect()
            {
                Assert.AreEqual(CultureRoleEn.Description, _countryPerformanceCultureContractEn.CultureDescription);
            }

            //[Test]
            //public void ThenNumberOfUsersWithAccessAreCorrect()
            //{
            //    Assert.AreEqual(3, _countryPerformanceCultureContractEn.NumberOfUsersWithAccess);
            //}
        }

        public class  WhenMakingACorrectRequestForFilteredCountryPerformanceContracts : GivenGettingOverviewCountryProgress
        {
            private CountryPerformanceContract _countryPerformanceContract;
            private CountryPerformanceCultureContract _countryPerformanceCultureContractEn;
            protected override async void When()
            {
                string jobRoleIds = "83,7,84";
                _overviewCountryProgressResponseContract =
                    await SUT.GetOverviewCountryProgressFilteredByJobFunction(jobRoleIds);
            }


            [Test]
            public void ThenCountryPerformanceContractIsThree()
            {
                Assert.AreEqual(_overviewCountryProgressResponseContract.CountryPerformanceContracts.Count(), 3);
            }

        }

	    public class WhenMakingACorrectRequestForFilteredRegionalCountryPerformanceContracts :
	        GivenGettingOverviewCountryProgress
	    {
	        protected override async void When()
	        {
	            string jobRoleIds = "7,83,84";
	            int regionId = 4;
                

	            _overviewCountryProgressResponseContract =
	                await SUT.GetOverviewCountryProgressForRegionFilteredResponseContract(jobRoleIds, regionId);
	        }

	        [Test]
	        public void ThenCountryPerformanceContractIsNotNull()
	        {
	            Assert.IsNotNull(_overviewCountryProgressResponseContract.CountryPerformanceContracts);
	        }

	       

	       
	    }
    }
}
