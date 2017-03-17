using System.Collections.Specialized;
using System.Web;
using System.Web.Routing;
using Moq;
using NUnit.Framework;
using SpecsFor.Helpers.Web.Mvc;

namespace LP.PresentationLayer.Tests.FilterTests.CultureFilterTests
{
    public class GivenACultureHasBeenSetInRequestContext : BaseGiven
    {
        private FakeActionExecutingContext _filterContext;

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenTheCultureIsNotSetInTheRequestHeader : GivenACultureHasBeenSetInRequestContext
        {
            protected override void When()
            {
                _filterContext = new FakeActionExecutingContext();
                SUT.OnActionExecuting(_filterContext);
            }

            [Test]
            public void ThenFilterContextIsNotNull()
            {
                Assert.IsNotNull(_filterContext);
            }

            [Test]
            public void ThenCultureIsSetCorrectlyInRouteData()
            {
                Assert.AreEqual(_filterContext.RouteData.Values["Culture"], "en");
            }
        }

        public class WhenTheCultureIsSetInTheRequestHeader : GivenACultureHasBeenSetInRequestContext
        {
            private const string HeaderCultureValue = "zh";

            protected override void When()
            {
                
                var requestContextMock = new Mock<RequestContext>();

                var httpContextBase = new Mock<HttpContextBase>();
                var httpRequestBase = new Mock<HttpRequestBase>();

                var nameValueCollection = new NameValueCollection { { "x-culture", "zh" } };
                httpRequestBase.Setup(m => m.Headers).Returns(nameValueCollection);

                httpContextBase.Setup(m => m.Request).Returns(httpRequestBase.Object);
                requestContextMock.Setup(m => m.HttpContext).Returns(httpContextBase.Object);

                var routeDataMock = new Mock<RouteData>();

                _filterContext = new FakeActionExecutingContext
                {
                    RequestContext = requestContextMock.Object,
                    RouteData = routeDataMock.Object
                };

                SUT.OnActionExecuting(_filterContext);
            }

            [Test]
            public void ThenFilterContextIsNotNull()
            {
                Assert.IsNotNull(_filterContext);
            }

            [Test]
            public void ThenCultureIsSetCorrectlyInRouteData()
            {
                Assert.AreEqual(HeaderCultureValue, _filterContext.RouteData.Values["Culture"]);
            }
        }
    }
}
