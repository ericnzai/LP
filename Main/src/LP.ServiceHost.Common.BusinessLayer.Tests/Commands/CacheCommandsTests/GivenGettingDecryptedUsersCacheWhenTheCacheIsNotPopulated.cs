using System.Collections.Generic;
using System.Linq;
using LP.EntityModels;
using LP.Model.Authentication;
using Moq;
using NUnit.Framework;

namespace LP.ServiceHost.Common.BusinessLayer.Tests.Commands.CacheCommandsTests
{
    public class GivenGettingDecryptedUsersCacheWhenTheCacheIsNotPopulated : BaseGiven
    {
        private List<DecryptedUser> _decryptedUsers;

        public class WhenUserNameCacheIsNotPopulated : GivenGettingDecryptedUsersCacheWhenTheCacheIsNotPopulated
        {
            protected override void When()
            {
                UserCacheItems = new List<DecryptedUser>();

                Users = new List<User>
                    {
                        new User {UserID = 1, UserName = "Db User 1"},
                        new User {UserID = 2, UserName = "Db User 2"},
                        new User {UserID = 3, UserName = "Db User 3"}
                    };

                PrepareSut();

                _decryptedUsers = SUT.GetCachedDecryptedUsers();
            }

            [Test]
            public void ThenUserCacheItemsIsNotNull()
            {
                Assert.IsNotNull(_decryptedUsers);
            }

            [Test]
            public void ThenGetAllApplicationUsersIsCalledOnce()
            {
                UserBaseCommandsMock.Verify(m => m.GetAll<User>(), Times.Once());
            }

            [Test]
            public void ThenSetCacheIsCalledTheCorrectAmountOfTimes()
            {
                DecryptedUserMemoryCacheWrapper.Verify(m => m.Set(It.IsAny<List<DecryptedUser>>()), Times.Once());
            }

            [Test]
            public void ThenUserNameCacheContainsTheCorrectAmountOfItems()
            {
                const int expected = 3;

                Assert.AreEqual(expected, _decryptedUsers.Count);
            }

            [Test]
            public void ThenFirstUserCacheItemIsCorrect()
            {
                var userCacheItem = _decryptedUsers.First(a => a.UserId == 1);

                Assert.AreEqual("Db User 1", userCacheItem.DecryptedUserName);
            }

            [Test]
            public void ThenSecondUserCacheItemIsCorrect()
            {
                var userCacheItem = _decryptedUsers.First(a => a.UserId == 2);

                Assert.AreEqual("Db User 2", userCacheItem.DecryptedUserName);
            }

            [Test]
            public void ThenThirdUserCacheItemIsCorrect()
            {
                var userCacheItem = _decryptedUsers.First(a => a.UserId == 3);

                Assert.AreEqual("Db User 3", userCacheItem.DecryptedUserName);
            }
        }
    }
}
