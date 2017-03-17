using System;
using System.Configuration;
using System.Linq;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Cors;
using System.Web.Http;
using Ask.Core.Logging;
using LP.Api.Shared.Attributes;
using LP.Api.Shared.Formatters;
using LP.Api.Shared.Interfaces.BusinessLayer.Authentication;
using LP.Api.Shared.Interfaces.Data;
using LP.Api.Shared.IoC;
using LP.Api.Shared.Providers;
using LP.Authentication.IoC;
using LP.Content.IoC;
using LP.Core.IoC;
using LP.Data.Commands;
using LP.Data.Context;
using LP.Exams.IoC;
using LP.PresentationLayer.IoC;
using LP.ServiceHost.Common.BusinessLayer.IoC;
using LP.Translation.IoC;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;

[assembly: OwinStartup(typeof(LP.Host.Startup))]
namespace LP.Host
{
    public class Startup
    {
        private StandardKernel _standardKernel;
        public void Configuration(IAppBuilder app)
        {
            var policy = new CorsPolicy
            {
                AllowAnyHeader = true,
                AllowAnyMethod = true,
                AllowAnyOrigin = true,
                SupportsCredentials = true
            };

            policy.ExposedHeaders.Add("x-culture");

            app.UseCors(new CorsOptions
            {
                PolicyProvider = new CorsPolicyProvider
                {
                    PolicyResolver = context => Task.FromResult(policy)
                }
            });

            _standardKernel = CreateKernel();

            var webApiConfiguration = new HttpConfiguration();
            webApiConfiguration.Filters.Add(new AskErrorAttribute());
            webApiConfiguration.MapHttpAttributeRoutes();
            
            

            ConfigureWebApi(webApiConfiguration);
            ConfigureOAuthTokenGeneration(app);
            ConfigureOAuthTokenConsumption(app);

            app.UseNinjectMiddleware(() => _standardKernel);
            app.UseNinjectWebApi(webApiConfiguration);

        }

        
        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            try
            {
                var executingAssembly = Assembly.GetExecutingAssembly();
                kernel.Load(executingAssembly);
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                InitializeLoggingServiceLocator(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        private static void InitializeLoggingServiceLocator(IKernel container)
        {
            LoggingServiceLocator.Initialize(container);
            LoggingServiceLocator.ApplicationEnvironment = string.Format("LearningPlatform.{0}", "Dev");// ConfigurationHelper.Environment);
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Load(new AuthenticationNinjectModule());
            kernel.Load(new ApiSharedNinjectModule());
            kernel.Load(new ExamsNinjectModule());
            kernel.Load(new TranslationNinjectModule());
            kernel.Load(new ContentNinjectModule());
            kernel.Load(new CommonBusinessLayerNinjectModule());
            kernel.Load(new CoreNinjectModule());
            //kernel.Load(new ApiHostNinjectModule());
            kernel.Load(new PresentationLayerNinjectModule());
            BindDataDependencies(kernel);
           
            LoadLoggerModule(kernel);
        }

        private static void LoadLoggerModule(IKernel kernel)
        {
            kernel.Load(new Ask.Core.Logging.IoC.LoggerBindings());   
        }

        private static void BindDataDependencies(IKernel kernel)
        {
            kernel.Bind<LearningPlatformCodeFirstContext>().ToSelf().InRequestScope();
            kernel.Bind<IBaseCommands>().To<BaseCommands>().InRequestScope();
        }

        private void ConfigureWebApi(HttpConfiguration config)
        {
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
           
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };
      
            config.Formatters.JsonFormatter.SerializerSettings = jsonSerializerSettings;

            jsonFormatter.SerializerSettings = jsonSerializerSettings;

        }

        private void ConfigureOAuthTokenGeneration(IAppBuilder app)
        {
            var oAuthProvider = new OAuthProvider(_standardKernel.Get<IUserCommands>());

            var oAuthServerOptions = new OAuthAuthorizationServerOptions
            {
                //For Dev enviroment only (on production should be AllowInsecureHttp = false)
                AllowInsecureHttp = Convert.ToBoolean(ConfigurationManager.AppSettings["AllowInsecureHttp"]),
                TokenEndpointPath = new PathString("/oauth/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = oAuthProvider,
                AccessTokenFormat = new CustomJwtFormat(ConfigurationManager.AppSettings["BaseUrl"])
            };

            // OAuth 2.0 Bearer Access Token Generation
            app.UseOAuthAuthorizationServer(oAuthServerOptions);
        }

        private static void ConfigureOAuthTokenConsumption(IAppBuilder app)
        {

            var issuer = ConfigurationManager.AppSettings["BaseUrl"];
            var audienceId = ConfigurationManager.AppSettings["as:audienceId"];
            var audienceSecret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["as:AudienceSecret"]);

            // Api controllers with an [Authorize] attribute will be validated with JWT

            var jwtBearerAuthenticationOptions = new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                AllowedAudiences = new[] { audienceId },
                IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                {
                    new SymmetricKeyIssuerSecurityTokenProvider(issuer, audienceSecret)
                }
            };

            app.UseJwtBearerAuthentication(jwtBearerAuthenticationOptions);
        }
    }

}