using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LP.Api.Shared.Interfaces.BusinessLayer.Exams.Filters;
using LP.Api.Shared.Interfaces.Data;
using LP.EntityModels.Exam;
using LP.Exams.BusinessLayer.Commands;
using Moq;
using SpecsFor;

namespace LP.Exams.BusinessLayer.Tests.CommandTests.CertificatesAchievedCommandsTests
{
    public class BaseGiven : SpecsFor<CertificatesAchievedCommands>
    {
        protected readonly Mock<IBaseCommands> BaseCommandsMock = new Mock<IBaseCommands>();
        protected readonly Mock<IFilterCertificatesAchieved> FilterCertificatesAchievedMock = new Mock<IFilterCertificatesAchieved>();
        protected readonly Mock<IFilterAllowedUser> FilterAllowedUserMock = new Mock<IFilterAllowedUser>();
        protected readonly Mock<IFilterAllowedGroups> FilterAllowedGroups = new Mock<IFilterAllowedGroups>();
        protected List<CertificatesAchieved> CertificatesAchieved = new List<CertificatesAchieved>();
        
        protected void PrepareSut()
        {
            BaseCommandsMock.Setup(m => m.GetAllAsync<CertificatesAchieved>()).ReturnsAsync(CertificatesAchieved.AsQueryable());

            FilterCertificatesAchievedMock.Setup(
                m => m.GetOnlyWithLiveGroup(It.IsAny<IEnumerable<CertificatesAchieved>>()))
                .ReturnsAsync(CertificatesAchieved);

            SUT = new CertificatesAchievedCommands(BaseCommandsMock.Object, 
                FilterCertificatesAchievedMock.Object, 
                FilterAllowedUserMock.Object, 
                FilterAllowedGroups.Object);
        }
    }
}
