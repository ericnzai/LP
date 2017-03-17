using System.Collections.Generic;
using System.Linq;
using LP.EntityModels;
using LP.Model.Authentication;
using Moq;
using NUnit.Framework;

namespace LP.ServiceHost.Common.BusinessLayer.Tests.Commands.CacheCommandsTests
{
    public class GivenGettingDecryptedUsersCache : BaseGiven
    {
        private List<DecryptedUser> _decryptedUsers;

        public class WhenUserNameCacheIsAlreadyPopulated : GivenGettingDecryptedUsersCache
        {
            protected override void When()
            {
                PrepareSut();

                _decryptedUsers = SUT.GetCachedDecryptedUsers();
            }

            [Test]
            public void ThenUserNameCacheContainsTheCorrectAmountOfItems()
            {
                const int expected = 4;

                Assert.AreEqual(expected, _decryptedUsers.Count);
            }

            [Test]
            public void ThenGetAllApplicationUsersIsNotCalled()
            {
                UserBaseCommandsMock.Verify(m => m.GetAll<User>(), Times.Never());
            }

            [Test]
            public void ThenAddToCacheIsNeverCalled()
            {
                DecryptedUserMemoryCacheWrapper.Verify(m => m.Add(It.IsAny<DecryptedUser>()), Times.Never());
            }

            [Test]
            public void ThenFirstUserCacheItemIsCorrect()
            {
                var decryptedUser = _decryptedUsers.First(a => a.UserId == 1);

                Assert.AreEqual("User 1", decryptedUser.DecryptedUserName);
            }

            [Test]
            public void ThenSecondUserCacheItemIsCorrect()
            {
                var decryptedUser = _decryptedUsers.First(a => a.UserId == 2);

                Assert.AreEqual("User 2", decryptedUser.DecryptedUserName);
            }

            [Test]
            public void ThenThirdUserCacheItemIsCorrect()
            {
                var decryptedUser = _decryptedUsers.First(a => a.UserId == 3);

                Assert.AreEqual("User 3", decryptedUser.DecryptedUserName);
            }

            [Test]
            public void ThenFourthUserCacheItemIsCorrect()
            {
                var decryptedUser = _decryptedUsers.First(a => a.UserId == 4);

                Assert.AreEqual("User 4", decryptedUser.DecryptedUserName);
            }
        }
    }
}
