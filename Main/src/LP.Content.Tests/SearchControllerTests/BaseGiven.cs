using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;
using LP.Api.Shared.Interfaces.BusinessLayer.Content;
using LP.Api.Shared.Requests;
using LP.Content.Controllers;
using LP.ServiceHost.DataContracts.Common.Content;
using LP.ServiceHost.DataContracts.Request.Content;
using LP.ServiceHost.DataContracts.Response.Content;
using Moq;
using SpecsFor;

namespace LP.Content.Tests.SearchControllerTests
{
    public class BaseGiven : SpecsFor<SearchController>
    {
        protected string Culture = "en";
        protected string Search = "test";
        protected string GroupTypeId = "0";
        protected List<int> RoleIds = new List<int>();
        protected readonly Mock<IAskContentApiBusiness> AskContentApiBusinessMock = new Mock<IAskContentApiBusiness>();
        protected readonly Mock<ISearchCommands> SearchCommandsMock = new Mock<ISearchCommands>();
        protected Mock<IRequestExecutor> RequestExecutorMock = new Mock<IRequestExecutor>();
        protected SearchItemsResponseContract SearchItemsResponseContract = new SearchItemsResponseContract
        {
            SearchItems = new List<SearchItem>()
            {
                new SearchItem()
                {
                    Title="Search 1",
                    Description= "Description search 1"
                },
                new SearchItem()
                {
                    Title="Search 2",
                    Description= "Description search 2"
                },
                new SearchItem()
                {
                    Title="Search 3",
                    Description= "Description search 3"
                },
            }
        };

        protected SearchRequestContract _searchRequestContract = new SearchRequestContract()
        {
            SearchTerm = "test",
            GroupTypeId = "0"
        };

        protected void PrepareSut()
        {
            AskContentApiBusinessMock.Setup(m => m.SearchCommands).Returns(SearchCommandsMock.Object);
            SearchCommandsMock.Setup(m => m.GetAllSearchItems(Culture, _searchRequestContract.SearchTerm, RoleIds, _searchRequestContract.GroupTypeId, _searchRequestContract.TopicIds))
                .ReturnsAsync(SearchItemsResponseContract);

            SearchCommandsMock.Setup(m => m.GetAllSearchItems(Culture, _searchRequestContract.SearchTerm, RoleIds, _searchRequestContract.GroupTypeId, _searchRequestContract.TopicIds))
                .ReturnsAsync(SearchItemsResponseContract);

            RequestExecutorMock.Setup(m => m.ExecutePostAsync(It.IsAny<string>(), It.IsAny<List<KeyValuePair<string, string>>>(), It.IsAny<ContentType>(), It.IsAny<bool>()))
                .ReturnsAsync(new HttpResponseMessage());

            var config = new HttpConfiguration();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/test");
            request.Headers.Add("x-culture", Culture);
            var route = config.Routes.MapHttpRoute("RouteName", "api/{controller}/{id}");
            var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "Account" } });

            var searchController = new SearchController(AskContentApiBusinessMock.Object)
            {
                ControllerContext = new HttpControllerContext(config, routeData, request)
            };

            SUT = searchController;
        }
    }
}
