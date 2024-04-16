using Lion.AbpPro.BasicManagement;
using Lion.AbpPro.DataDictionaryManagement;
using Lion.AbpPro.NotificationManagement;

namespace Yuannisha.AutomaticElectricitySystem
{
    [DependsOn(
        typeof(AutomaticElectricitySystemDomainSharedModule),
        typeof(BasicManagementDomainModule),
        typeof(NotificationManagementDomainModule),
        typeof(DataDictionaryManagementDomainModule)
    )]
    public class AutomaticElectricitySystemDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpMultiTenancyOptions>(options => { options.IsEnabled = MultiTenancyConsts.IsEnabled; });
            Configure<AbpAutoMapperOptions>(options => { options.AddMaps<AutomaticElectricitySystemDomainModule>(); });
        }
    }
}