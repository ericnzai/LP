using System.Net.Http;
using System.Web.Http;
using LP.Api.Shared.Interfaces.BusinessLayer.Content;
using LP.Content.Controllers;
using LP.ServiceHost.DataContracts.Response.Content;
using Moq;
using SpecsFor;

namespace LP.Content.Tests.DashboardFilterCommandsTests
{
    public class BaseGiven : SpecsFor<DashboardFilterController>
    {
        protected readonly Mock<IAskContentApiBusiness> AskContentApiBusinessMock = new Mock<IAskContentApiBusiness>();
        protected readonly Mock<IDropdownFilterCommands> DropdownFilterCommandsMock = new Mock<IDropdownFilterCommands>();
        
        protected void PrepareSut()
        {
            AskContentApiBusinessMock.Setup(m => m.DropdownFilterCommands).Returns(DropdownFilterCommandsMock.Object);

            DropdownFilterCommandsMock.Setup(m => m.Country(It.IsAny<int>()))
                .ReturnsAsync(new DashboardFilterDropdownResponseContract());

            DropdownFilterCommandsMock.Setup(m => m.Country())
                .ReturnsAsync(new DashboardFilterDropdownResponseContract());

            SUT = new DashboardFilterController(AskContentApiBusinessMock.Object)
            {
                Configuration = new HttpConfiguration(),
                Request = new HttpRequestMessage(),
            };

            SUT.Request.SetConfiguration(new HttpConfiguration());
        }
    }
}
