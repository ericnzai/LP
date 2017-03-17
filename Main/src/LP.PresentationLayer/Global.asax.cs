using System.Data.Entity.Infrastructure.Interception;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Ask.Core.Logging.Interceptors;
using Ask.Core.Logging.Migrations;
using LP.Data.Migrations;

namespace LP.PresentationLayer
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
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
