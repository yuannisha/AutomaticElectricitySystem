using Lion.AbpPro.BasicManagement;
using Lion.AbpPro.DataDictionaryManagement;
using Lion.AbpPro.NotificationManagement;

namespace Yuannisha.AutomaticElectricitySystem
{
    [DependsOn(
        typeof(AutomaticElectricitySystemDomainSharedModule),
        typeof(AbpObjectExtendingModule),
        typeof(BasicManagementApplicationContractsModule),
        typeof(NotificationManagementApplicationContractsModule),
        typeof(DataDictionaryManagementApplicationContractsModule)
    )]
    public class AutomaticElectricitySystemApplicationContractsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            AutomaticElectricitySystemDtoExtensions.Configure();
        }
    }
}
