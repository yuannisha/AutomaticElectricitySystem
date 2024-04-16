using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Linq;
using Volo.Abp.ObjectMapping;
using Yuannisha.AutomaticElectricitySystem.BookingInformationsAppservice;
using Yuannisha.AutomaticElectricitySystem.BuildingsEntity;
using Yuannisha.AutomaticElectricitySystem.DailyTotalConsumptionEntity;
using Yuannisha.AutomaticElectricitySystem.PowerConsumption;
using Yuannisha.AutomaticElectricitySystem.PowerConsumptionIAppservice;

namespace Yuannisha.AutomaticElectricitySystem.BuildingConsumptionsAppservice;

public class BuildingConsumptionAppservice : IBuildingConsumptionAppservice
{
    private readonly IRepository<BuildingConsumption, Guid> _BuildingConsumptionRepository;
    // private readonly IRepository<BuildingConsumption,Guid> _repositoryOfBuildingConsumption;
    private readonly IObjectMapper _objectMapper;
    private readonly IGuidGenerator _guidGenerator;
    private readonly BuildingsManager _buildingsManager;
    private readonly IAsyncQueryableExecuter _asyncQueryableExecuter;
    // private readonly ConsumptionAmountAppservice _consumptionAmountAppservice;
    private readonly BookingInformationAppService _bookingInformationsAppservice;
    private readonly DailyTotalConsumptionManager _dailyTotalConsumptionManager;
    
    public BuildingConsumptionAppservice(IObjectMapper objectMapper,
        // IRepository<BuildingConsumption,Guid> repositoryOfBuildingConsumption,
        IRepository<BuildingConsumption, Guid> BuildingConsumptionRepository,
        IGuidGenerator guidGenerator,
        BuildingsManager buildingsManager,
        IAsyncQueryableExecuter asyncQueryableExecuter,
        // ConsumptionAmountAppservice consumptionAmountAppservice,
        BookingInformationAppService bookingInformationsAppservice,
        DailyTotalConsumptionManager dailyTotalConsumptionManager
        )
    {
        _objectMapper = objectMapper;
        // _repositoryOfBuildingConsumption = repositoryOfBuildingConsumption;
        _BuildingConsumptionRepository = BuildingConsumptionRepository;
        _guidGenerator = guidGenerator;
        _buildingsManager = buildingsManager;
        _asyncQueryableExecuter = asyncQueryableExecuter;
        // _consumptionAmountAppservice = consumptionAmountAppservice;
        _bookingInformationsAppservice = bookingInformationsAppservice;
        _dailyTotalConsumptionManager = dailyTotalConsumptionManager;
    }
    public async Task<GetAllBuildingConsumptionOutput> GetAllBuildingConsumption()
    {
        var list = await _BuildingConsumptionRepository.GetListAsync();
        var res = _objectMapper.Map<List<BuildingConsumption>, List<BuildingConsumptionOutputDto>>(list);
        return new GetAllBuildingConsumptionOutput() { BuildingConsumptionOutputDto = res };
    }

    public async Task CreateBuildingConsumption(CreateBuildingConsumptionInput input)
    {
        await _BuildingConsumptionRepository.InsertAsync(new BuildingConsumption(_guidGenerator.Create(),input
            .BuildingId, input.Date,input.PowerConsumption));
    }

    public async Task<BuildingConsumptionOutputDto> UpdateBuildingConsumption(UpdateBuildingConsumptionInput input)
    {
        // var queryable = await _BuildingConsumptionRepository.GetQueryableAsync();
        // var query = queryable.WhereIf(input.BuildingId!=Guid.Empty,x=>x.BuildingId.Equals(input.BuildingId));
        // var items = await _asyncQueryableExecuter.ToListAsync(query.OrderByDescending(b=>b.Date));
        // var entity = await _repositoryOfBuildingConsumption.GetAsync()
        var entity = await _BuildingConsumptionRepository.FindAsync(input.Id) ?? throw new UserFriendlyException
            ($"楼栋消耗数据不存在");
        entity.Update(input.Id,input.BuildingId,input.Date,input.PowerConsumption);
        entity = await _BuildingConsumptionRepository.UpdateAsync(entity);
        return  _objectMapper.Map<BuildingConsumption,BuildingConsumptionOutputDto>(entity);
    }

