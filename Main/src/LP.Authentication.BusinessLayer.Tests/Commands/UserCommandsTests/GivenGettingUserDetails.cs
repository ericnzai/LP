using System.Collections.Generic;
using LP.EntityModels;
using LP.Model.Authentication;
using Moq;
using NUnit.Framework;

namespace LP.Authentication.BusinessLayer.Tests.Commands.UserCommandsTests
{
    public class GivenGettingUserDetails : BaseGiven
    {
        private UserDetails _userDetails;

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenGettingAUserThatExists : GivenGettingUserDetails
        {
            protected async override void When()
            {
                _userDetails = await SUT.GetUserDetailsAsync(ExisingUserName);
            }

            [Test]
            public void ThenGetAllUserAsyncIsNeverCalled()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<User>(), Times.Never());
            }

            [Test]
            public void ThenCacheCommandsGetDecryptedUserIsCalledOnce()
            {
                CacheCommandsMock.Verify(m => m.GetDecryptedUser(It.IsAny<string>()), Times.Once());
            }

            [Test]
            public void ThenCacheCommandsGetDecryptedUserIsCalledOnceWithTheCorrectParameters()
            {
                CacheCommandsMock.Verify(m => m.GetDecryptedUser(It.Is<string>(x => x == ExisingUserName)), Times.Once());
            }

            [Test]
            public void ThenGetRoleForUserIsCalledOnce()
            {
                UserRoleCommandsMock.Verify(m => m.GetRolesForUserAsync(It.IsAny<int>()), Times.Once());
            }

            [Test]
            public void ThenUserDetailsIsNotNull()
            {
                Assert.IsNotNull(_userDetails);
            }

            [Test]
            public void ThenUserNameIsSetCorrectly()
            {
                Assert.AreEqual(ExisingUserName, _userDetails.UserName);
            }

            [Test]
            public void ThenUserIdIsCorrect()
            {
                const int expected = 3;

                Assert.AreEqual(expected, _userDetails.UserId);
            }

            [Test]
            public void ThenTheUserHasTheCorrectAmountOfRoles()
            {
                const int expected = 6;
                Assert.AreEqual(expected, _userDetails.RoleIds.Count);
            }

            [Test]
            public void ThenTheUserHasTheCorrectRolesSet()
            {
                var expected = new List<int> {4, 6, 8, 9, 2, 1};

               CollectionAssert.AreEquivalent(expected, _userDetails.RoleIds);
            }

            [Test]
            public void ThenTheUserHasRoleIdsStringSetCorrectly()
            {
                const string expected = "4,6,8,9,2,1";

                Assert.AreEqual(expected, _userDetails.RoleIdsString);
            }

            [Test]
            public void ThenIsAdminUserIsCalledOnce()
            {
                UserRoleCommandsMock.Verify(m => m.IsUserAdmin(It.IsAny<IEnumerable<UserRole>>()), Times.Once());
            }

            [Test]
            public void ThenIsAdminIsFalse()
            {
                Assert.False(_userDetails.IsAdmin);
            }

            [Test]
            public void ThenIsTranslatorUserIsCalledOnce()
            {
                UserRoleCommandsMock.Verify(m => m.IsUserTranslator(It.IsAny<IEnumerable<UserRole>>()), Times.Once());
            }

            [Test]
            public void ThenIsTranslatorIsFalse()
            {
                Assert.False(_userDetails.IsTranslator);
            }
        }

        public class WhenTheCurrentUserIsAnAdminUser : GivenGettingUserDetails
        {
            protected async override void When()
            {
                _userDetails = await SUT.GetUserDetailsAsync(AdminUserName);
            }

            [Test]
            public void ThenGetAllUserAsyncIsNeverCalled()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<User>(), Times.Never());
            }


            [Test]
            public void ThenIsAdminUserIsCalledOnce()
            {
                UserRoleCommandsMock.Verify(m => m.IsUserAdmin(It.IsAny<IEnumerable<UserRole>>()), Times.Once());
            }

            [Test]
            public void ThenIsAdminUserIsTrue()
            {
                Assert.True(_userDetails.IsAdmin);
            }
        }

        public class WhenTheCurrentUserIsATranslator : GivenGettingUserDetails
        {
            protected async override void When()
            {
                _userDetails = await SUT.GetUserDetailsAsync(TranslatorUserName);
            }

            [Test]
            public void ThenGetAllUserAsyncIsNeverCalled()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<User>(), Times.Never());
            }

            [Test]
            public void ThenIsUserTranslatorIsCalledOnce()
            {
                UserRoleCommandsMock.Verify(m => m.IsUserTranslator(It.IsAny<IEnumerable<UserRole>>()), Times.Once());
            }

            [Test]
            public void ThenIsTranslatorIsTrue()
            {
                Assert.True(_userDetails.IsTranslator);
            }
        }

        public class WhenGettingUserInformationAndTheUserIsNotIntTheDecryptedUserCache : GivenGettingUserDetails
        {
            protected async override void When()
            {
                _userDetails = await SUT.GetUserDetailsAsync(ExistingUserButNotDecryptedUserName);
            }

            [Test]
            public void ThenGetAllUserAsyncIsCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<User>(), Times.Once());
            }

            [Test]
            public void TheAddDecryptedUserToCacheIsCalledOnce()
            {
                CacheCommandsMock.Verify(m => m.AddDecryptedUserToCache(It.IsAny<DecryptedUser>()), Times.Once());
            }

            [Test]
            public void TheAddDecryptedUserToCacheIsCalledOnceWithTheCorrectParameters()
            {
                CacheCommandsMock.Verify(m => m.AddDecryptedUserToCache(It.Is<DecryptedUser>(x => x.UserId == 7)), Times.Once());
            }
        }
    }
}
