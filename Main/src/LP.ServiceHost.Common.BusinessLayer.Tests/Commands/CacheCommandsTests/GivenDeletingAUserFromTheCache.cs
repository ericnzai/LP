using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LP.Model.Authentication;
using Moq;
using NUnit.Framework;

namespace LP.ServiceHost.Common.BusinessLayer.Tests.Commands.CacheCommandsTests
{
    public class GivenDeletingAUserFromTheCache : BaseGiven
    {
        protected override void Given()
        {
            UserCacheItems = new List<DecryptedUser>
                {
                    new DecryptedUser{UserId = 1, DecryptedUserName = "User 1"},
                    new DecryptedUser{UserId = 2, DecryptedUserName = "User 2"},
                    new DecryptedUser{UserId = 3, DecryptedUserName = "User 3"},
                    new DecryptedUser{UserId = 4, DecryptedUserName = "User 4"},
                };

            PrepareSut();
        }

        public class WhenTheCacheHasSomeUsersAndTheUserBeingAddedIsNotAlreadyThere : GivenDeletingAUserFromTheCache
        {
            protected override void When()
            {
                SUT.RemoveDecryptedUserFromCache(5);
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

        public class WhenTheCacheHasSomeUsersAndTheUserBeingAddedIsAlreadyThere : GivenDeletingAUserFromTheCache
        {
            protected override void When()
            {
                SUT.RemoveDecryptedUserFromCache(4);
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
    }

}