    public async Task DeleteBuildingConsumption(Guid Id)
    {
        var entity = await _BuildingConsumptionRepository.FindAsync(Id) ?? throw new UserFriendlyException
            ($"楼栋消耗数据不存在");
        await _BuildingConsumptionRepository.DeleteAsync(entity);
    }

    public async Task<GetBuildingConsumptionRankOutput> GetBuildingConsumptionRank()
    {
        var buildings = await _buildingsManager.GetAllBuildingsAsyncUnConditional();
        var queryable =await _BuildingConsumptionRepository.GetQueryableAsync();
        var currentDate = DateTime.Now.ToString("yyyy-MM-dd");
        var query = queryable.WhereIf(true, 
            x => x.Date.Equals(currentDate));
        var items = await _asyncQueryableExecuter.ToListAsync(
            query
            .OrderByDescending(b => b.PowerConsumption));
        var res = _objectMapper.Map<List<BuildingConsumption>, 
            List<GetBuildingConsumptionRankOutput.BuildingConsumptionRankOutputDto>>(items);
        // buildings.ForEach(building =>
        // {
        //     res.ForEach(x =>
        //     {
        //         if (building.Id.Equals(x.BuildingId))
        //         {
        //             x.BuildingName = building.Name;
        //         }
        //     });
        // });
        foreach (var ttt in res)
        {
            var building = buildings.FirstOrDefault(b=>b.Id.Equals(ttt.BuildingId));
            ttt.BuildingName = building.Name;
        }
        return new GetBuildingConsumptionRankOutput() { BuildingConsumptionRankOutput = res };
    }

    public async Task<GetDatasCountOutput> GetTodayDatasCount()
    {
        var currentDate1 = DateTime.Now.ToString("yyyy/M/d");
        var currentDate2 = DateTime.Now.ToString("M/d/yyyy");
    
        var bookingInfor =await _bookingInformationsAppservice.GetAllInformation();
        var todayStudentsOfBooking = 0;
        var totalStudentsOfBooking = 0;
        var todayRoomsOfBooking = 0;
        var totalRoomsOfBooking = 0;
        
        // 获取今天的预约信息，并去重（基于 StudentName 和 BookingTimespan）
        var uniqueBookingsToday = bookingInfor
            .Where(x => x.BookingTimespan.Contains(currentDate1) || x.BookingTimespan.Contains(currentDate2))
            .GroupBy(x => new { x.StudentName, x.BookingTimespan })
            .Select(g => g.First())
            .ToList();
        // 获取今天使用教室的总数量
        todayRoomsOfBooking = uniqueBookingsToday
            .Select(x => x.UsingClassroom)
            .Distinct()
            .Count();
        // 获取今天所有学生名字的总数量
        todayStudentsOfBooking = uniqueBookingsToday
            .Select(x => x.StudentName)
            .Distinct()
            .Count();
        // 获取去重后的预约信息（基于 StudentName 和 BookingTimespan）
        var uniqueBookings = bookingInfor
            .GroupBy(x => new { x.StudentName, x.BookingTimespan })
            .Select(g => g.First())
            .ToList();
    
        // 获取使用教室的总数量
        totalRoomsOfBooking = uniqueBookings
            .Select(x => x.UsingClassroom)
            .Distinct()
            .Count();
    
        // 获取所有学生名字的总数量
        totalStudentsOfBooking = uniqueBookings
            .Select(x => x.StudentName)
            .Distinct()
            .Count();

        var currentdate3 = DateTime.Now.ToString("yyyy/MM/dd");
        var totalConsumption =await _dailyTotalConsumptionManager.GetAllDailyTotalConsumption();
        var singledata = totalConsumption.AllDailyTotalConsumptionOutput.FirstOrDefault(x=>x.Date.Equals(currentdate3));
        double totaldata = 0;
        totalConsumption.AllDailyTotalConsumptionOutput.ForEach(x =>
        {
            totaldata += x.PowerConsumption;
        });
        return new GetDatasCountOutput()
        {
            // TodayEnergyConsumption = todayConsumption, TotalEnergyConsumption =
            //     totalConsumption,
            TodayRoomsBeBooked = todayRoomsOfBooking, TodayStudentsOfBooking =
                todayStudentsOfBooking,
            TotalRoomsBeBooked = totalRoomsOfBooking, TotalStudentsOfBooking = totalStudentsOfBooking,
            TodayConsumption = singledata.PowerConsumption,
            TotalConsumption = totaldata
        };
    }
}