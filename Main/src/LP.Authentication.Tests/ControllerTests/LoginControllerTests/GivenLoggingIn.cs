using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Web.Http;
using System.Web.Http.Results;
using LP.Model.Authentication;
using LP.ServiceHost.DataContracts.Enums;
using LP.ServiceHost.DataContracts.Request.Authentication;
using LP.ServiceHost.DataContracts.Response.Authentication;
using Moq;
using NUnit.Framework;

namespace LP.Authentication.Tests.ControllerTests.LoginControllerTests
{
    public class GivenLoggingIn : BaseGiven
    {
        private IHttpActionResult _httpActionResult;
  
        private LoginResponseContract _loginResponseContract;

        private const string UserName = "TestUserName";
        private const string Password = "TestPassword";

        private readonly LoginRequestContract _loginRequestContract = new LoginRequestContract
        {
            Password = Password,
            UserName = UserName
        };

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenCallingLoginWithAUserThatExistsAndShouldBeAuthorised : GivenLoggingIn
        {
            protected override async void When()
            {
                HttpResponseStatus = HttpResponseStatus.Success;

                PrepareSut();

                _httpActionResult = await SUT.Post(_loginRequestContract);

                _loginResponseContract = await LP.Api.Shared.Tests.TestHelpers.DeserializationHelper.GetDeserializedResponseContent<LoginResponseContract>(_httpActionResult);
            }

            [Test]
            public void ThenHttpActionResultIsNotNull()
            {
                Assert.IsNotNull(_httpActionResult);
            }

            [Test]
            public void ThenAuthenticateUserIsCalledOnce()
            {
                UserCommandsMock.Verify(m => m.AuthenticateUserAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
            }

            [Test]
            public void ThenAuthenticateUserIsCalledOnceWithTheCorrectParameters()
            {
                UserCommandsMock.Verify(m => m.AuthenticateUserAsync(It.Is<string>(x => x == UserName), It.Is<string>(x => x == Password)), Times.Once());
            }

            [Test]
            public void ThenExecutePostAsyncForAccessTokenIsCalledOnce()
            {
                RequestExecutorMock.Verify(m => m.ExecutePostAsync(It.IsAny<string>(), It.IsAny<List<KeyValuePair<string, string>>>(), It.IsAny<ContentType>(), It.IsAny<bool>()), Times.Once());
            }

            [Test]
            public void ThenDeserialiseJsonAccessTokenModelIsCalledOnce()
            {
                HttpContentBindingMock.Verify(m => m.DeserialiseJson<AccessTokenModel>(It.IsAny<HttpContent>()), Times.Once());
            }

            [Test]
            public void ThenALoginResponseContractIsReturned()
            {
                Assert.IsNotNull(_loginResponseContract);
            }

            [Test]
            public void ThenASuccessStatusIsReturned()
            {
                Assert.AreEqual(HttpResponseStatus.Success, _loginResponseContract.HttpResponseStatus);
            }
        }

        public class WhenCallingLoginWithAUserThatExistsButShouldNotBeAuthorised : GivenLoggingIn
        {
            protected override async void When()
            {
                HttpResponseStatus = HttpResponseStatus.Unauthorised;

                PrepareSut();

                _httpActionResult = await SUT.Post(_loginRequestContract);
            }

            [Test]
            public void ThenHttpActionResultIsNotNull()
            {
                Assert.IsNotNull(_httpActionResult);
            }

            [Test]
            public void ThenExecutePostAsyncForAccessTokenIsNeverCalled()
            {
                RequestExecutorMock.Verify(m => m.ExecutePostAsync(It.IsAny<string>(), It.IsAny<List<KeyValuePair<string, string>>>(), It.IsAny<ContentType>(), It.IsAny<bool>()), Times.Never());
            }

            [Test]
            public void ThenDeserialiseJsonAccessTokenModelIsNeverCalled()
            {
                HttpContentBindingMock.Verify(m => m.DeserialiseJson<AccessTokenModel>(It.IsAny<HttpContent>()), Times.Never());
            }

            [Test]
            public void ThenAnUnauthorizedResultIsReturned()
            {
                Assert.IsNotNull(_httpActionResult as UnauthorizedResult);
            }
        }

        public class WhenCallingLoginWithAUserThatDoesNotExist : GivenLoggingIn
        {
            protected override async void When()
            {
                HttpResponseStatus = HttpResponseStatus.NotFound;

                PrepareSut();

                _httpActionResult = await SUT.Post(_loginRequestContract);
            }

            [Test]
            public void ThenHttpActionResultIsNotNull()
            {
                Assert.IsNotNull(_httpActionResult);
            }

            [Test]
            public void ThenExecutePostAsyncForAccessTokenIsNeverCalled()
            {
                RequestExecutorMock.Verify(m => m.ExecutePostAsync(It.IsAny<string>(), It.IsAny<List<KeyValuePair<string, string>>>(), It.IsAny<ContentType>(), It.IsAny<bool>()), Times.Never());
            }

            [Test]
            public void ThenDeserialiseJsonAccessTokenModelIsNeverCalled()
            {
                HttpContentBindingMock.Verify(m => m.DeserialiseJson<AccessTokenModel>(It.IsAny<HttpContent>()), Times.Never());
            }

            [Test]
            public void ThenANotFoundResultIsReturned()
            {
                Assert.IsNotNull(_httpActionResult as NotFoundResult);
            }
        }

        public class WhenLoggingInWithAnEmptyUserName : GivenLoggingIn
        {
            protected override async void When()
            {
                _loginRequestContract.UserName = string.Empty;

                _httpActionResult = await SUT.Post(_loginRequestContract);
            }

            [Test]
            public void ThenHttpActionResultIsNotNull()
            {
                Assert.IsNotNull(_httpActionResult);
            }

            [Test]
            public void ThenExecutePostAsyncForAccessTokenIsNeverCalled()
            {
                RequestExecutorMock.Verify(m => m.ExecutePostAsync(It.IsAny<string>(), It.IsAny<List<KeyValuePair<string, string>>>(), It.IsAny<ContentType>(), It.IsAny<bool>()), Times.Never());
            }

            [Test]
            public void ThenDeserialiseJsonAccessTokenModelIsNeverCalled()
            {
                HttpContentBindingMock.Verify(m => m.DeserialiseJson<AccessTokenModel>(It.IsAny<HttpContent>()), Times.Never());
            }

            [Test]
            public void ThenANotFoundResultIsReturned()
            {
                Assert.IsNotNull(_httpActionResult as NotFoundResult);
            }
        }

        public class WhenLoggingInWithANullUserName : GivenLoggingIn
        {
            protected override async void When()
            {
                _loginRequestContract.UserName = null;

                _httpActionResult = await SUT.Post(_loginRequestContract);
            }

            [Test]
            public void ThenHttpActionResultIsNotNull()
            {
                Assert.IsNotNull(_httpActionResult);
            }

            [Test]
            public void ThenExecutePostAsyncForAccessTokenIsNeverCalled()
            {
                RequestExecutorMock.Verify(m => m.ExecutePostAsync(It.IsAny<string>(), It.IsAny<List<KeyValuePair<string, string>>>(), It.IsAny<ContentType>(), It.IsAny<bool>()), Times.Never());
            }

            [Test]
            public void ThenDeserialiseJsonAccessTokenModelIsNeverCalled()
            {
                HttpContentBindingMock.Verify(m => m.DeserialiseJson<AccessTokenModel>(It.IsAny<HttpContent>()), Times.Never());
            }

            [Test]
            public void ThenANotFoundResultIsReturned()
            {
                Assert.IsNotNull(_httpActionResult as NotFoundResult);
            }
        }

        public class WhenLoggingInWithAnEmptyPassword : GivenLoggingIn
        {
            protected override async void When()
            {
                _loginRequestContract.Password = string.Empty;

                _httpActionResult = await SUT.Post(_loginRequestContract);
            }

            [Test]
            public void ThenHttpActionResultIsNotNull()
            {
                Assert.IsNotNull(_httpActionResult);
            }

            [Test]
            public void ThenExecutePostAsyncForAccessTokenIsNeverCalled()
            {
                RequestExecutorMock.Verify(m => m.ExecutePostAsync(It.IsAny<string>(), It.IsAny<List<KeyValuePair<string, string>>>(), It.IsAny<ContentType>(), It.IsAny<bool>()), Times.Never());
            }

            [Test]
            public void ThenDeserialiseJsonAccessTokenModelIsNeverCalled()
            {
                HttpContentBindingMock.Verify(m => m.DeserialiseJson<AccessTokenModel>(It.IsAny<HttpContent>()), Times.Never());
            }

            [Test]
            public void ThenANotFoundResultIsReturned()
            {
                Assert.IsNotNull(_httpActionResult as NotFoundResult);
            }
        }

        public class WhenLoggingInWithANullPassword : GivenLoggingIn
        {
            protected override async void When()
            {
                _loginRequestContract.Password = null;

                _httpActionResult = await SUT.Post(_loginRequestContract);
            }

            [Test]
            public void ThenHttpActionResultIsNotNull()
            {
                Assert.IsNotNull(_httpActionResult);
            }

            [Test]
            public void ThenExecutePostAsyncForAccessTokenIsNeverCalled()
            {
                RequestExecutorMock.Verify(m => m.ExecutePostAsync(It.IsAny<string>(), It.IsAny<List<KeyValuePair<string, string>>>(), It.IsAny<ContentType>(), It.IsAny<bool>()), Times.Never());
            }

            [Test]
            public void ThenDeserialiseJsonAccessTokenModelIsNeverCalled()
            {
                HttpContentBindingMock.Verify(m => m.DeserialiseJson<AccessTokenModel>(It.IsAny<HttpContent>()), Times.Never());
            }

            [Test]
            public void ThenANotFoundResultIsReturned()
            {
                Assert.IsNotNull(_httpActionResult as NotFoundResult);
            }
        }

        public class WhenLoggingInWithAnEmptyUserNameAndPassword : GivenLoggingIn
        {
            protected override async void When()
            {
                _loginRequestContract.Password = string.Empty;
                _loginRequestContract.UserName = string.Empty;
                _httpActionResult = await SUT.Post(_loginRequestContract);
            }

            [Test]
            public void ThenHttpActionResultIsNotNull()
            {
                Assert.IsNotNull(_httpActionResult);
            }

            [Test]
            public void ThenExecutePostAsyncForAccessTokenIsNeverCalled()
            {
                RequestExecutorMock.Verify(m => m.ExecutePostAsync(It.IsAny<string>(), It.IsAny<List<KeyValuePair<string, string>>>(), It.IsAny<ContentType>(), It.IsAny<bool>()), Times.Never());
            }

            [Test]
            public void ThenDeserialiseJsonAccessTokenModelIsNeverCalled()
            {
                HttpContentBindingMock.Verify(m => m.DeserialiseJson<AccessTokenModel>(It.IsAny<HttpContent>()), Times.Never());
            }

            [Test]
            public void ThenANotFoundResultIsReturned()
            {
                Assert.IsNotNull(_httpActionResult as NotFoundResult);
            }
        }

        public class WhenLoggingInWithANullUserNameAndPassword : GivenLoggingIn
        {
            protected override async void When()
            {
                _loginRequestContract.Password = null;
                _loginRequestContract.UserName = null;
                _httpActionResult = await SUT.Post(_loginRequestContract);
            }

            [Test]
            public void ThenHttpActionResultIsNotNull()
            {
                Assert.IsNotNull(_httpActionResult);
            }

            [Test]
            public void ThenExecutePostAsyncForAccessTokenIsNeverCalled()
            {
                RequestExecutorMock.Verify(m => m.ExecutePostAsync(It.IsAny<string>(), It.IsAny<List<KeyValuePair<string, string>>>(), It.IsAny<ContentType>(), It.IsAny<bool>()), Times.Never());
            }

            [Test]
            public void ThenDeserialiseJsonAccessTokenModelIsNeverCalled()
            {
                HttpContentBindingMock.Verify(m => m.DeserialiseJson<AccessTokenModel>(It.IsAny<HttpContent>()), Times.Never());
            }

            [Test]
            public void ThenANotFoundResultIsReturned()
            {
                Assert.IsNotNull(_httpActionResult as NotFoundResult);
            }
        }
    }
}
