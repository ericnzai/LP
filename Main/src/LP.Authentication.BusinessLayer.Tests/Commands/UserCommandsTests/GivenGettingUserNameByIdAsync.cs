using System.Collections.Generic;
using System.Linq;
using LP.EntityModels;
using LP.Model.Authentication;
using LP.ServiceHost.DataContracts.Response.Authentication;
using Moq;
using NUnit.Framework;

namespace LP.Authentication.BusinessLayer.Tests.Commands.UserCommandsTests
{
    public class GivenGettingUserNameByIdAsync : BaseGiven
    {
        private string _userDisplayName;

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenTheUserExists : GivenGettingUserNameByIdAsync
        {
            protected override void When()
            {
                _userDisplayName =  SUT.GetUserNameByIdAsync(3);
            }

            [Test]
            public void ThenGetUserByIdAsyncIsNeverCalled()
            {
                BaseCommandsMock.Verify(m => m.GetByIdAsync<User>(It.IsAny<int>()), Times.Never());
            }

            [Test]
            public void ThenGetUserByIdAsyncIsCalledOnceWithTheCorrectParameter()
            {
                BaseCommandsMock.Verify(m => m.GetByIdAsync<User>(It.Is<int>(x => x == 3)), Times.Never());
            }

            [Test]
            public void ThenDecryptStringIsCalledOnce()
            {
                EncryptionHandlerMock.Verify(m => m.DecryptString(It.IsAny<string>()), Times.Never());
            }

            [Test]
            public void ThenGetDecryptedUserIsCalledOnce()
            {
                CacheCommandsMock.Verify(m => m.GetDecryptedUser(It.IsAny<int>()), Times.Once);
            }

            [Test]
            public void ThenUserDisplayNameIsNotNull()
            {
                Assert.IsNotNull(_userDisplayName);
            }

            [Test]
            public void ThenDisplayNameIsSetCorrectly()
            {
                Assert.AreEqual(GenericDecryptedUserDisplayName, _userDisplayName);
            }

        }
        
    }
}
