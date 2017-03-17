using System.Net;
using System.Net.Mime;
using LP.Api.Shared.Mime;
using LP.Host.Integration.Providers;
using LP.ServiceHost.DataContracts.Request.Authentication;
using LP.ServiceHost.DataContracts.Response.Authentication;
using NUnit.Framework;

namespace LP.Host.Integration.Tests.AuthenticationTests.Login
{
    public class GivenLogging : BaseGiven
    {
        private LoginRequestContract _loginRequestContract;
        private LoginResponseContract _loginResponseContract;

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenPostingAnEmptyRequestContract : GivenLogging
        {
            protected override void When()
            {
                Uri = UriProvider.Authentication.Login;

                _loginRequestContract = new LoginRequestContract();

                Response = SUT.Post(Uri, _loginRequestContract, new ContentType { MediaType = MediaTypes.Application.Json }, true);

                _loginResponseContract = HttpContentBinding.DeserialiseJson<LoginResponseContract>(Response.Content);
            }

            [Test]
            public void ThenResponseStatusIsNotFound()
            {
                Assert.AreEqual(Response.StatusCode, HttpStatusCode.NotFound);
            }

            [Test]
            public void ThenResponseContractIsNull()
            {
                Assert.IsNull(_loginResponseContract);
            }
        }
    }
}
