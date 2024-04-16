using Yuannisha.AutomaticElectricitySystem.BookingInformationsEntity;
using Yuannisha.AutomaticElectricitySystem.BookingInformationsIAppservice;
using Yuannisha.AutomaticElectricitySystem.BookingInformationsShared;
using Yuannisha.AutomaticElectricitySystem.BookingLimitedsEntity;
using Yuannisha.AutomaticElectricitySystem.BookingLimitedsIAppservice;
using Yuannisha.AutomaticElectricitySystem.BookingLimitedsShared;
using Yuannisha.AutomaticElectricitySystem.BuildingsEntity;
using Yuannisha.AutomaticElectricitySystem.BuildingsIAppservice;
using Yuannisha.AutomaticElectricitySystem.BuildingsShared;
using Yuannisha.AutomaticElectricitySystem.DailyTotalConsumptionEntity;
using Yuannisha.AutomaticElectricitySystem.DailyTotalConsumptionShared;
using Yuannisha.AutomaticElectricitySystem.PowerConsumption;
using Yuannisha.AutomaticElectricitySystem.PowerConsumptionIAppservice;
using Yuannisha.AutomaticElectricitySystem.PowerSwitchsEntity;
using Yuannisha.AutomaticElectricitySystem.PowerSwitchsIAppservice;
using Yuannisha.AutomaticElectricitySystem.PowerSwitchsShared;
using Yuannisha.AutomaticElectricitySystem.RoomsEntity;
using Yuannisha.AutomaticElectricitySystem.RoomsIAppservice;
using Yuannisha.AutomaticElectricitySystem.RoomsShared;

namespace Yuannisha.AutomaticElectricitySystem
{
    public class AutomaticElectricitySystemDomainAutoMapperProfile : Profile
    {
        public AutomaticElectricitySystemDomainAutoMapperProfile()
        {
            // CreateMap<RoomList, PageRoomsOutput>();
            CreateMap<PowerSwitchsDto, PagePowerSwitchsOutput>();
            CreateMap<BuildingsDto, PageBuildingsOutput>();
            CreateMap<BookingInformationDto, PageBookingInformationOutput>();
            CreateMap<BookingLimitedDto, PageBookingLimitedOutput>();
            CreateMap<ClassroomBookingEditDto, BookingInformation>();
            CreateMap<BookingInformationDto, BookingInforDto>();
            CreateMap<ClassroomBookingEditDto, BookingLimitedEditDto>();
            CreateMap<BookingInWeekendInputDto, ClassroomBookingEditDto>();
            CreateMap<BookingInWeekendInputDto, BookingLimitedEditDto>();
            CreateMap<BookingInformation, ClassroomBookingEditDto>();
            CreateMap<BookingLimitedEditDto, BookingLimited>();
            CreateMap<Rooms, RoomList>();
            CreateMap<RoomList, PageRoomsOutput>();
            CreateMap<Buildings, GetBuildingOutPutDto>();
            CreateMap<Rooms, GetAllClassroomsOutPutDto>();
            CreateMap<PowerSwitchs, PowerSwitchsDto>();
            CreateMap<Rooms, RoomsDto>();
            CreateMap<BookingLimited, BookingLimitedDto>();
            CreateMap<Buildings, BuildingsDto>();
            CreateMap<BookingInformation, BookingInformationDto>();
            CreateMap<BuildingConsumption, BuildingConsumptionOutputDto>();
            CreateMap<BuildingConsumption, GetBuildingConsumptionRankOutput.BuildingConsumptionRankOutputDto>();
            CreateMap<DailyTotalConsumption, AllDailyTotalConsumptionDto>();
        }
    }
}

