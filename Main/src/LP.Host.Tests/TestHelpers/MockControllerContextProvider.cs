using System;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Routing;
using Moq;

namespace LP.Host.Tests.TestHelpers
{
    public static class MockControllerContextProvider
    {
        public static Mock<HttpControllerContext> GetBasic(bool isRequestAuthenticated, int userId,
            string userIdentityName, ApiController sut)
        {
            var mockHttpContextBase = new Mock<HttpContextBase>();
            var mockHttpRequestBase = new Mock<HttpRequestBase>();

            var mockHttpSessionStateBase = new Mock<HttpSessionStateBase>();
            var mockHttpResponseBase = new Mock<HttpResponseBase>();
            var mockHttpServerUtilityBase = new Mock<HttpServerUtilityBase>();


            var routeData = new RouteData();

            //var uri = new Uri("http://localhost");

            mockHttpRequestBase.Setup(x => x.RequestContext).Returns(new RequestContext());
            mockHttpRequestBase.Setup(x => x.RequestContext.RouteData).Returns(routeData);

            mockHttpRequestBase.SetupGet(x => x.Url).Returns(new Uri("http://localhost/a", UriKind.Absolute));
            mockHttpRequestBase.Setup(x => x.IsAuthenticated).Returns(isRequestAuthenticated);

            mockHttpRequestBase.Setup(r => r.AppRelativeCurrentExecutionFilePath).Returns("/");
            //mockHttpRequestBase.Setup(r => r.UrlReferrer).Returns(uri);
            mockHttpRequestBase.Setup(r => r.ApplicationPath).Returns("/");

            mockHttpResponseBase.Setup(s => s.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(s => s);
            mockHttpResponseBase.SetupProperty(res => res.StatusCode, (int) System.Net.HttpStatusCode.OK);

            mockHttpServerUtilityBase.Setup(m => m.MapPath(It.IsAny<string>())).Returns("c:/mapped");

            mockHttpContextBase.Setup(x => x.Session).Returns(mockHttpSessionStateBase.Object);
            mockHttpContextBase.Setup(x => x.Server).Returns(mockHttpServerUtilityBase.Object);
            mockHttpContextBase.Setup(x => x.Request).Returns(mockHttpRequestBase.Object);
            mockHttpContextBase.Setup(x => x.Response).Returns(mockHttpResponseBase.Object);
            var fakeIdentity = new GenericIdentity(userIdentityName);

            var principal = new GenericPrincipal(fakeIdentity, new string[] {"Admin"});

            mockHttpContextBase.Setup(m => m.User).Returns(principal);
            mockHttpContextBase.Setup(m => m.User.Identity.Name).Returns(userIdentityName);

            var mockControllerContext = new Mock<HttpControllerContext>(mockHttpContextBase.Object, routeData, sut);
            //mockControllerContext.Setup(m => m.).Returns(mockHttpContextBase.Object);
            //mockControllerContext.SetupGet(p => p.HttpContext.User).Returns(principal);
            //mockControllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns(userIdentityName);
            //mockControllerContext.SetupGet(p => p.HttpContext.Request).Returns(mockHttpRequestBase.Object);
            //var config = new HttpConfiguration();
            //var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/api/test");
            //var route = config.Routes.MapHttpRoute("RouteName", "api/{controller}/{id}");
            //var routeData = new HttpRouteData(route, new HttpRouteValueDictionary { { "controller", "Account" } });


            return mockControllerContext;
        }

        //public static MockHttpObject GetBMockHttpObject(bool isRequestAuthenticated, int userId, string userIdentityName, Controller sut, string userRoleSelected = "Delegate")
        //{
        //    var mockHttpContextBase = new Mock<HttpContextBase>();
        //    var mockHttpRequestBase = new Mock<HttpRequestBase>();

        //    var mockHttpSessionStateBase = new Mock<HttpSessionStateBase>();
        //    var mockHttpResponseBase = new Mock<HttpResponseBase>();
        //    var mockHttpServerUtilityBase = new Mock<HttpServerUtilityBase>();
        //    var formNameValueCollection = new NameValueCollection { { "title-radio", "1" }, { "userid", userId.ToString() } };

        //    if (!string.IsNullOrEmpty(userRoleSelected))
        //    {
        //        formNameValueCollection.Add("UserRole", userRoleSelected);
        //    }

        //    var routeData = new RouteData();

        //    //var uri = new Uri("http://localhost");

        //    mockHttpRequestBase.Setup(x => x.RequestContext).Returns(new RequestContext());
        //    mockHttpRequestBase.Setup(x => x.RequestContext.RouteData).Returns(routeData);
        //    mockHttpRequestBase.Setup(x => x.Form).Returns(formNameValueCollection);
        //    mockHttpRequestBase.SetupGet(x => x.Url).Returns(new Uri("http://localhost/a", UriKind.Absolute));
        //    mockHttpRequestBase.Setup(x => x.IsAuthenticated).Returns(isRequestAuthenticated);
        //    mockHttpRequestBase.Setup(m => m.QueryString).Returns(formNameValueCollection);
        //    mockHttpRequestBase.Setup(r => r.AppRelativeCurrentExecutionFilePath).Returns("/");
        //    //mockHttpRequestBase.Setup(r => r.UrlReferrer).Returns(uri);
        //    mockHttpRequestBase.Setup(r => r.ApplicationPath).Returns("/");

        //    mockHttpResponseBase.Setup(s => s.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(s => s);
        //    mockHttpResponseBase.SetupProperty(res => res.StatusCode, (int)System.Net.HttpStatusCode.OK);

        //    mockHttpServerUtilityBase.Setup(m => m.MapPath(It.IsAny<string>())).Returns("c:/mapped");

        //    mockHttpContextBase.Setup(x => x.Session).Returns(mockHttpSessionStateBase.Object);
        //    mockHttpContextBase.Setup(x => x.Server).Returns(mockHttpServerUtilityBase.Object);
        //    mockHttpContextBase.Setup(x => x.Request).Returns(mockHttpRequestBase.Object);
        //    mockHttpContextBase.Setup(x => x.Response).Returns(mockHttpResponseBase.Object);
        //    var fakeIdentity = new GenericIdentity(userIdentityName);

        //    var principal = new GenericPrincipal(fakeIdentity, new string[] { "Admin" });

        //    mockHttpContextBase.Setup(m => m.User).Returns(principal);
        //    mockHttpContextBase.Setup(m => m.User.Identity.Name).Returns(userIdentityName);

        //    var mockControllerContext = new Mock<ControllerContext>(mockHttpContextBase.Object, routeData, sut);
        //    mockControllerContext.Setup(m => m.HttpContext).Returns(mockHttpContextBase.Object);
        //    mockControllerContext.SetupGet(p => p.HttpContext.User).Returns(principal);
        //    mockControllerContext.SetupGet(p => p.HttpContext.User.Identity.Name).Returns(userIdentityName);
        //    mockControllerContext.SetupGet(p => p.HttpContext.Request).Returns(mockHttpRequestBase.Object);

        //    var mockHttpObject = new MockHttpObject
        //    {
        //        MockControllerContext = mockControllerContext,
        //        MockHttpContextBase = mockHttpContextBase,
        //        MockHttpRequestBase = mockHttpRequestBase,
        //        MockHttpResponseBase = mockHttpResponseBase,
        //        MockHttpServerUtilityBase = mockHttpServerUtilityBase,
        //        MockHttpSessionStateBase = mockHttpSessionStateBase
        //    };

        //    return mockHttpObject;
        //}
    }
}
