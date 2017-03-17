using JetBrains.Annotations;
using LP.Api.Shared.Interfaces.Data;
using LP.Data.Commands;
using LP.Host.Integration.IoC;
using Ninject;

namespace LP.Host.Integration.Providers
{
    public static  class BaseCommandsProvider
    {
        private static readonly IBaseCommands BaseCommands = SetupNinjectDependencies.CreateKernel().Get<BaseCommands>();

        public static IBaseCommands GetBaseCommandsInstance()
        {
            return BaseCommands;// ?? (_baseCommands = SetupNinjectDependencies.CreateKernel().Get<BaseCommands>());
        }
    }
}
