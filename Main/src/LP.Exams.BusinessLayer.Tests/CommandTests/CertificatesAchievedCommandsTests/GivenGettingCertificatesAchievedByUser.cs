using System.Collections.Generic;
using LP.EntityModels.Exam;
using LP.Model.Authentication;
using LP.ServiceHost.DataContracts.Common.Exams;
using Moq;
using NUnit.Framework;

namespace LP.Exams.BusinessLayer.Tests.CommandTests.CertificatesAchievedCommandsTests
{
    public class GivenGettingCertificatesAchievedByUser : BaseGiven
    {
        private IEnumerable<CertificateAchievedInformation> _certificateAchievedInformations;
        private const int UserId = 2;
        protected override void Given()
        {
            CertificatesAchieved = new List<CertificatesAchieved>
            {
                new CertificatesAchieved{GroupId = 1, UserId = UserId}
            };

            PrepareSut();
        }

        public class WhenTheUserHasSomeCertificates : GivenGettingCertificatesAchievedByUser
        {
            protected override async void When()
            {
                var userDetails = new UserDetails { UserId = UserId };

                var groupIds = new List<int>{1,2,3};

                _certificateAchievedInformations = await SUT.GetCertificatesAchievedByUserForGroups(userDetails, groupIds);
            }

            [Test]
            public void ThenGetAllCertificatesAchievedAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<CertificatesAchieved>(), Times.Once());
            }

            [Test]
            public void ThenFilterCertificatesAchievedGetOnlyWithLiveGroupIsNeverCalled()
            {
                FilterCertificatesAchievedMock.Verify(m => m.GetOnlyWithLiveGroup(It.IsAny<IEnumerable<CertificatesAchieved>>()), Times.Never());
            }

            [Test]
            public void ThenCertificateAchievedInformationsIsNotNull()
            {
                Assert.IsNotNull(_certificateAchievedInformations);
            }
        }
    }
}
