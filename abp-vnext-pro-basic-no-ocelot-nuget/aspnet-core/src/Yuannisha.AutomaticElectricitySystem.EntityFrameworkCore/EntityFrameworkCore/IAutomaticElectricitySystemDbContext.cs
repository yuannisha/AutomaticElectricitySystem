using Yuannisha.AutomaticElectricitySystem.BookingInformationsEntity;
using Yuannisha.AutomaticElectricitySystem.BookingLimitedsEntity;
using Yuannisha.AutomaticElectricitySystem.BuildingsEntity;
using Yuannisha.AutomaticElectricitySystem.DailyTotalConsumptionEntity;
using Yuannisha.AutomaticElectricitySystem.PowerConsumption;
using Yuannisha.AutomaticElectricitySystem.PowerSwitchsEntity;
using Yuannisha.AutomaticElectricitySystem.RoomsEntity;

namespace Yuannisha.AutomaticElectricitySystem.EntityFrameworkCore
{
    [ConnectionStringName("Default")]
    public interface IAutomaticElectricitySystemDbContext : IEfCoreDbContext
    {
        DbSet<Rooms> Rooms { get; set; }
        DbSet<PowerSwitchs> PowerSwitchs { get; set; }
        DbSet<Buildings> Buildings { get; set; }
        DbSet<BookingInformation> BookingInformations { get; set; }
        DbSet<BookingLimited> BookingLimiteds { get; set; }
        
        DbSet<BuildingConsumption> BuildingConsumption { get; set; }
        DbSet<DailyTotalConsumption> DailyTotalConsumption { get; set; }
    }
}
