using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;
using LP.Api.Shared.Interfaces.BusinessLayer.Content;
using LP.Api.Shared.Providers;
using LP.Api.Shared.Requests;
using LP.ServiceHost.DataContracts.Common.Translation;
using LP.ServiceHost.DataContracts.Request.Content;
using LP.ServiceHost.DataContracts.Response.Content;
using Moq;
using SpecsFor;
using LP.Content.Controllers;

namespace LP.Content.Tests.GlossaryControllerTests.GlossaryPDFControllerTests
{
    public class BaseGiven : SpecsFor<GlossaryPdfController>
    {
        protected readonly Mock<IAskContentApiBusiness> AskContentApiBusinessMock = new Mock<IAskContentApiBusiness>();
        protected readonly Mock<IGlossaryPdfCommands> GlossaryPdfCommandsMock = new Mock<IGlossaryPdfCommands>();
        protected Mock<GlossaryItemController> GlossaryItemControllerMock = new Mock<GlossaryItemController>();
        protected Mock<IRequestExecutor> RequestExecutorMock = new Mock<IRequestExecutor>();
        protected string Culture = "en";
        protected string FiltersValue = "search_";
        protected string SortValue = "asc";

        protected readonly GlossaryPdfRequestContract GlossaryPdfRequestContract = new GlossaryPdfRequestContract
        {
            TranslatedItems = new List<TranslatedItem>()
            {
                new TranslatedItem
                {
                    ResourceId = "ltGlossary.Text",
                    ResourceSet =TranslatedItemResourceSetProvider.MasterMasterPage
                },
                new TranslatedItem
                {
                    ResourceId = "msgSorryNoResults",
                    ResourceSet = TranslatedItemResourceSetProvider.MasterMasterPage
                },
                new TranslatedItem
                {
                    ResourceId = "msgFollowingFiltersSelected",
                    ResourceSet = TranslatedItemResourceSetProvider.MasterMasterPage
                },
                new TranslatedItem
                {
                    ResourceId = "msgFollowingFiltersSelectedForSearch",
                    ResourceSet = TranslatedItemResourceSetProvider.MasterMasterPage
                },
                new TranslatedItem
                {
                    ResourceId = "msgFollowingFiltersSelectedForTrainingModules",
                    ResourceSet = TranslatedItemResourceSetProvider.MasterMasterPage
                }
            }
        };

        protected readonly GlossaryFilteredPdfRequestContract GlossaryFilteredPdfRequestContract = new GlossaryFilteredPdfRequestContract
        {
            TranslatedItems = new List<TranslatedItem>()
            {
                new TranslatedItem
                {
                    ResourceId = "ltGlossary.Text",
                    ResourceSet = TranslatedItemResourceSetProvider.MasterMasterPage
                },
                new TranslatedItem
                {
                    ResourceId = "msgSorryNoResults",
                    ResourceSet = TranslatedItemResourceSetProvider.MasterMasterPage
                },
                new TranslatedItem
                {
                    ResourceId = "msgFollowingFiltersSelected",
                    ResourceSet = TranslatedItemResourceSetProvider.MasterMasterPage
                },
                new TranslatedItem
                {
                    ResourceId = "msgFollowingFiltersSelectedForSearch",
                    ResourceSet = TranslatedItemResourceSetProvider.MasterMasterPage
                },
                new TranslatedItem
                {
                    ResourceId = "msgFollowingFiltersSelectedForTrainingModules",
                    ResourceSet = TranslatedItemResourceSetProvider.MasterMasterPage
                }
            },
            Filters = "search_",
            Sort = "asc"
        };

        protected void PrepareSut()
        {
            AskContentApiBusinessMock.Setup(m => m.GlossaryPdfCommands).Returns(GlossaryPdfCommandsMock.Object);
            GlossaryPdfCommandsMock.Setup(m => m.GetWholeGlossaryPdfContent(Culture, GlossaryPdfRequestContract.TranslatedItems))
                .ReturnsAsync(new GlossaryPDFResponseContract());
            GlossaryPdfCommandsMock.Setup(m => m.GetFilteredGlossaryPdfContent(Culture, GlossaryFilteredPdfRequestContract.TranslatedItems, FiltersValue, SortValue))
                .ReturnsAsync(new GlossaryPDFResponseContract());

            RequestExecutorMock.Setup(m => m.ExecutePostAsync(It.IsAny<string>(), It.IsAny<List<KeyValuePair<string, string>>>(), It.IsAny<ContentType>(), It.IsAny<bool>()))
                .ReturnsAsync(new HttpResponseMessage());

            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/test");
            request.Headers.Add("x-culture", Culture);
            var route = config.Routes.MapHttpRoute("RouteName", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "Account" } });

            var glossaryPdfController = new GlossaryPdfController(AskContentApiBusinessMock.Object)
            {
                ControllerContext = new HttpControllerContext(config, routeData, request)
            };

            SUT = glossaryPdfController;
        }
    }
}
