using AutoMapper;
using Yuannisha.AutomaticElectricitySystem.BookingInformationsEntity;
using Yuannisha.AutomaticElectricitySystem.BookingInformationsShared;
using Yuannisha.AutomaticElectricitySystem.BookingLimitedsEntity;
using Yuannisha.AutomaticElectricitySystem.BookingLimitedsShared;
using Yuannisha.AutomaticElectricitySystem.BuildingsEntity;
using Yuannisha.AutomaticElectricitySystem.BuildingsShared;
using Yuannisha.AutomaticElectricitySystem.DailyTotalConsumptionEntity;
using Yuannisha.AutomaticElectricitySystem.DailyTotalConsumptionShared;
using Yuannisha.AutomaticElectricitySystem.PowerSwitchsEntity;
using Yuannisha.AutomaticElectricitySystem.PowerSwitchsShared;
using Yuannisha.AutomaticElectricitySystem.RoomsEntity;
using Yuannisha.AutomaticElectricitySystem.RoomsShared;

namespace Yuannisha.AutomaticElectricitySystem
{
    public class AutomaticElectricitySystemDomainAutoMapperProfile : Profile
    {
        public AutomaticElectricitySystemDomainAutoMapperProfile()
        {
            CreateMap<Rooms, RoomsDto>();
            CreateMap<PowerSwitchs, PowerSwitchsDto>();
            CreateMap<Buildings, BuildingsDto>();
            CreateMap<BookingInformation, BookingInformationDto>();
            CreateMap<BookingLimited, BookingLimitedDto>();
            CreateMap<DailyTotalConsumption, AllDailyTotalConsumptionDto>();
        }
    }
}

