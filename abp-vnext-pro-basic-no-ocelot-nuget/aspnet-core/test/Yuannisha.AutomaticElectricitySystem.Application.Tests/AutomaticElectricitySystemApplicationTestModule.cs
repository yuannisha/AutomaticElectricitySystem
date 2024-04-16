using Volo.Abp.Modularity;

namespace Yuannisha.AutomaticElectricitySystem
{
    [DependsOn(
        typeof(AutomaticElectricitySystemApplicationModule),
        typeof(AutomaticElectricitySystemDomainTestModule)
        )]
    public class AutomaticElectricitySystemApplicationTestModule : AbpModule
    {

    }
}