using System.Web.Http;
using LP.Api.Shared.Tests.TestHelpers;
using LP.ServiceHost.DataContracts.Request.Content;
using LP.ServiceHost.DataContracts.Response.Content;
using NUnit.Framework;

namespace LP.Content.Tests.SearchControllerTests
{
    public class GivenARequestIsMadeToGetAllSearchItems : BaseGiven
    {
        private IHttpActionResult _response;
        private SearchItemsResponseContract _searchItemsResponseContract;
        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenTheCultureIsEnglish : GivenARequestIsMadeToGetAllSearchItems
        {
            protected override async void When()
            {
                _response = await SUT.Post(_searchRequestContract);

                _searchItemsResponseContract =
                    await DeserializationHelper.GetDeserializedResponseContent<SearchItemsResponseContract>(_response);
            }

            //[Test]
            //public void ThenResponseIsNotNull()
            //{
            //    Assert.IsNotNull(_response);
            //}
        }
    }
}
