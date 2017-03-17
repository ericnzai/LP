using LP.Api.Shared.Interfaces.BusinessLayer.Authentication;
using LP.Authentication.BusinessLayer;
using LP.Authentication.BusinessLayer.Commands;
using Ninject.Modules;
using Ninject.Web.Common;

namespace LP.Authentication.IoC
{
    public class AuthenticationNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IAskAuthenticationApiBusiness>().To<AskAuthenticationApiBusiness>().InRequestScope();
            Bind<IAuthenticationCommands>().To<AuthenticationCommands>().InRequestScope();
            Bind<IUserCommands>().To<UserCommands>().InRequestScope();
            Bind<IRoleCommands>().To<RoleCommands>().InRequestScope();
            Bind<IUserRoleCommands>().To<UserRoleCommands>().InRequestScope();
        }
    }
}