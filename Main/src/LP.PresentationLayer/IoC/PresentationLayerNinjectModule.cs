using LP.Api.Shared.Interfaces.Wrappers;
using LP.PresentationLayer.Wrappers;
using Ninject.Modules;
using Ninject.Web.Common;

namespace LP.PresentationLayer.IoC
{
    public class PresentationLayerNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IServerWrapper>().To<ServerWrapper>().InRequestScope();
            Bind<IFactoryDirectoryInfoWrapper>().To<FactoryDirectoryInfoWrapper>().InRequestScope();
        }
    }
}