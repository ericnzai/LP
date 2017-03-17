using System.Globalization;
using System.Web.Http;
using LP.Api.Shared.Tests.TestHelpers;
using LP.ServiceHost.DataContracts.Response.Content;
using Moq;
using NUnit.Framework;

namespace LP.Content.Tests.LatestNewsControllerTests
{
    public class GivenGettingTheLatestNews : BaseGiven
    {
        private IHttpActionResult _httpActionResult;

        private LatestNewsResponseContract _latestNewsResponseContract;
        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenProvidingTheGlobalCultureInTheRequestHeader : GivenGettingTheLatestNews
        {
            protected override async void When()
            {
                _httpActionResult = await SUT.Get();

                _latestNewsResponseContract = await DeserializationHelper.GetDeserializedResponseContent<LatestNewsResponseContract>(_httpActionResult);
            }

            [Test]
            public void TheGetLatestNewsAsyncIsCalledOnce()
            {
                NewsCommandsMock.Verify(m => m.GetLatestNewsAsync(It.IsAny<CultureInfo>()), Times.Once());
            }

            [Test]
            public void TheGetLatestNewsAsyncIsCalledOnceWithTheCorrectCultureInfoParameter()
            {
                NewsCommandsMock.Verify(m => m.GetLatestNewsAsync(It.Is<CultureInfo>(x => x.Name == "en")), Times.Once());
            }

            [Test]
            public void ThenLatestNewsResponseContractIsNotNull()
            {
                Assert.IsNotNull(_latestNewsResponseContract);
            }

            [Test]
            public void ThenTheCorrectNumberOfNewsItemsAreReturned()
            {
                const int expected = 4;

                Assert.AreEqual(expected, _latestNewsResponseContract.LatestNewsItems.Count);
            }
        }

        public class WhenProvidingTheTurkishCultureInTheRequestHeader : GivenGettingTheLatestNews
        {
            protected override async void When()
            {
                Culture = "tr";

                PrepareSut();

                _httpActionResult = await SUT.Get();

                _latestNewsResponseContract = await DeserializationHelper.GetDeserializedResponseContent<LatestNewsResponseContract>(_httpActionResult);
            }

            [Test]
            public void TheGetLatestNewsAsyncIsCalledOnce()
            {
                NewsCommandsMock.Verify(m => m.GetLatestNewsAsync(It.IsAny<CultureInfo>()), Times.Once());
            }

            [Test]
            public void TheGetLatestNewsAsyncIsCalledOnceWithTheCorrectCultureInfoParameter()
            {
                NewsCommandsMock.Verify(m => m.GetLatestNewsAsync(It.Is<CultureInfo>(x => x.Name == "tr")), Times.Once());
            }

            [Test]
            public void ThenLatestNewsResponseContractIsNotNull()
            {
                Assert.IsNotNull(_latestNewsResponseContract);
            }

            [Test]
            public void ThenTheCorrectNumberOfNewsItemsAreReturned()
            {
                const int expected = 4;

                Assert.AreEqual(expected, _latestNewsResponseContract.LatestNewsItems.Count);
            }
        }

    }
}
