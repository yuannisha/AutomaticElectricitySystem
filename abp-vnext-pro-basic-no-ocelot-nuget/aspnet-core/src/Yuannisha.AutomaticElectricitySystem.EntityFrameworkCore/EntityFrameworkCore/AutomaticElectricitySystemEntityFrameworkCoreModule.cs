using Lion.AbpPro.BasicManagement.EntityFrameworkCore;
using Lion.AbpPro.DataDictionaryManagement.EntityFrameworkCore;
using Lion.AbpPro.NotificationManagement.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.Guids;
using Yuannisha.AutomaticElectricitySystem.DailyTotalConsumptionEntity;

namespace Yuannisha.AutomaticElectricitySystem.EntityFrameworkCore
{
    [DependsOn(
        typeof(AutomaticElectricitySystemDomainModule),
        typeof(AbpEntityFrameworkCoreMySQLModule),
        typeof(BasicManagementEntityFrameworkCoreModule),
        typeof(DataDictionaryManagementEntityFrameworkCoreModule),
        typeof(NotificationManagementEntityFrameworkCoreModule)
    )]
    public class AutomaticElectricitySystemEntityFrameworkCoreModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            AutomaticElectricitySystemEfCoreEntityExtensionMappings.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<AutomaticElectricitySystemDbContext>(options =>
            {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
                options.AddDefaultRepositories(includeAllEntities: true);

            });
            Configure<AbpDbContextOptions>(options =>
            {
                /* The main point to change your DBMS.
                 * See also AutomaticElectricitySystemMigrationsDbContextFactory for EF Core tooling. */
                options.UseMySQL();
            });
        }
    }
}