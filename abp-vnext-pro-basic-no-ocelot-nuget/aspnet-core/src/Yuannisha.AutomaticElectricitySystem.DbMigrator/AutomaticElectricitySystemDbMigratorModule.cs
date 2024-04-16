namespace Yuannisha.AutomaticElectricitySystem.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AutomaticElectricitySystemEntityFrameworkCoreModule),
        typeof(AutomaticElectricitySystemApplicationContractsModule)
        )]
    public class AutomaticElectricitySystemDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
