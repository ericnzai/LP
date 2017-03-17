using System.Collections.Generic;
using System.Web.Http;
using LP.Api.Shared.Binding;
using LP.ServiceHost.DataContracts.Common.Translation;
using LP.ServiceHost.DataContracts.Response.Content;
using Moq;
using NUnit.Framework;

namespace LP.Content.Tests.GlossaryControllerTests.GlossaryPDFControllerTests
{
    public class GivenARequestsMadeToGetWholePdfContent : BaseGiven
    {
        private IHttpActionResult _httpActionResult;
        private GlossaryPDFResponseContract _glossaryPdfResponseContract;
        protected Mock<IHttpContentBinding> HttpContentBindingMock = new Mock<IHttpContentBinding>();

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenCultureIsEnglishAndHasTranslations : GivenARequestsMadeToGetWholePdfContent
        {
            protected override async void When()
            {
                _httpActionResult = await SUT.Post(GlossaryPdfRequestContract);

                _glossaryPdfResponseContract = await LP.Api.Shared.Tests.TestHelpers.DeserializationHelper.GetDeserializedResponseContent<GlossaryPDFResponseContract>(_httpActionResult);

            }

            [Test]
            public void ThenHttpActionResultIsNotNull()
            {
                Assert.IsNotNull(_httpActionResult);
            }

            [Test]
            public void ThenGetWholeGlossaryPdfContentIsCalledOnce()
            {
                GlossaryPdfCommandsMock.Verify(m => m.GetWholeGlossaryPdfContent(It.IsAny<string>(), It.IsAny<List<TranslatedItem>>()));
            }

            [Test]
            public void ThenAGlossaryPdfResponseContractIsReturned()
            {
                Assert.IsNotNull(_glossaryPdfResponseContract);
            }
        }
    }
}
