using System.Collections.Generic;
using System.Linq;

using LP.Api.Shared.Interfaces.Core.Caching;
using LP.Api.Shared.Interfaces.Core.Encryption;
using LP.Api.Shared.Interfaces.Data;
using LP.EntityModels;
using LP.Model.Authentication;
using LP.ServiceHost.Common.BusinessLayer.Commands;
using Moq;
using SpecsFor;

namespace LP.ServiceHost.Common.BusinessLayer.Tests.Commands.CacheCommandsTests
{
    public class BaseGiven : SpecsFor<CacheCommands>
    {
        protected readonly Mock<IBaseCommands> UserBaseCommandsMock = new Mock<IBaseCommands>();
        protected readonly Mock<IMemoryCacheWrapper<DecryptedUser>> DecryptedUserMemoryCacheWrapper = new Mock<IMemoryCacheWrapper<DecryptedUser>>();
        protected readonly Mock<IEncryptionHandler> EncryptionHandlerMock = new Mock<IEncryptionHandler>();

        protected List<DecryptedUser> UserCacheItems = new List<DecryptedUser>
        {
            new DecryptedUser{UserId = 1, DecryptedUserName = "User 1"},
            new DecryptedUser{UserId = 2, DecryptedUserName = "User 2"},
            new DecryptedUser{UserId = 3, DecryptedUserName = "User 3"},
            new DecryptedUser{UserId = 4, DecryptedUserName = "User 4"},
        };

        protected List<User> Users = new List<User>(); 

        protected void PrepareSut()
        {
            UserBaseCommandsMock.Setup(m => m.GetAll<User>()).Returns(Users.AsQueryable());

            DecryptedUserMemoryCacheWrapper.Setup(m => m.Get()).Returns(UserCacheItems);

            DecryptedUserMemoryCacheWrapper.Setup(m => m.Set(It.IsAny<List<DecryptedUser>>()))
                .Callback((List<DecryptedUser> userCacheItems) => UserCacheItems.AddRange(userCacheItems));
                
            DecryptedUserMemoryCacheWrapper.Setup(m => m.Add(It.IsAny<DecryptedUser>()))
                .Callback((DecryptedUser userCacheItem) => UserCacheItems.Add(userCacheItem));
                
            EncryptionHandlerMock.Setup(m => m.DecryptString(It.IsAny<string>()))
                .Returns<string>(encryptedString => encryptedString);

            SUT = new CacheCommands(UserBaseCommandsMock.Object, DecryptedUserMemoryCacheWrapper.Object, EncryptionHandlerMock.Object);
        }
    }
}
