using System.Collections.Generic;
using System.Linq;
using LP.EntityModels;
using LP.Model.Authentication;
using LP.ServiceHost.DataContracts.Response.Authentication;
using Moq;
using NUnit.Framework;

namespace LP.Authentication.BusinessLayer.Tests.Commands.UserCommandsTests
{
    public class GivenGettingListOfUsersByTrainerId : BaseGiven
    {
        private List<DecryptedUser> _users;

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenCorrectRequestIsMade : GivenGettingListOfUsersByTrainerId
        {
        private readonly List<int> _listWithExistingUsers = new List<int> {1, 2, 3};
            protected override void When()
            {
                _users = SUT.GetDecryptedUsers(_listWithExistingUsers);
            }

            [Test]
            public void ThenGetAllAsyncUsersIsNeverCalled()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<User>(), Times.Never());
            }

            [Test]
            public void ThenDecryptStringNeverCalled()
            {
                EncryptionHandlerMock.Verify(m => m.DecryptString(It.IsAny<string>()), Times.Never());
            }

            [Test]
            public void ThenGetDecryptedUserIsCalledExpectedNumberOfTimes()
            {
                const int expected = 3;
                CacheCommandsMock.Verify(m => m.GetDecryptedUser(It.IsAny<int>()), Times.Exactly(expected));
            }

            [Test]
            public void ThenExpectedNumberIsReturned()
            {
                const int expected = 3;
                Assert.AreEqual(expected, _users.Count());
            }
        }

        public class WhenRequestIsMadeWithEmptyParameter : GivenGettingListOfUsersByTrainerId
        {
            protected override void When()
            {
                _users = SUT.GetDecryptedUsers(new List<int>());
            }

            [Test]
            public void ThenGetAllAsyncUsersIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<User>(), Times.Never);
            }

            [Test]
            public void ThenDecryptStringIsCalledExpectedNumberOfTimes()
            {
                EncryptionHandlerMock.Verify(m => m.DecryptString(It.IsAny<string>()), Times.Never);
            }

            [Test]
            public void ThenGetDecryptedUserIsNeverCalled()
            {
                CacheCommandsMock.Verify(m => m.GetDecryptedUser(It.IsAny<int>()), Times.Never);
            }

            [Test]
            public void ThenEmptyListISReturned()
            {
                Assert.IsFalse(_users.Any());
            }
        }
    }
}
