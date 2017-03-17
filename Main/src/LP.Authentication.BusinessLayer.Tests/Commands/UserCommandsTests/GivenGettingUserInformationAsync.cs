using System.Collections.Generic;
using System.Linq;
using LP.EntityModels;
using LP.Model.Authentication;
using LP.ServiceHost.DataContracts.Response.Authentication;
using Moq;
using NUnit.Framework;

namespace LP.Authentication.BusinessLayer.Tests.Commands.UserCommandsTests
{
    public class GivenGettingUserInformationAsync : BaseGiven
    {
        private UserInformationResponseContract _userInformationResponseContract;

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenTheUserExistsAndHasACountryThatIsNotNull : GivenGettingUserInformationAsync
        {
            protected override async void When()
            {
                _userInformationResponseContract = await SUT.GetUserInformationAsync(new UserDetails{UserId = 3, UserName = ExisingUserName});
            }

            [Test]
            public void ThenGetUserByIdAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetByIdAsync<User>(It.IsAny<int>()), Times.Once());
            }

            [Test]
            public void ThenGetUserByIdAsyncIsCalledOnceWithTheCorrectParameter()
            {
                BaseCommandsMock.Verify(m => m.GetByIdAsync<User>(It.Is<int>(x => x == 3)), Times.Once());
            }

            [Test]
            public void ThenDecryptStringIsCalledOnce()
            {
                EncryptionHandlerMock.Verify(m => m.DecryptString(It.IsAny<string>()), Times.Once());
            }

            [Test]
            public void ThenGetCountryByIdAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetByIdAsync<Country>(It.IsAny<int>()), Times.Once());
            }

            [Test]
            public void ThenRoleCommandsGetFieldOfEmploymentIsCalledOnce()
            {
                RoleCommandsMock.Verify(m => m.GetFieldOfEmployment(It.IsAny<ICollection<int>>()), Times.Once());
            }

            [Test]
            public void ThenUserInformationResponseContractIsNotNull()
            {
                Assert.IsNotNull(_userInformationResponseContract);
            }

            [Test]
            public void ThenUserNameIsCorrect()
            {
                Assert.AreEqual(ExisingUserName, _userInformationResponseContract.UserName);
            }

            [Test]
            public void ThenUserIdIsCorrect()
            {
                const int expected = 3;

                Assert.AreEqual(expected, _userInformationResponseContract.UserId);
            }

            [Test]
            public void ThenDisplayNameIsSetCorrectly()
            {
                Assert.AreEqual(GenericDecryptedUserDisplayName, _userInformationResponseContract.DisplayName);
            }

            [Test]
            public void ThenFieldOfEmploymentIsSetCorrectly()
            {
                const string expected = "MSL";

                Assert.AreEqual(expected, _userInformationResponseContract.FieldOfEmployment);
            }

            [Test]
            public void ThenUserCountryIsSetCorrectly()
            {
                const string expected = "England";

                Assert.AreEqual(expected, _userInformationResponseContract.UserCountry);
            }
        }

        public class WhenTheUserExistsAndHasACountryThatIsNull : GivenGettingUserInformationAsync
        {
            protected override async void When()
            {
                Users.First(x => x.UserID == 3).CountryID = null;

                PrepareSut();

                _userInformationResponseContract = await SUT.GetUserInformationAsync(new UserDetails { UserId = 3, UserName = ExisingUserName });
            }

            [Test]
            public void ThenGetUserByIdAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetByIdAsync<User>(It.IsAny<int>()), Times.Once());
            }

            [Test]
            public void ThenGetUserByIdAsyncIsCalledOnceWithTheCorrectParameter()
            {
                BaseCommandsMock.Verify(m => m.GetByIdAsync<User>(It.Is<int>(x => x == 3)), Times.Once());
            }

            [Test]
            public void ThenDecryptStringIsCalledOnce()
            {
                EncryptionHandlerMock.Verify(m => m.DecryptString(It.IsAny<string>()), Times.Once());
            }

            [Test]
            public void ThenGetCountryByIdAsyncIsNeverCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetByIdAsync<Country>(It.IsAny<int>()), Times.Never());
            }

            [Test]
            public void ThenRoleCommandsGetFieldOfEmploymentIsCalledOnce()
            {
                RoleCommandsMock.Verify(m => m.GetFieldOfEmployment(It.IsAny<ICollection<int>>()), Times.Once());
            }

            [Test]
            public void ThenUserInformationResponseContractIsNotNull()
            {
                Assert.IsNotNull(_userInformationResponseContract);
            }

            [Test]
            public void ThenUserNameIsCorrect()
            {
                Assert.AreEqual(ExisingUserName, _userInformationResponseContract.UserName);
            }

            [Test]
            public void ThenUserIdIsCorrect()
            {
                const int expected = 3;

                Assert.AreEqual(expected, _userInformationResponseContract.UserId);
            }

            [Test]
            public void ThenDisplayNameIsSetCorrectly()
            {
                Assert.AreEqual(GenericDecryptedUserDisplayName, _userInformationResponseContract.DisplayName);
            }

            [Test]
            public void ThenFieldOfEmploymentIsSetCorrectly()
            {
                const string expected = "MSL";

                Assert.AreEqual(expected, _userInformationResponseContract.FieldOfEmployment);
            }

            [Test]
            public void ThenUserCountryIsSetCorrectly()
            {
                Assert.AreEqual(string.Empty, _userInformationResponseContract.UserCountry);
            }
        }
    }
}
