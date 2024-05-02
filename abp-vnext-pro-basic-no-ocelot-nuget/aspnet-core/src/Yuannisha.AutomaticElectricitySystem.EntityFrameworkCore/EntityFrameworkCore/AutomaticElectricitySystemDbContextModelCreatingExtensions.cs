using Volo.Abp.EntityFrameworkCore.Modeling;
using Yuannisha.AutomaticElectricitySystem.BookingInformationsEntity;
using Yuannisha.AutomaticElectricitySystem.BookingLimitedsEntity;
using Yuannisha.AutomaticElectricitySystem.DailyTotalConsumptionEntity;
using Yuannisha.AutomaticElectricitySystem.PowerConsumption;
using Yuannisha.AutomaticElectricitySystem.PowerSwitchsEntity;
using Yuannisha.AutomaticElectricitySystem.RoomsEntity;

namespace Yuannisha.AutomaticElectricitySystem.EntityFrameworkCore
{
    public static class AutomaticElectricitySystemDbContextModelCreatingExtensions
    {
        public static void ConfigureAutomaticElectricitySystem(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<Rooms>(b =>
            {
                b.ToTable(AutomaticElectricitySystemDomainSharedConsts.DbTablePrefix + nameof(Rooms));
                b.Property(e => e.BuildingId);
                b.Property(e => e.Floor);
                b.Property(e => e.No);
                b.Property(e => e.IsUsingOrNot);
                b.Property(e => e.RoomType);
                b.Property(e => e.ControlType);
                b.ConfigureByConvention();
                b.HasOne(a => a.Building).WithMany(b => b.Rooms).
                    HasForeignKey(c => c.BuildingId).OnDelete(DeleteBehavior.Restrict);
            });
            builder.Entity<PowerSwitchs>(b =>
            {
                b.ToTable(AutomaticElectricitySystemDomainSharedConsts.DbTablePrefix + nameof(PowerSwitchs));
                b.Property(e => e.RoomId);
                b.Property(e => e.SerialNumber).IsRequired().HasMaxLength(14);
                b.Property(e => e.ControlledMachineName);
                b.Property(e => e.IsOnline);
                b.Property(e => e.Status);
                b.ConfigureByConvention();
                b.HasOne(a => a.Room).WithMany(b => b.PowerSwitches)
                    .HasForeignKey(c => c.RoomId).OnDelete(DeleteBehavior.Restrict);
            });
            builder.Entity<BuildingsEntity.Buildings>(b =>
            {
                b.ToTable(AutomaticElectricitySystemDomainSharedConsts.DbTablePrefix + nameof(BuildingsEntity.Buildings));
                b.Property(e => e.Name).IsRequired().HasMaxLength(50);
                b.Property(e => e.DisplayOrder);
                b.ConfigureByConvention();
                b.Property(x => x.Id).ValueGeneratedNever();
            });
            builder.Entity<BookingInformation>(b =>
            {
                b.ToTable(AutomaticElectricitySystemDomainSharedConsts.DbTablePrefix + nameof(BookingInformation));
                b.Property(e => e.StudentId).IsRequired().HasMaxLength(15);
                b.Property(e => e.StudentName).IsRequired().HasMaxLength(50);
                b.Property(e => e.StudentClass).IsRequired().HasMaxLength(50);
                b.Property(e => e.TelephoneNumber).IsRequired().HasMaxLength(11);
                b.Property(e => e.UsingClassroom).IsRequired().HasMaxLength(50);
                b.Property(e => e.UsingPurpose).IsRequired().HasMaxLength(50);
                b.Property(e => e.BookingTimespan).IsRequired().HasMaxLength(80);
                b.ConfigureByConvention();
            });
            builder.Entity<BookingLimited>(b =>
            {
                b.ToTable(AutomaticElectricitySystemDomainSharedConsts.DbTablePrefix + nameof(BookingLimited));
                b.Property(e => e.StudentId).IsRequired().HasMaxLength(15);
                b.Property(e => e.StudentName).IsRequired().HasMaxLength(50);
                b.Property(e => e.Date).IsRequired().HasMaxLength(50);
                b.Property(e => e.BookedHours);
                b.ConfigureByConvention();
            });
            builder.Entity<BuildingConsumption>(b =>
            {
                b.ToTable(AutomaticElectricitySystemDomainSharedConsts.DbTablePrefix + nameof(BuildingConsumption));
                b.Property(e => e.BuildingId).IsRequired();
                b.Property(e => e.Date).IsRequired().HasMaxLength(50);
                b.Property(e => e.PowerConsumption).IsRequired();
                b.ConfigureByConvention();
            });
            builder.Entity<DailyTotalConsumption>(b =>
                {
                    b.ToTable(AutomaticElectricitySystemDomainSharedConsts.DbTablePrefix + nameof(DailyTotalConsumption));
                    b.Property(e => e.Date).IsRequired().HasMaxLength(50);
                    b.Property(e => e.PowerConsumption).IsRequired();
                    b.ConfigureByConvention();
                }
                );
        }
    }
}