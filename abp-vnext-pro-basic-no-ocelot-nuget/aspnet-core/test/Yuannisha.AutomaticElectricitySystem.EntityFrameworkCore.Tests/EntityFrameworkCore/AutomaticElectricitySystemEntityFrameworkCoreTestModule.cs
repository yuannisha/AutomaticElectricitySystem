namespace Yuannisha.AutomaticElectricitySystem.EntityFrameworkCore
{
    [DependsOn(
        typeof(AutomaticElectricitySystemTestBaseModule),
        typeof(AutomaticElectricitySystemEntityFrameworkCoreModule),
        typeof(AbpEntityFrameworkCoreSqliteModule)
        )]
    public class AutomaticElectricitySystemEntityFrameworkCoreTestModule : AbpModule
    {
        private SqliteConnection _sqliteConnection;

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            ConfigureInMemorySqlite(context.Services);
        }

        private void ConfigureInMemorySqlite(IServiceCollection services)
        {
            _sqliteConnection = CreateDatabaseAndGetConnection();

            services.Configure<AbpDbContextOptions>(options =>
            {
                options.PreConfigure<AutomaticElectricitySystemDbContext>(options =>
                {
                    options.DbContextOptions.UseBatchEF_Sqlite();
                });
                options.Configure(context =>
                {
                    context.DbContextOptions.UseSqlite(_sqliteConnection);
                });
            });
        }

        public override void OnApplicationShutdown(ApplicationShutdownContext context)
        {
            _sqliteConnection.Dispose();
        }

        private static SqliteConnection CreateDatabaseAndGetConnection()
        {
            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<AutomaticElectricitySystemDbContext>()
                .UseSqlite(connection)
                .Options;

            using (var context = new AutomaticElectricitySystemDbContext(options))
            {
                context.GetService<IRelationalDatabaseCreator>().CreateTables();
            }

            return connection;
        }
    }
}
