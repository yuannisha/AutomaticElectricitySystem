using Lion.AbpPro.DataDictionaryManagement;

namespace Yuannisha.AutomaticElectricitySystem
{
    [DependsOn(
        typeof(AutomaticElectricitySystemApplicationContractsModule),
        typeof(BasicManagementHttpApiModule),
        typeof(NotificationManagementHttpApiModule),
        typeof(DataDictionaryManagementHttpApiModule)
        )]
    public class AutomaticElectricitySystemHttpApiModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            ConfigureLocalization();
        }

        private void ConfigureLocalization()
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<AutomaticElectricitySystemResource>()
                    .AddBaseTypes(
                        typeof(AbpUiResource)
                    );
            });
        }
    }
}
