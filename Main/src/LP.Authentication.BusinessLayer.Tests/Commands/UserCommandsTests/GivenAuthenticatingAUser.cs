using LP.EntityModels;
using LP.Model.Authentication;
using LP.ServiceHost.DataContracts.Enums;
using Moq;
using NUnit.Framework;

namespace LP.Authentication.BusinessLayer.Tests.Commands.UserCommandsTests
{
    public class GivenAuthenticatingAUser : BaseGiven
    {
        private HttpResponseStatus _httpResponseStatus;

        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenTheDecryptedUserDoesNotExist : GivenAuthenticatingAUser
        {
            private const string UserName = "test@example.com";

            protected async override void When()
            {
                _httpResponseStatus = await SUT.AuthenticateUserAsync(UserName, "password");
            }

            [Test]
            public void ThenGetDecryptedUserIsCalledOnce()
            {
                CacheCommandsMock.Verify(m => m.GetDecryptedUser(It.IsAny<string>()), Times.Once());
            }

            [Test]
            public void ThenGetDecryptedUserIsCalledOnceWithTheCorrectParameters()
            {
                CacheCommandsMock.Verify(m => m.GetDecryptedUser(It.Is<string>(x => x == UserName)), Times.Once());
            }

            [Test]
            public void ThenGetAllAsyncCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<User>(), Times.Once());
            }

            [Test]
            public void ThenDecryptPasswordStringIsNeverCalled()
            {
                EncryptionHandlerMock.Verify(m => m.DecryptString(It.IsAny<string>()), Times.Never());
            }

