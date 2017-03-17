using LP.Api.Shared.Binding;
using LP.Api.Shared.HttpClient;
using LP.Api.Shared.Interfaces.BusinessLayer.Common.Filters;
using LP.Api.Shared.Requests;
using Ninject.Modules;
using Ninject.Web.Common;

namespace LP.Api.Shared.IoC
{
    public class ApiSharedNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IHttpClientWrapperAsync>().To<HttpClientWrapperAsync>().InRequestScope();
            Bind<IRequestExecutor>().To<RequestExecutor>().InRequestScope();
            Bind<IHttpContentBinding>().To<HttpContentBinding>().InRequestScope();
        }
    }
}
