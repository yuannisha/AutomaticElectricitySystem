using Lion.AbpPro.BasicManagement;
using Lion.AbpPro.BasicManagement.Localization;
using Lion.AbpPro.Core;
using Lion.AbpPro.DataDictionaryManagement;
using Lion.AbpPro.NotificationManagement;

namespace Yuannisha.AutomaticElectricitySystem
{
    [DependsOn(
        typeof(BasicManagementDomainSharedModule),
        typeof(NotificationManagementDomainSharedModule),
        typeof(DataDictionaryManagementDomainSharedModule),
        typeof(LionAbpProCoreModule)
    )]
    public class AutomaticElectricitySystemDomainSharedModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            AutomaticElectricitySystemGlobalFeatureConfigurator.Configure();
            AutomaticElectricitySystemModuleExtensionConfigurator.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<AutomaticElectricitySystemDomainSharedModule>(AutomaticElectricitySystemDomainSharedConsts.NameSpace);
            });
          
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<AutomaticElectricitySystemResource>(AutomaticElectricitySystemDomainSharedConsts.DefaultCultureName)
                    .AddVirtualJson("/Localization/AutomaticElectricitySystem")
                    .AddBaseTypes(typeof(BasicManagementResource))
                    .AddBaseTypes(typeof(AbpTimingResource));

                options.DefaultResourceType = typeof(AutomaticElectricitySystemResource);
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace(AutomaticElectricitySystemDomainSharedConsts.NameSpace, typeof(AutomaticElectricitySystemResource));
            });
        }

       
    }
}