            [Test]
            public void ThenResponseIsUserNotFound()
            {
                Assert.AreEqual(HttpResponseStatus.NotFound, _httpResponseStatus);
            }
        }

        public class WhenTheUserDoesnNotExistInTheDatabase : GivenAuthenticatingAUser
        {
            protected async override void When()
            {
                _httpResponseStatus = await SUT.AuthenticateUserAsync(NotInDatabaseDecryptedUserName, "password");
            }

            [Test]
            public void ThenGetDecryptedUserIsCalledOnce()
            {
                CacheCommandsMock.Verify(m => m.GetDecryptedUser(It.IsAny<string>()), Times.Once());
            }

            [Test]
            public void ThenGetDecryptedUserIsCalledOnceWithTheCorrectParameters()
            {
                CacheCommandsMock.Verify(m => m.GetDecryptedUser(It.Is<string>(x => x == NotInDatabaseDecryptedUserName)), Times.Once());
            }

            [Test]
            public void ThenGetAllAsyncCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<User>(), Times.Once());
            }

            [Test]
            public void ThenDecryptPasswordStringIsNeverCalled()
            {
                EncryptionHandlerMock.Verify(m => m.DecryptString(It.IsAny<string>()), Times.Never());
            }

            [Test]
            public void ThenResponseIsUserNotFound()
            {
                Assert.AreEqual(HttpResponseStatus.NotFound, _httpResponseStatus);
            }
        }

        public class WhenTheUserExistsAndHasTheCorrectPassword : GivenAuthenticatingAUser
        {
            protected async override void When()
            {
                _httpResponseStatus = await SUT.AuthenticateUserAsync(ExisingUserName, CorrectPassword);
            }

            [Test]
            public void ThenGetDecryptedUserIsCalledOnce()
            {
                CacheCommandsMock.Verify(m => m.GetDecryptedUser(It.IsAny<string>()), Times.Once());
            }

            [Test]
            public void ThenGetDecryptedUserIsCalledOnceWithTheCorrectParameters()
            {
                CacheCommandsMock.Verify(m => m.GetDecryptedUser(It.Is<string>(x => x == ExisingUserName)), Times.Once());
            }

            [Test]
            public void ThenGetAllAsyncCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<User>(), Times.Once());
            }

            [Test]
            public void ThenDecryptPasswordStringIsCalledTwice()
            {
                EncryptionHandlerMock.Verify(m => m.DecryptString(It.IsAny<string>()), Times.Exactly(2));
            }

            [Test]
            public void ThenDecryptPasswordStringIsCalledOnceWithTheCorrectPassword()
            {
                EncryptionHandlerMock.Verify(m => m.DecryptString(It.Is<string>(x => x == CorrectPassword)), Times.Exactly(2));
            }

            [Test]
            public void ThenResponseIsUserSuccess()
            {
                Assert.AreEqual(HttpResponseStatus.Success, _httpResponseStatus);
            }
        }

        public class WhenTheUserExistsAndHasTheWrongPassword : GivenAuthenticatingAUser
        {
            protected async override void When()
            {
                _httpResponseStatus = await SUT.AuthenticateUserAsync(ExisingUserName, IncorrectPassword);
            }

            [Test]
            public void ThenGetDecryptedUserIsCalledOnce()
            {
                CacheCommandsMock.Verify(m => m.GetDecryptedUser(It.IsAny<string>()), Times.Once());
            }

            [Test]
            public void ThenGetDecryptedUserIsCalledOnceWithTheCorrectParameters()
            {
                CacheCommandsMock.Verify(m => m.GetDecryptedUser(It.Is<string>(x => x == ExisingUserName)), Times.Once());
            }

            [Test]
            public void ThenGetAllAsyncCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<User>(), Times.Once());
            }

            [Test]
            public void ThenDecryptPasswordStringIsCalledTwice()
            {
                EncryptionHandlerMock.Verify(m => m.DecryptString(It.IsAny<string>()), Times.Exactly(2));
            }

            [Test]
            public void ThenDecryptPasswordStringIsCalledOnceWithTheCorrectPassword()
            {
                EncryptionHandlerMock.Verify(m => m.DecryptString(It.Is<string>(x => x == CorrectPassword)), Times.Once());
            }

            [Test]
            public void ThenDecryptPasswordStringIsCalledOnceWithTheInCorrectPassword()
            {
                EncryptionHandlerMock.Verify(m => m.DecryptString(It.Is<string>(x => x == IncorrectPassword)), Times.Once());
            }

            [Test]
            public void ThenResponseIsUserSuccess()
            {
                Assert.AreEqual(HttpResponseStatus.Unauthorised, _httpResponseStatus);
            }
        }

        public class WhenTheUserExistsButIsNotInTheCachedDecryptedUsers : GivenAuthenticatingAUser
        {
            protected async override void When()
            {
                _httpResponseStatus = await SUT.AuthenticateUserAsync(ExistingUserButNotDecryptedUserName, CorrectPassword);
            }

            [Test]
            public void ThenGetDecryptedUserIsCalledOnce()
            {
                CacheCommandsMock.Verify(m => m.GetDecryptedUser(It.IsAny<string>()), Times.Once());
            }

            [Test]
            public void ThenGetDecryptedUserIsCalledOnceWithTheCorrectParameters()
            {
                CacheCommandsMock.Verify(m => m.GetDecryptedUser(It.Is<string>(x => x == ExistingUserButNotDecryptedUserName)), Times.Once());
            }

            [Test]
            public void ThenGetAllAsyncCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<User>(), Times.Once());
            }

            [Test]
            public void ThenDecryptPasswordStringIsCalledTheCorrectAmountOfTimes()
            {
                EncryptionHandlerMock.Verify(m => m.DecryptString(It.IsAny<string>()), Times.Exactly(3));
            }

            [Test]
            public void ThenDecryptPasswordStringIsCalledOnceWithTheCorrectPassword()
            {
                EncryptionHandlerMock.Verify(m => m.DecryptString(It.Is<string>(x => x == CorrectPassword)), Times.Exactly(2));
            }

            [Test]
            public void ThenDecryptPasswordStringIsCalledOnceWithTheCorrectUserName()
            {
                EncryptionHandlerMock.Verify(m => m.DecryptString(It.Is<string>(x => x == ExistingUserButNotDecryptedUserName)), Times.Once());
            }

            [Test]
            public void ThenAddDecryptedUserToCacheIsCalledOnce()
            {
                CacheCommandsMock.Verify(m => m.AddDecryptedUserToCache(It.IsAny<DecryptedUser>()), Times.Once());
            }

            [Test]
            public void ThenAddDecryptedUserToCacheIsCalledOnceWithTheCorrectUserId()
            {
                CacheCommandsMock.Verify(m => m.AddDecryptedUserToCache(It.Is<DecryptedUser>(x => x.UserId == 7)), Times.Once());
            }

            [Test]
            public void ThenResponseIsSuccess()
            {
                Assert.AreEqual(HttpResponseStatus.Success, _httpResponseStatus);
            }
        }

        public class WhenTheUserDoesNotExistAnyMoreInTheDatabaseButIsStillInTheDecryptedUserCache : GivenAuthenticatingAUser
        {
            protected async override void When()
            {
                _httpResponseStatus = await SUT.AuthenticateUserAsync(UserNotInDatabaseButStillInDecryptedUsersCacheUserName, CorrectPassword);
            }

            [Test]
            public void ThenGetDecryptedUserIsCalledOnce()
            {
                CacheCommandsMock.Verify(m => m.GetDecryptedUser(It.IsAny<string>()), Times.Once());
            }

            [Test]
            public void ThenGetDecryptedUserIsCalledOnceWithTheCorrectParameters()
            {
                CacheCommandsMock.Verify(m => m.GetDecryptedUser(It.Is<string>(x => x == UserNotInDatabaseButStillInDecryptedUsersCacheUserName)), Times.Once());
            }

            [Test]
            public void ThenGetAllAsyncCalledOnce()
            {
                BaseCommandsMock.Verify(m => m.GetAllAsync<User>(), Times.Once());
            }

            [Test]
            public void ThenDecryptPasswordStringIsNeverCalled()
            {
                EncryptionHandlerMock.Verify(m => m.DecryptString(It.IsAny<string>()), Times.Never());
            }

            [Test]
            public void ThenAddDecryptedUserToCacheIsNeverCalled()
            {
                CacheCommandsMock.Verify(m => m.AddDecryptedUserToCache(It.IsAny<DecryptedUser>()), Times.Never());
            }

            [Test]
            public void ThenRemoveDecryptedUserFromCacheIsCalledOnce()
            {
                CacheCommandsMock.Verify(m => m.RemoveDecryptedUserFromCache(It.IsAny<int>()), Times.Once());
            }

            [Test]
            public void ThenRemoveDecryptedUserFromCacheIsCalledOnceWithTheCorrectUserId()
            {
                CacheCommandsMock.Verify(m => m.RemoveDecryptedUserFromCache(It.Is<int>(userId => userId == 8)), Times.Once());
            }

            [Test]
            public void ThenResponseIsNotFound()
            {
                Assert.AreEqual(HttpResponseStatus.NotFound, _httpResponseStatus);
            }
        }
    }
}
