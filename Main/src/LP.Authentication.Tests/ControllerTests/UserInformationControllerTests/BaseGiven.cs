using LP.Api.Shared.Interfaces.BusinessLayer.Authentication;
using LP.Authentication.Controllers;
using LP.Model.Authentication;
using LP.ServiceHost.DataContracts.Response.Authentication;
using Moq;
using SpecsFor;

namespace LP.Authentication.Tests.ControllerTests.UserInformationControllerTests
{
    public class BaseGiven : SpecsFor<UserInformationController>
    {
        protected readonly Mock<IAskAuthenticationApiBusiness> AskAuthenticationApiBusinessMock = new Mock<IAskAuthenticationApiBusiness>();
        protected readonly Mock<IUserCommands> UserCommandsMock = new Mock<IUserCommands>();
        protected UserInformationResponseContract UserInformationResponseContract = new UserInformationResponseContract();

        protected void PrepareSut()
        {
            UserCommandsMock.Setup(m => m.GetUserInformationAsync(It.IsAny<UserDetails>()))
                .ReturnsAsync(UserInformationResponseContract);

            AskAuthenticationApiBusinessMock.Setup(m => m.UserCommands).Returns(UserCommandsMock.Object);

            SUT = new UserInformationController(AskAuthenticationApiBusinessMock.Object);
        }
    }
}
