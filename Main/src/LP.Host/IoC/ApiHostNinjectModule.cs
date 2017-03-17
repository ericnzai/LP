using LP.Api.Shared.Interfaces.Api;
using LP.Host.Providers;
using Ninject.Modules;
using Ninject.Web.Common;

namespace LP.Host.IoC
{
    public class ApiHostNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICultureProvider>().To<CultureProvider>().InRequestScope();
            Bind<IConfigurationProvider>().To<ConfigurationProvider>().InRequestScope();
        }
    }
}