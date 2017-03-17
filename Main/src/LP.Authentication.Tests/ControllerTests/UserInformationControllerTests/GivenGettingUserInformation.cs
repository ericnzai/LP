using System.Web.Http;
using System.Web.Http.Results;
using LP.Model.Authentication;
using LP.ServiceHost.DataContracts.Response.Authentication;
using Moq;
using NUnit.Framework;

namespace LP.Authentication.Tests.ControllerTests.UserInformationControllerTests
{
    public class GivenGettingUserInformation : BaseGiven
    {
        private IHttpActionResult _httpActionResult;
        private OkNegotiatedContentResult<UserInformationResponseContract> _negotiatedContentResult;
        private const string DisplayName = "CorrectDisplayName";
        private const string FieldOfEmployment = "field";
        private const string UserCountry = "england";
        private const string UserName = "namehere";
        private const int UserId = 190;
        protected override void Given()
        {
            UserInformationResponseContract = new UserInformationResponseContract
            {
                DisplayName = DisplayName,
                FieldOfEmployment = FieldOfEmployment,
                UserCountry = UserCountry,
                UserName = UserName,
                UserId = UserId
            };

            PrepareSut();
        }

        public class WhenMakingACorrectRequest : GivenGettingUserInformation
        {
            protected override async void When()
            {
                _httpActionResult = await SUT.Get();

                _negotiatedContentResult = _httpActionResult as OkNegotiatedContentResult<UserInformationResponseContract>;
            }

            [Test]
            public void ThenGetUserInformationAsyncIsCalledOnce()
            {
                UserCommandsMock.Verify(m => m.GetUserInformationAsync(It.IsAny<UserDetails>()), Times.Once());
            }

            [Test]
            public void ThenNegotiatedContentResultIsNotNull()
            {
                Assert.IsNotNull(_negotiatedContentResult);
            }

            [Test]
            public void ThenResponseContractIsNotNull()
            {
                Assert.IsNotNull(_negotiatedContentResult.Content);
            }

            [Test]
            public void ThenUserNameIsReturnedCorrectly()
            {
                Assert.AreEqual(UserName, _negotiatedContentResult.Content.UserName);
            }

            [Test]
            public void ThenDisplayNameIsReturnedCorrectly()
            {
                Assert.AreEqual(DisplayName, _negotiatedContentResult.Content.DisplayName);
            }

            [Test]
            public void ThenFieldOfEmploymentIsReturnedCorrectly()
            {
                Assert.AreEqual(FieldOfEmployment, _negotiatedContentResult.Content.FieldOfEmployment);
            }

            [Test]
            public void ThenUserIdIsReturnedCorrectly()
            {
                Assert.AreEqual(UserId, _negotiatedContentResult.Content.UserId);
            }

            [Test]
            public void ThenUserCountryIsReturnedCorrectly()
            {
                Assert.AreEqual(UserCountry, _negotiatedContentResult.Content.UserCountry);
            }
        }
    }
}
