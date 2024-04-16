using Lion.AbpPro.DataDictionaryManagement;

namespace Yuannisha.AutomaticElectricitySystem
{
    [DependsOn(
        typeof(AutomaticElectricitySystemDomainModule),
        typeof(AutomaticElectricitySystemApplicationContractsModule),
        typeof(BasicManagementApplicationModule),
        typeof(NotificationManagementApplicationModule),
        typeof(DataDictionaryManagementApplicationModule),
        typeof(AutomaticElectricitySystemFreeSqlModule)
        )]
    public class AutomaticElectricitySystemApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<AutomaticElectricitySystemApplicationModule>();
            });
            
        }
    }
}
