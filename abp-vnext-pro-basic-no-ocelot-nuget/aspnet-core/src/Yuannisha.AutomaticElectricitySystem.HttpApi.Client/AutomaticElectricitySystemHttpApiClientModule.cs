using Lion.AbpPro.BasicManagement;
using Lion.AbpPro.DataDictionaryManagement;
using Lion.AbpPro.NotificationManagement;

namespace Yuannisha.AutomaticElectricitySystem
{
    [DependsOn(
        typeof(AutomaticElectricitySystemApplicationContractsModule),
        typeof(BasicManagementHttpApiClientModule),
        typeof(NotificationManagementHttpApiClientModule),
        typeof(DataDictionaryManagementHttpApiClientModule)
    )]
    public class AutomaticElectricitySystemHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "Default";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(AutomaticElectricitySystemApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
