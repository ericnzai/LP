using System.Collections.Generic;
using System.Linq;
using LP.EntityModels.Exam;
using LP.Model.Authentication;
using LP.ServiceHost.DataContracts.Common.Exams;
using Moq;
using NUnit.Framework;

namespace LP.Exams.BusinessLayer.Tests.CommandTests.CertificatesAchievedCommandsTests
{
    public class GivenGettingCertificatesAchievedByUserForGroups : BaseGiven
    {
        private IEnumerable<CertificateAchievedInformation> _certificateAchievedInformations;
        protected override void Given()
        {
            CertificatesAchieved = new List<CertificatesAchieved>
            {
                new CertificatesAchieved{UserId = 1, Attempt = new Attempt(), Certificate = new Certificate()},
                new CertificatesAchieved{UserId = 1, Attempt = new Attempt(), Certificate = new Certificate()},
                new CertificatesAchieved{UserId = 1, Attempt = new Attempt(), Certificate = new Certificate()},
                new CertificatesAchieved{UserId = 1, Attempt = new Attempt(), Certificate = new Certificate()}
            };

            PrepareSut();
        }

        public class WhenTheUserHasSomeCertificates : GivenGettingCertificatesAchievedByUserForGroups
        {
            protected override async void When()
            {
                var userDetails = new UserDetails { UserId = 1 };

                _certificateAchievedInformations = await SUT.GetCertificatesAchievedByUser(userDetails);
            }

            [Test]
            public void ThenGetAllCertificatesAchievedAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<CertificatesAchieved>(), Times.Once());
            }

            [Test]
            public void ThenFilterCertificatesAchievedGetOnlyWithLiveGroupIsCalledOnce()
            {
                FilterCertificatesAchievedMock.Verify(m => m.GetOnlyWithLiveGroup(It.IsAny<IEnumerable<CertificatesAchieved>>()), Times.Once());
            }

            [Test]
            public void ThenCertificateAchievedInformationsIsNotNull()
            {
                Assert.IsNotNull(_certificateAchievedInformations);
            }

            [Test]
            public void ThenTheCorrectAmountOfCertificatesAchievedAreReturned()
            {
                const int expected = 4;

                Assert.AreEqual(expected, _certificateAchievedInformations.Count());
            }

            [Test]
            public void ThenAllCertificatesAchievedAreUnique()
            {
                CollectionAssert.AllItemsAreUnique(_certificateAchievedInformations);
            }
        }
    }
}
