using System;
using System.Reflection;
using LP.Api.Shared.Interfaces.Data;
using LP.Data.Commands;
using LP.Data.Context;
using LP.Host.Integration.HttpClient;
using Ninject;
using Ninject.Web.Common;

namespace LP.Host.Integration.IoC
{
    public static class SetupNinjectDependencies
    {
        public static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            try
            {
                var executingAssembly = Assembly.GetExecutingAssembly();
                kernel.Load(executingAssembly);
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
               
                RegisterServices(kernel);
               
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        private static void RegisterServices(IKernel kernel)
        {
            BindDataDependencies(kernel);

        }

        private static void BindDataDependencies(IKernel kernel)
        {
            kernel.Bind<LearningPlatformCodeFirstContext>().ToSelf().InRequestScope();
            kernel.Bind<IBaseCommands>().To<BaseCommands>().InRequestScope();
            kernel.Bind<IHttpClientWrapper>().To<HttpClientWrapper>().InRequestScope();
        }
    }

    
}
