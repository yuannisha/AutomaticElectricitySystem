using Lion.AbpPro.BasicManagement.EntityFrameworkCore;
using Lion.AbpPro.DataDictionaryManagement.DataDictionaries.Aggregates;
using Lion.AbpPro.DataDictionaryManagement.EntityFrameworkCore;
using Lion.AbpPro.NotificationManagement.EntityFrameworkCore;
using Lion.AbpPro.NotificationManagement.Notifications.Aggregates;
using Yuannisha.AutomaticElectricitySystem.BookingInformationsEntity;
using Yuannisha.AutomaticElectricitySystem.BookingLimitedsEntity;
using Yuannisha.AutomaticElectricitySystem.BuildingsEntity;
using Yuannisha.AutomaticElectricitySystem.DailyTotalConsumptionEntity;
using Yuannisha.AutomaticElectricitySystem.PowerConsumption;
using Yuannisha.AutomaticElectricitySystem.PowerSwitchsEntity;
using Yuannisha.AutomaticElectricitySystem.RoomsEntity;

namespace Yuannisha.AutomaticElectricitySystem.EntityFrameworkCore
{
    /* This is your actual DbContext used on runtime.
     * It includes only your entities.
     * It does not include entities of the used modules, because each module has already
     * its own DbContext class. If you want to share some database tables with the used modules,
     * just create a structure like done for AppUser.
     *
     * Don't use this DbContext for database migrations since it does not contain tables of the
     * used modules (as explained above). See AutomaticElectricitySystemMigrationsDbContext for migrations.
     */
    [ConnectionStringName("Default")]
    public class AutomaticElectricitySystemDbContext : AbpDbContext<AutomaticElectricitySystemDbContext>, 
        IAutomaticElectricitySystemDbContext,
        IBasicManagementDbContext,
        INotificationManagementDbContext,
        IDataDictionaryManagementDbContext
    {
        public DbSet<IdentityUser> Users { get; set; }
        public DbSet<IdentityRole> Roles { get; set; }
        public DbSet<IdentityClaimType> ClaimTypes { get; set; }
        public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
        public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
        public DbSet<IdentityLinkUser> LinkUsers { get; set; }
        public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
        public DbSet<FeatureGroupDefinitionRecord> FeatureGroups { get; set; }
        public DbSet<FeatureDefinitionRecord> Features { get; set; }
        public DbSet<FeatureValue> FeatureValues { get; set; }
        public DbSet<PermissionGroupDefinitionRecord> PermissionGroups { get; set; }
        public DbSet<PermissionDefinitionRecord> Permissions { get; set; }
        public DbSet<PermissionGrant> PermissionGrants { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }
        public DbSet<BackgroundJobRecord> BackgroundJobs { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<DataDictionary> DataDictionaries { get; set; }
        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<PowerSwitchs> PowerSwitchs { get; set; }
        public DbSet<Buildings> Buildings { get; set; }
        public DbSet<BookingInformation> BookingInformations { get; set; }
        public DbSet<BookingLimited> BookingLimiteds { get; set; }
        public DbSet<BuildingConsumption> BuildingConsumption { get; set; }
        public DbSet<DailyTotalConsumption> DailyTotalConsumption { get; set; }

        public AutomaticElectricitySystemDbContext(DbContextOptions<AutomaticElectricitySystemDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            
            builder.ConfigureAutomaticElectricitySystem();

            // 基础模块
            builder.ConfigureBasicManagement();
            
            // 消息通知
            builder.ConfigureNotificationManagement();
            
            //数据字典
            builder.ConfigureDataDictionaryManagement();
            
        }
    }
}