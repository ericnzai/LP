using System.Data.Entity;
using System.Linq;
using LP.Data.Context;

namespace LP.Data.Migrations
{
    public class MigrateDb : MigrateDatabaseToLatestVersion<LearningPlatformCodeFirstContext, Configuration>
    {
        public override void InitializeDatabase(LearningPlatformCodeFirstContext context)
        {
            var configuration = new Configuration();
            var migrator = new System.Data.Entity.Migrations.DbMigrator(configuration);
            if (migrator.GetPendingMigrations().Any())
            {
                migrator.Update();
            }
        }

    }
}
