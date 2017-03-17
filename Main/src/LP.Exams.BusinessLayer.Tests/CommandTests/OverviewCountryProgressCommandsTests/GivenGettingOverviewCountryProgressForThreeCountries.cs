using LP.EntityModels.Exam;
using LP.ServiceHost.DataContracts.Response.Exams;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LP.Exams.BusinessLayer.Tests.CommandTests.OverviewCountryProgressCommandsTests
{
    public class GivenGettingOverviewCountryProgressForThreeCountries : BaseGiven
    {
        private static OverviewCountryProgressResponseContract _overviewCountryProgressResponseContract;

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenMakingACorrectRequestAllCalculationsAreReturnedCorrectly : GivenGettingOverviewCountryProgress
        {
            protected override async void When()
            {
                _overviewCountryProgressResponseContract = await SUT.GetOverviewCountryProgressResponseContract();
            }

            //[Test]
            //public void ThenCountryPerformanceContractContainsTheCorrectAmountOfCountries()
            //{
            //    int expected = Countries.Count();

            //    Assert.AreEqual(expected, _overviewCountryProgressResponseContract.CountryPerformanceContracts.Count);
            //}

            //[Test]
            //public void ThenGetNumberOfUsersCertififedForGroupTypeByCountryIsCalledTheCorrectNumberOfTimes()
            //{
            //    const int expected = 24;

            //    CertificatesAchievedCommandsMock.Verify(m => m.GetNumberOfUsersCertififedForGroupTypeByCountry(It.IsAny<int>(), It.IsAny<IEnumerable<int>>(), It.IsAny<IEnumerable<CertificatesAchieved>>()), Times.Exactly(expected));
            //}

        }
    }
}
