namespace Yuannisha.AutomaticElectricitySystem.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class AutomaticElectricitySystemMigrationsDbContextFactory :
        IDesignTimeDbContextFactory<AutomaticElectricitySystemDbContext>
    {
        public AutomaticElectricitySystemDbContext CreateDbContext(string[] args)
        {
            AutomaticElectricitySystemEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<AutomaticElectricitySystemDbContext>()
                .UseMySql(configuration.GetConnectionString("Default"), 
                    MySqlServerVersion.LatestSupportedServerVersion,mySqlOptionsAction:
                    opt =>
                    {
                        opt.EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null);
                    });

            return new AutomaticElectricitySystemDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath
                (
                    Path.Combine
                    (
                        Directory.GetCurrentDirectory(),
                        "../Yuannisha.AutomaticElectricitySystem.DbMigrator/"
                    )
                )
                .AddJsonFile
                (
                    "appsettings.json",
                    false
                );

            return builder.Build();
        }
    }
}