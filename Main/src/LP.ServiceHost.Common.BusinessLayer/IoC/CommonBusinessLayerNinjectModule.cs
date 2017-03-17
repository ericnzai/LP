using LP.Api.Shared.Interfaces.BusinessLayer.Common;
using LP.Api.Shared.Interfaces.BusinessLayer.Common.Commands;
using LP.Api.Shared.Interfaces.BusinessLayer.Common.Filters;
using LP.Api.Shared.Interfaces.BusinessLayer.Common.Providers;
using LP.ServiceHost.Common.BusinessLayer.Commands;
using LP.ServiceHost.Common.BusinessLayer.Filters;
using LP.ServiceHost.Common.BusinessLayer.Providers;
using Ninject.Modules;

namespace LP.ServiceHost.Common.BusinessLayer.IoC
{
    public class CommonBusinessLayerNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAskApiBusiness>().To<AskApiBusiness>().InTransientScope();
            Bind<ICacheCommands>().To<CacheCommands>().InTransientScope();
            Bind<IGroupCommands>().To<GroupCommands>().InTransientScope();
            Bind<ITrainingAreaCommands>().To<TrainingAreaCommands>().InTransientScope();
            Bind<IGroupPermissionCommands>().To<GroupPermissionCommands>().InTransientScope();
            Bind<ITrainingGroupPermissionFilter>().To<TrainingGroupPermissionFilter>().InTransientScope();
            Bind<IAvailableStatusesProvider>().To<AvailableStatusesProvider>().InTransientScope();
            Bind<IRoleProvider>().To<RoleProvider>().InTransientScope();
            Bind<ILastAreasViewedProvider>().To<LastAreasViewedProvider>().InTransientScope();
            Bind<ICommonCalculatorCommands>().To<CommonCalculatorCommands>().InTransientScope();
            Bind<IUrlMapperCommands>().To<UrlMapperCommands>().InTransientScope();
            Bind<ITrainerCommands>().To<TrainerCommands>().InTransientScope();
            Bind<IGroupTypeCommands>().To<GroupTypeCommands>().InTransientScope();
            Bind<IPostPermissionFilter>().To<PostPermissionFilter>().InTransientScope();
        }
    }
}
