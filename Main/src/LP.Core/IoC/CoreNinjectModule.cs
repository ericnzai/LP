using LP.Api.Shared.Interfaces.Core.Caching;
using LP.Api.Shared.Interfaces.Core.Encryption;
using LP.Core.Caching;
using LP.Core.Encryption;
using LP.Model.Authentication;
using Ninject.Modules;

namespace LP.Core.IoC
{
    public class CoreNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IEncryptionHandler>().To<EncryptionHandler>().InTransientScope();
            Bind<IMemoryCacheWrapper<DecryptedUser>>().To<DecryptedUserMemoryCacheWrapper>().InTransientScope();
        }
    }
}
