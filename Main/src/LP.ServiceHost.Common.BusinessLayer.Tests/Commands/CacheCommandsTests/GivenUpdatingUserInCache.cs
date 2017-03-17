using System.Collections.Generic;
using LP.Model.Authentication;
using Moq;
using NUnit.Framework;

namespace LP.ServiceHost.Common.BusinessLayer.Tests.Commands.CacheCommandsTests
{
    public class GivenUpdatingUserInCache : BaseGiven
    {
        protected override void Given()
        {
            PrepareSut();
        }

        public class WhenTheCacheHasSomeUsersAndTheUserBeingAddedIsNotAlreadyThere : GivenUpdatingUserInCache
        {
            protected override void When()
            {
                var decryptedUser = new DecryptedUser { UserId = 5, DecryptedUserName = "Test user 5" };

                SUT.UpdateDecryptedUserInCache(decryptedUser);
            }

            [Test]
            public void ThenGetCachedUsersIsCalledOnce()
            {
                DecryptedUserMemoryCacheWrapper.Verify(m => m.Get(), Times.Once());
            }

            [Test]
            public void TheSetCachedUsersIsCalledOnce()
            {
                DecryptedUserMemoryCacheWrapper.Verify(m => m.Set(It.IsAny<List<DecryptedUser>>()), Times.Once());
            }
        }

        public class WhenTheCacheHasSomeUsersAndTheUserBeingAddedIsAlreadyThere : GivenUpdatingUserInCache
        {
            protected override void When()
            {
                var decrpytedUser = new DecryptedUser { UserId = 4, DecryptedUserName = "User 4" };

                SUT.UpdateDecryptedUserInCache(decrpytedUser);
            }

            [Test]
            public void ThenGetCachedUsersIsCalledOnce()
            {
                DecryptedUserMemoryCacheWrapper.Verify(m => m.Get(), Times.Once());
            }

            [Test]
            public void TheSetCachedUsersIsNeverCalled()
            {
                DecryptedUserMemoryCacheWrapper.Verify(m => m.Set(It.IsAny<List<DecryptedUser>>()), Times.Never());
            }
        }
    }
}
