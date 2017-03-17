using System.Data.Entity.Infrastructure.Interception;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Ask.Core.Logging.Interceptors;
using Ask.Core.Logging.Migrations;
using LP.Api.Shared.Interfaces.BusinessLayer.Common;
using LP.Data.Migrations;

namespace LP.Host
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ConfigureDatabase();
        }

        private static void ConfigureDatabase()
        {
            var initializeDatabaseMigrations = new MigrateDb();
            initializeDatabaseMigrations.InitializeDatabase(null);

            var initializeLoggerMigrations = new LoggerMigrateDb();
            initializeLoggerMigrations.InitializeDatabase(null);
            DbInterception.Add(new LoggerCommandInterceptor());
        }

        protected void Application_PreSendRequestHeaders()
        {
            Response.Headers.Remove("Server");
            Response.Headers.Remove("X-AspNet-Version");
            Response.Headers.Remove("X-AspNetMvc-Version");
        }
    }
}
