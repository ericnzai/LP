using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;
using LP.Api.Shared.Binding;
using LP.Api.Shared.Interfaces.BusinessLayer.Content;
using LP.Content.Controllers;
using LP.ServiceHost.DataContracts.Common.Content;
using LP.ServiceHost.DataContracts.Response.Content;
using Moq;
using SpecsFor;

namespace LP.Content.Tests.GlossaryControllerTests.GlossaryItemControllerTests
{
    public class BaseGiven : SpecsFor<GlossaryItemController>
    {
        protected readonly Mock<IAskContentApiBusiness> AskContentApiBusinessMock = new Mock<IAskContentApiBusiness>();
        protected readonly Mock<IGlossaryCommands> GlossaryCommandsMock = new Mock<IGlossaryCommands>();
        protected Mock<GlossaryItemController> GlossaryItemControllerMock = new Mock<GlossaryItemController>();
        protected Mock<HttpContentBinding> HttpContentBindingMock = new Mock<HttpContentBinding>();
        protected string Culture = "en";
        protected GlossaryItemsResponseContract GlossaryItemsResponseContract = new GlossaryItemsResponseContract
        {
            GlossaryItems = new List<GlossaryItem> ()
            {
                new GlossaryItem()
                {
                    Title="Glossary 1",
                    Description= "Description glossary 1"
                },
                new GlossaryItem()
                {
                    Title="Glossary 1",
                    Description= "Description glossary 1"
                },
                new GlossaryItem()
                {
                    Title="Glossary 1",
                    Description= "Description glossary 1"
                },
            }
        };

        protected void PrepareSut()
        {
            AskContentApiBusinessMock.Setup(m => m.GlossaryCommands).Returns(GlossaryCommandsMock.Object);
            GlossaryCommandsMock.Setup(m => m.GetAllGlossaryItems(Culture))
                .ReturnsAsync(GlossaryItemsResponseContract);
            
            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/test");
            request.Headers.Add("x-culture", Culture);
            var route = config.Routes.MapHttpRoute("RouteName", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "Account" } });

            var glossaryItemController = new GlossaryItemController(AskContentApiBusinessMock.Object)
            {
                ControllerContext = new HttpControllerContext(config, routeData, request)
               
            };

            SUT = glossaryItemController;
        }
    }
}
