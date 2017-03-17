using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using LP.Api.Shared.Mime;
using LP.Host.Integration.Providers;
using NUnit.Framework;

namespace LP.Host.Integration.Tests.AuthenticationTests.UserInformation
{
    public class GivenGettingUserInformation
    {
        public class WhenTheEndpointIsCalledWithNoHeader : GivenGettingInformationOfANewlyCreatedUser
        {
            protected override void When()
            {
                Uri = UriProvider.Authentication.UserInformation;

                Response = SUT.Get(Uri, new ContentType { MediaType = MediaTypes.Application.Json }, new List<KeyValuePair<string, string>>(), true);
            }

            [Test]
            public void ThenUnauthorizedStatusCodeShouldBeReturned()
            {
                Assert.AreEqual(HttpStatusCode.Unauthorized, Response.StatusCode);
            }
        }
    }
}
