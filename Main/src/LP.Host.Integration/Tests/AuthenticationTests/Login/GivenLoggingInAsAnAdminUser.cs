using System.Net;
using System.Net.Mime;
using LP.Api.Shared.Mime;
using LP.Host.Integration.Providers;
using LP.ServiceHost.DataContracts.Request.Authentication;
using LP.ServiceHost.DataContracts.Response.Authentication;
using NUnit.Framework;

namespace LP.Host.Integration.Tests.AuthenticationTests.Login
{
    public class GivenLoggingInAsAnAdminUser : BaseGiven
    {
        private LoginRequestContract _loginRequestContract;
        private LoginResponseContract _loginResponseContract;

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenLoggingInWithTheWrongPasswordForAdmin : GivenLoggingInAsAnAdminUser
        {
            protected override void When()
            {
                Uri = UriProvider.Authentication.Login;

                _loginRequestContract = new LoginRequestContract
                {
                    UserName = "admin",
                    Password = "wrongPassword!"
                };

                Response = SUT.Post(Uri, _loginRequestContract, new ContentType { MediaType = MediaTypes.Application.Json }, true);

                _loginResponseContract = HttpContentBinding.DeserialiseJson<LoginResponseContract>(Response.Content);

            }

            [Test]
            public void ThenResponseStatusIsUnauthorized()
            {
                Assert.AreEqual(Response.StatusCode, HttpStatusCode.Unauthorized);
            }

            [Test]
            public void ThenResponseContractIsNull()
            {
                Assert.IsNull(_loginResponseContract);
            }
        }

        public class WhenLoggingInWithTheCorrectPasswordForAdmin : GivenLoggingInAsAnAdminUser
        {
            protected override void When()
            {
                Uri = "api/authentication/login";

                _loginRequestContract = new LoginRequestContract
                {
                    UserName = "admin",
                    Password = "123Hello"
                };

                Response = SUT.Post(Uri, _loginRequestContract, new ContentType { MediaType = MediaTypes.Application.Json });

                _loginResponseContract = HttpContentBinding.DeserialiseJson<LoginResponseContract>(Response.Content);

            }

            [Test]
            public void ThenResponseStatusIsOk()
            {
                Assert.AreEqual(Response.StatusCode, HttpStatusCode.OK);
            }

            [Test]
            public void ThenResponseContractIsNotNull()
            {
                Assert.IsNotNull(_loginResponseContract);
            }

            [Test]
            public void ThenExpiryTimeOfTokenIsCorrect()
            {
                const int expected = 86399;

                Assert.AreEqual(expected, _loginResponseContract.TokenExpiry);
            }

            [Test]
            public void ThenTheAccessTokenIsNotNullOrEmpty()
            {
                Assert.False(string.IsNullOrEmpty(_loginResponseContract.AccessToken));
            }

            [Test]
            public void ThenTheAccessTokenStartsWithTheCorrectCharacters()
            {
                Assert.True(_loginResponseContract.AccessToken.StartsWith("ey"));
            }

            [Test]
            public void ThenTheAccessTokenIsAtLeast400Characters()
            {
                const int minimumExpectedLength = 400;

                Assert.GreaterOrEqual(_loginResponseContract.AccessToken.Length, minimumExpectedLength);
            }
        }

    }
}
