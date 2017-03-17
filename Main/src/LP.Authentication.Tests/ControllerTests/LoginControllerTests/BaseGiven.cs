using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;
using LP.Api.Shared.Binding;
using LP.Api.Shared.Interfaces.BusinessLayer.Authentication;
using LP.Api.Shared.Requests;
using LP.Authentication.Controllers;
using LP.Model.Authentication;
using LP.ServiceHost.DataContracts.Enums;
using Moq;
using SpecsFor;

namespace LP.Authentication.Tests.ControllerTests.LoginControllerTests
{
    public class BaseGiven : SpecsFor<LoginController>
    {
        protected Mock<IAskAuthenticationApiBusiness> AskAuthenticationApiBusinessMock = new Mock<IAskAuthenticationApiBusiness>();
        protected Mock<IUserCommands> UserCommandsMock = new Mock<IUserCommands>();
        protected Mock<IRequestExecutor> RequestExecutorMock = new Mock<IRequestExecutor>();
        protected Mock<IHttpContentBinding> HttpContentBindingMock = new Mock<IHttpContentBinding>();
        protected HttpResponseStatus HttpResponseStatus = HttpResponseStatus.Success;
        protected Mock<HttpControllerContext>  HttpControllerContextMock = new Mock<HttpControllerContext>();
 
        protected void PrepareSut()
        {
            HttpContentBindingMock.Setup(m => m.DeserialiseJson<AccessTokenModel>(It.IsAny<HttpContent>()))
                .Returns(new AccessTokenModel());

            UserCommandsMock.Setup(m => m.AuthenticateUserAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(HttpResponseStatus);

            AskAuthenticationApiBusinessMock.Setup(m => m.UserCommands).Returns(UserCommandsMock.Object);

            RequestExecutorMock.Setup(m => m.ExecutePostAsync(It.IsAny<string>(), It.IsAny<List<KeyValuePair<string, string>>>(), It.IsAny<ContentType>(), It.IsAny<bool>()))
                .ReturnsAsync(new HttpResponseMessage());

            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/test");
            var route = config.Routes.MapHttpRoute("RouteName", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "Account" } });

            var accountController = new LoginController(AskAuthenticationApiBusinessMock.Object)
            {
                RequestExecutor = RequestExecutorMock.Object,
                HttpContentBinding = HttpContentBindingMock.Object,
                ControllerContext = new HttpControllerContext(config, routeData, request)
            };

            SUT = accountController;

        }
    }
}
