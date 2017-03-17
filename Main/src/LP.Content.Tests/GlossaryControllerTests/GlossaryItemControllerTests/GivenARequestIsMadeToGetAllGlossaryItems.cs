using System.Web.Http;
using LP.Api.Shared.Tests.TestHelpers;
using LP.ServiceHost.DataContracts.Response.Content;
using NUnit.Framework;

namespace LP.Content.Tests.GlossaryControllerTests.GlossaryItemControllerTests
{
    public class GivenARequestIsMadeToGetAllGlossaryItems : BaseGiven
    {
        private IHttpActionResult _response;
        private GlossaryItemsResponseContract _glossaryItemsResponseContract;
        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenTheCultureIsEnglish : GivenARequestIsMadeToGetAllGlossaryItems
        {
            protected override async void When()
            {
                _response = await SUT.Get();

                _glossaryItemsResponseContract =
                    await DeserializationHelper.GetDeserializedResponseContent<GlossaryItemsResponseContract>(_response);
            }

            [Test]
            public void ThenResponseIsNotNull()
            {
                Assert.IsNotNull(_response);
            }
        }
    }
}
