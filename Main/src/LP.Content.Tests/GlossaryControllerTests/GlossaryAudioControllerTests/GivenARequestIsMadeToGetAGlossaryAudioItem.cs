using System.Web.Http;
using System.Web.Http.Results;
using LP.Api.Shared.Tests.TestHelpers;
using LP.ServiceHost.DataContracts.Response.Content;
using Moq;
using NUnit.Framework;

namespace LP.Content.Tests.GlossaryControllerTests.GlossaryAudioControllerTests
{
    public class GivenARequestIsMadeToGetAGlossaryAudioItem : BaseGiven
    {
        private IHttpActionResult _httpActionResult;

        private GlossaryAudioResponseContract _glossaryAudioResponseContract;
        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenTheGlossaryAudioItemDoesNotExist : GivenARequestIsMadeToGetAGlossaryAudioItem
        {
            protected override async void When()
            {
                _httpActionResult = await SUT.Get(NonExistantGlossaryAudioId);

                _glossaryAudioResponseContract = await DeserializationHelper.GetDeserializedResponseContent<GlossaryAudioResponseContract>(_httpActionResult);
            }

            [Test]
            public void ThenGetGlossaryAudioIsCalledOnce()
            {
                GlossaryCommandsMock.Verify(m => m.GetGlossaryAudio(It.IsAny<int>()), Times.Once());
            }

            [Test]
            public void ThenGetGlossaryAudioIsCalledOnceWithTheCorrectGlossaryItemId()
            {
                GlossaryCommandsMock.Verify(m => m.GetGlossaryAudio(It.Is<int>(x => x == NonExistantGlossaryAudioId)), Times.Once());
            }

            [Test]
            public void ThenNotFoundStatusCodeIsReturned()
            {
                Assert.IsNotNull(_httpActionResult as NotFoundResult);
            }
        }

        public class WhenTheGlossaryAudioItemExistsButIsNotEnabled : GivenARequestIsMadeToGetAGlossaryAudioItem
        {
            protected override async void When()
            {
                PrepareSut();

                _httpActionResult = await SUT.Get(ExistingDisabledGlossaryAudioId);

                _glossaryAudioResponseContract = await DeserializationHelper.GetDeserializedResponseContent<GlossaryAudioResponseContract>(_httpActionResult);
            }

            [Test]
            public void ThenGetGlossaryAudioIsCalledOnce()
            {
                GlossaryCommandsMock.Verify(m => m.GetGlossaryAudio(It.IsAny<int>()), Times.Once());
            }

            [Test]
            public void ThenGetGlossaryAudioIsCalledOnceWithTheCorrectGlossaryItemId()
            {
                GlossaryCommandsMock.Verify(m => m.GetGlossaryAudio(It.Is<int>(x => x == ExistingDisabledGlossaryAudioId)), Times.Once());
            }

            [Test]
            public void ThenNotFoundStatusCodeIsReturned()
            {
                Assert.IsNotNull(_httpActionResult as NotFoundResult);
            }
        }

        public class WhenTheGlossaryAudioItemExists : GivenARequestIsMadeToGetAGlossaryAudioItem
        {
            protected override async void When()
            {
                _httpActionResult = await SUT.Get(ExistingEnabledGlossaryAudioId);

                _glossaryAudioResponseContract = await DeserializationHelper.GetDeserializedResponseContent<GlossaryAudioResponseContract>(_httpActionResult);
            }

            [Test]
            public void ThenGetGlossaryAudioIsCalledOnce()
            {
                GlossaryCommandsMock.Verify(m => m.GetGlossaryAudio(It.IsAny<int>()), Times.Once());
            }

            [Test]
            public void ThenGetGlossaryAudioIsCalledOnceWithTheCorrectGlossaryItemId()
            {
                GlossaryCommandsMock.Verify(m => m.GetGlossaryAudio(It.Is<int>(x => x == ExistingEnabledGlossaryAudioId)), Times.Once());
            }

            [Test]
            public void ThenGlossaryAudioResponseContractIsNotNull()
            {
                Assert.IsNotNull(_glossaryAudioResponseContract);
            }

            [Test]
            public void ThenFileNameIsSetCorrectly()
            {
                const string expected = "ExistingEnabledGlossary.mp3";

                Assert.AreEqual(expected, _glossaryAudioResponseContract.FileName);
            }

            [Test]
            public void ThenAudioBytesAreReturnedCorrectly()
            {
                const string expected = "ThisIsTheExistingEnabledGlossary";

                Assert.AreEqual(expected, _glossaryAudioResponseContract.AudioBase64);
            }
        }
    }
}
