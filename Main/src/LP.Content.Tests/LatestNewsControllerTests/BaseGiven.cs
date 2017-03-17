using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using LP.Api.Shared.Interfaces.BusinessLayer.Content;
using LP.Content.Controllers;
using LP.ServiceHost.DataContracts.Common.Content;
using LP.ServiceHost.DataContracts.Response.Content;
using Moq;
using SpecsFor;

namespace LP.Content.Tests.LatestNewsControllerTests
{
    public class BaseGiven : SpecsFor<LatestNewsController>
    {
        protected readonly Mock<IAskContentApiBusiness> AskContentApiBusinessMock = new Mock<IAskContentApiBusiness>();
        protected readonly Mock<INewsCommands> NewsCommandsMock = new Mock<INewsCommands>();
        
        protected LatestNewsResponseContract LatestNewsResponseContract = new LatestNewsResponseContract
        {
            LatestNewsItems = new List<LatestNewsItem>
            {
                new LatestNewsItem(),
                new LatestNewsItem(),
                new LatestNewsItem(),
                new LatestNewsItem()
            }
        };
        protected Mock<HttpConfiguration> HttpConfigurationMock = new Mock<HttpConfiguration>();
        protected string Culture = "en";
        protected void PrepareSut()
        {
            NewsCommandsMock.Setup(m => m.GetLatestNewsAsync(It.IsAny<CultureInfo>())).ReturnsAsync(LatestNewsResponseContract);
            
            AskContentApiBusinessMock.Setup(m => m.NewsCommands).Returns(NewsCommandsMock.Object);


            SUT = new LatestNewsController(AskContentApiBusinessMock.Object)
            {
                ControllerContext = new HttpControllerContext { Configuration = HttpConfigurationMock .Object},
                Request = new HttpRequestMessage()
            };

            SUT.Request.Headers.Clear();
            SUT.Request.Headers.Add("x-culture", Culture);
            SUT.Request.SetConfiguration(HttpConfigurationMock.Object);
        }
    }
}
