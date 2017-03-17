using System.Net;
using System.Net.Mime;
using LP.Api.Shared.Mime;
using LP.EntityModels;
using LP.Host.Integration.Behaviours.Authentication.UserInformation;
using LP.Host.Integration.Providers;
using LP.ServiceHost.DataContracts.Response.Authentication;
using NUnit.Framework;

namespace LP.Host.Integration.Tests.AuthenticationTests.UserInformation
{
    [TestFixture]
    public class GivenGettingInformationOfANewlyCreatedUser : BaseGiven
    {
        private UserInformationResponseContract _userInformationResponseContract;

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenTheUserIsCreated : GivenGettingInformationOfANewlyCreatedUser, INeedANewlyCreatedUser
        {
            public User User { get; set; }
            
            public string UserName {
                get
                {
                    return "userinfotest@asandk.com";
                }
            }
            
            public string Password {
                get
                {
                    return "123Hello";
                }
            }
            
            public int[] RolesIds {
                get
                {
                    return new []{1,2,3};
                } 
            }

            protected override void When()
            {
                Uri = UriProvider.Authentication.UserInformation;

                var header = CreateCustomAuthorizationHeader(UserName, Password);

                Response =  SUT.Get(Uri, new ContentType { MediaType = MediaTypes.Application.Json }, header, true);

                _userInformationResponseContract = HttpContentBinding.DeserialiseJson<UserInformationResponseContract>(Response.Content);
            }

            [Test]
            public void ThenResponseShouldNotBeNull()
            {
                Assert.IsNotNull(Response);
            }

            [Test]
            public void ThenHttpStatusShouldBeOk()
            {
                Assert.AreEqual(HttpStatusCode.OK, Response.StatusCode);
            }

            [Test]
            public void ThenUserInformationResponseContractShouldNotBeNull()
            {
                Assert.IsNotNull(_userInformationResponseContract);
            }

            [Test]
            public void ThenUserDisplayNameIsCorrect()
            {
                Assert.AreEqual("userinfotest@asandk.com Display", _userInformationResponseContract.DisplayName);
            }

            [Test]
            public void ThenFieldOfEmploymentIsCorrect()
            {
                Assert.AreEqual("Other", _userInformationResponseContract.FieldOfEmployment);
            }
        }
    }
}
