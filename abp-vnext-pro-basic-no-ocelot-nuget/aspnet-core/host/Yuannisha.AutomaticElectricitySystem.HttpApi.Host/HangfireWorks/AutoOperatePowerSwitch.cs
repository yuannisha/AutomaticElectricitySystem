using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Uow;
using Yuannisha.AutomaticElectricitySystem.BookingInformationsAppservice;
using Yuannisha.AutomaticElectricitySystem.BuildingConsumptionsAppservice;
using Yuannisha.AutomaticElectricitySystem.BuildingsEntity;
using Yuannisha.AutomaticElectricitySystem.DailyTotalConsumptionEntity;
using Yuannisha.AutomaticElectricitySystem.DailyTotalConsumptionShared;
using Yuannisha.AutomaticElectricitySystem.DeviceManagement;
using Yuannisha.AutomaticElectricitySystem.PowerConsumption;
using Yuannisha.AutomaticElectricitySystem.PowerConsumptionIAppservice;
using Yuannisha.AutomaticElectricitySystem.PowerSwitchsEntity;
using Yuannisha.AutomaticElectricitySystem.PowerSwitchsShared;
using Yuannisha.AutomaticElectricitySystem.RoomsEntity;
using Yuannisha.AutomaticElectricitySystem.SchoolClassTable;
using ITransientDependency = Volo.Abp.DependencyInjection.ITransientDependency;

namespace Yuannisha.AutomaticElectricitySystem.HangfireWorks;

public class AutoOperatePowerSwitch : ITransientDependency
{
    //TCP_serviceManagement tcp_Service = new TCP_serviceManagement();

    //DatasHandle datasHandle = new DatasHandle();

    private readonly RoomsManager _roommManager;
    private readonly PowerSwitchsManager _powerSwitchsManager;
    private readonly BuildingsManager _buildingsManager;
    private readonly Volo.Abp.Domain.Repositories.IRepository<Rooms, Guid> _roomRepository;
    private readonly IGuidGenerator _guidGenerator;
    private readonly BuildingConsumptionAppservice _buildingConsumptionAppservice;
    // private readonly ConsumptionAmountAppservice _consumptionAmountAppservice;
    private readonly DailyTotalConsumptionManager _dailyTotalConsumptionManager;
    private readonly Volo.Abp.Domain.Repositories.IRepository<BuildingConsumption, Guid> _repositoryOfBuildingConsumption;


    public AutoOperatePowerSwitch(/*IRepository<PowerSwitch, Guid> powerSwitchRepository,*/
        RoomsManager _roommManager, Volo.Abp.Domain.Repositories.IRepository<Rooms, Guid> roomRepository,
        PowerSwitchsManager powerSwitchsManager,
        BuildingsManager buildingsManager,
        IGuidGenerator guidGenerator,
        BuildingConsumptionAppservice buildingConsumptionAppservice,
        Volo.Abp.Domain.Repositories.IRepository<BuildingConsumption, Guid> repositoryOfBuildingConsumption,
        // ConsumptionAmountAppservice consumptionAmountAppservice
        DailyTotalConsumptionManager dailyTotalConsumptionManager
        )
    {
        //_powerSwitchRepository = powerSwitchRepository;
        _roommManager = _roommManager;
        _roomRepository = roomRepository;
        _powerSwitchsManager = powerSwitchsManager;
        _buildingsManager = buildingsManager;
        _guidGenerator = guidGenerator;
        _buildingConsumptionAppservice = buildingConsumptionAppservice;
        _repositoryOfBuildingConsumption = repositoryOfBuildingConsumption;
        _dailyTotalConsumptionManager = dailyTotalConsumptionManager;
        // _consumptionAmountAppservice = consumptionAmountAppservice;
    }

    public AutoOperatePowerSwitch()
    {
    }

    public void OperateDeviceTest()
    {
        string TempString = "";
        List<string> OnlineDevice = TCP_serviceManagement.OnlineDeviceList();
        if (!OnlineDevice.Count.Equals(0))
        {
            foreach (var item in OnlineDevice)
            {
                TempString = TCP_serviceManagement.AddSpaceInSeriesNum(item);
                var DeviceState = TCP_serviceManagement.GetDeviceStatus(TempString);
                if (DeviceState.Equals(0))
                    TCP_serviceManagement.DSNwithInfor[TempString].OpenSwitch(TempString);
                else if (DeviceState.Equals(1))
                    TCP_serviceManagement.DSNwithInfor[TempString].CloseSwitch(TempString);
            }
        }
    }

    [UnitOfWork]
    public virtual async void AutoOperateDeviceByTime()
    {
        var ss =await _roomRepository.WithDetailsAsync(x => x.PowerSwitches);
        var roomList = ss.ToList();
        foreach (var classRoom in roomList)
        {
            var OnlineDevice = TCP_serviceManagement.OnlineDeviceList();

            var TimeSpan = DatasHandle.JudgeTime(classRoom.No);

            //classRoom.PowerSwitches.ToList();
            foreach (var ps in classRoom.PowerSwitches)
            {
                var BeforeNum = ps.SerialNumber;
                string psNumChanged = TCP_serviceManagement.AddSpaceInSeriesNum(ps.SerialNumber);
                if (TimeSpan.Equals(0))
                {
                    if (OnlineDevice.Contains(ps.SerialNumber))
                    {
                        var DeviceState = TCP_serviceManagement.GetDeviceStatus(psNumChanged);
                        if (DeviceState.Equals(0))
                        {
                            continue;
                        }
                        else if (DeviceState.Equals(1))
                        {
                            TCP_serviceManagement.DSNwithInfor[psNumChanged].CloseSwitch(psNumChanged);

                        }
                    }
                    else//设备未在线
                    {
                        //System.Diagnostics.Trace.WriteLine($"此设备{ps.SerialNumber}未上线");
                        continue;
                    }
                }
                else if (TimeSpan.Equals(1))
                {
                    if (OnlineDevice.Contains(ps.SerialNumber))
                    {
                        var DeviceState = TCP_serviceManagement.GetDeviceStatus(psNumChanged);
                        if (DeviceState.Equals(0))
                        {
                            TCP_serviceManagement.DSNwithInfor[psNumChanged].OpenSwitch(psNumChanged);
                        }
                        else if (DeviceState.Equals(1))
                        {
                            continue;
                        }
                    }
                    else//设备未在线
                    {
                        //System.Diagnostics.Trace.WriteLine($"此设备{ps.SerialNumber}未上线");
                        continue;
                    }
                }
            }
        }
    }

    [UnitOfWork]
    public virtual async void AutoOperateByTime()
    {
        var ss = await _roomRepository.WithDetailsAsync(x => x.PowerSwitches);
        var roomlist = ss.ToList();
        //var roomlist = _roomRepository.GetAllList();
        var onlineDevices = TCP_serviceManagement.OnlineDeviceList();

        foreach (var room in roomlist)
        {
            Console.WriteLine($"这是教室{room.No}的测试");
            var timespan = DatasHandle.JudgeTime(room.No);
            foreach (var ps in room.PowerSwitches)
            {
                var serNum = TCP_serviceManagement.AddSpaceInSeriesNum(ps.SerialNumber);
                if (timespan.Equals(0))
                {
                    Console.WriteLine($"教室{room.No}没有在上课阶段!------+{DateTime.Now.ToString()}");
                    if (onlineDevices.Contains(ps.SerialNumber))
                    {
                        var state = TCP_serviceManagement.GetDeviceStatus(serNum);
                        if (state.Equals(0))
                        {
                            Console.WriteLine($"教室{room.No}的控电设备{ps.SerialNumber}是正常的，不需要操作！---{DateTime.Now.ToString()}");
                            continue;  
                        }
                        else if (state.Equals(1))
                        {
                            TCP_serviceManagement.DSNwithInfor[serNum].OpenSwitch(serNum);
                            Console.WriteLine($"教室{room.No}的控电设备{ps.SerialNumber}已打开(已关闭电灯电源)！---{DateTime.Now.ToString()}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"教室 {room.No} 的控电设备{ps.SerialNumber}不在线---{DateTime.Now.ToString()}");
                        continue;
                    }
                }
                else if (timespan.Equals(1))
                {
                    Console.WriteLine($"教室{room.No}在上课阶段!----{DateTime.Now.ToString()}");
                    if (onlineDevices.Contains(ps.SerialNumber))
                    {
                        var DeviceState = TCP_serviceManagement.GetDeviceStatus(serNum);
                        if (DeviceState.Equals(0))
                        {
                            TCP_serviceManagement.DSNwithInfor[serNum].CloseSwitch(serNum);
                            Console.WriteLine($"教室{room.No}的控电设备{ps.SerialNumber}已关闭(已打开电灯电源)！---{DateTime.Now.ToString()}");                                          
                        }
                        else if (DeviceState.Equals(1))
                        {
                            Console.WriteLine($"教室{room.No}的控电设备{ps.SerialNumber}是正常的，不需要操作！----{DateTime.Now.ToString()}");
                            continue;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"教室 {room.No} 的控电设备{ps.SerialNumber}不在线----{DateTime.Now.ToString()}");
                        continue;
                    }
                }
            }
        }
    }


    //[Obsolete("该方法显示被弃用了，但仍可以使用")]
    public void RegistryTask()
    {
        RecurringJob.RemoveIfExists("test");
        RecurringJob.RemoveIfExists("InitTimeSpans");
        RecurringJob.RemoveIfExists("AutoFuncTest");
        RecurringJob.RemoveIfExists("UpdateClassInformation");
        RecurringJob.RemoveIfExists("AutoOperateByTime");

        //var tz = TZConvert.GetTimeZoneInfo("Asia/Shanghai");
        //var roomRepository = IocManager.Instance.Resolve<IRepository<Basics.Room, Guid>>();
        //sss. StartSocketService();
        //RecurringJob.AddOrUpdate("test", () => sss.CheckTimeSpan( ), Cron.Minutely());


        TCP_serviceManagement.benginService();//项目启动时开启Socket服务
        // var aops = IocManager.Instance.Resolve<AutoOperatePowerSwitch>();


        DatasHandle.GetInformationOfClasses();//项目开启时获取系统数据库中的课表信息

        BookingInformationAppService.InitPianoRoomsBookingTimespan();

        //RecurringJob.AddOrUpdate("UpdateClassInformation", () => DatasHandle.GetInformationOfClasses(), Cron.Daily(0),TimeZoneInfo.Local);//每天更新课表信息(可能源课表数据库的数据有更改)
        //RecurringJob.AddOrUpdate("AutoFuncTest", () => aops.AutoOperateDeviceByTime(), Cron.Hourly,TimeZoneInfo.Local,"default");//自动每隔一分钟检测教室是否在上课时间从而控制智能空开
        //RecurringJob.AddOrUpdate("test", () => OperateDeviceTest(), Cron.MinuteInterval(3));
        RecurringJob.AddOrUpdate("test", () => Console.WriteLine("测试"), Cron.Minutely());
        //RecurringJob.AddOrUpdate("InitTimeSpans", () => ClassroomBookingAppService.InitPianoRoomsBookingTimespan(),Cron.Daily(0),TimeZoneInfo.Local);
        //RecurringJob.AddOrUpdate<AutoOperatePowerSwitch>("AutoOperateByTime", x => x.AutoOperateByTime(), Cron.MinuteInterval(10), TimeZoneInfo.Local);
        
    
    }

    public void AutoTleMetering()
    {
        // foreach (var powerSwitch in TCP_serviceManagement.PowerSwitchs.PowerSwitchsDto)
        // {
        //     TCP_serviceManagement.DSNwithInfor[powerSwitch.SerialNumber].Telemetering(powerSwitch.SerialNumber);
        // }
        var keys = TCP_serviceManagement.DSNwithInfor.Keys.ToList();
        foreach (var key in keys)
        {
            TCP_serviceManagement.DSNwithInfor[key].Telemetering(key);
        }
        
    }

    public async void AutoSetValueWithConsumption()
    {
        var rooms = await _roommManager.GetAllClassrooms();
        var buildings = await _buildingsManager.GetAllBuildingsAsyncUnConditional();
        Dictionary<Guid, List<PowerSwitchsDto>> pwInbBuilding = new Dictionary<Guid, List<PowerSwitchsDto>>();
        Dictionary<Guid, buildingAndPws> pwInRooms = new Dictionary<Guid, buildingAndPws>();
        Dictionary<Guid, double> buildingConsumption = new Dictionary<Guid, double>();
        foreach (var pw in TCP_serviceManagement.PowerSwitchs)
        {
            foreach (var room in rooms)
            {
                if (pw.RoomId.Equals(room.Id))
                {
                    if (!pwInRooms.Keys.Contains(room.Id))
                    {
                        var list = new List<PowerSwitchsDto>() { pw };
                        pwInRooms.Add(room.Id,new buildingAndPws(){BuildingId = room.BuildingId,PowerSwitchsDtoList =
                            list});
                    }
                    else if(pwInRooms.Keys.Contains(room.Id))
                    {
                        pwInRooms[room.Id].PowerSwitchsDtoList.Add(pw);
                    }
                }
            }
        }

        foreach (var item in pwInRooms)
        {
            foreach (var building in buildings)
            {
                if (item.Value.BuildingId.Equals(building.Id))
                {
                    if (!pwInbBuilding.Keys.Contains(building.Id))
                    {
                        pwInbBuilding.Add(building.Id,item.Value.PowerSwitchsDtoList);
                    }
                    else if(pwInbBuilding.Keys.Contains(building.Id))
                    {
                        foreach (var powerSwitchDto in item.Value.PowerSwitchsDtoList)
                        {
                            pwInbBuilding[building.Id].Add(powerSwitchDto);
                        }
                    }
                }
            }
        }
        
        foreach (var item in pwInbBuilding)
        {
            double sum = 0;
            foreach (var pw in item.Value)
            {
                sum += pw.EnergyConsumption;
            }
            buildingConsumption.Add(item.Key,sum);
        }

        var ttt =await _buildingConsumptionAppservice.GetAllBuildingConsumption();
        foreach (var BC in buildingConsumption)
        {
            var isExist = await _repositoryOfBuildingConsumption.AnyAsync(b => b.BuildingId == BC.Key && b.Date
                .Equals(DateTime.Now.ToString("yyyy-MM-dd")));
            if (!isExist)
            {
                await _buildingConsumptionAppservice.CreateBuildingConsumption(new CreateBuildingConsumptionInput()
                    { BuildingId = BC.Key, Date = DateTime.Now.ToString("yyyy-MM-dd") , PowerConsumption = BC.Value});
            }
            else
            {
                Guid guid = Guid.Empty;
                foreach (var buildingConsumptionOutputDto in ttt.BuildingConsumptionOutputDto)
                {
                    if (buildingConsumptionOutputDto.BuildingId.Equals(BC.Key))
                    {
                        guid = buildingConsumptionOutputDto.Id;
                    }
                }
                await _buildingConsumptionAppservice.UpdateBuildingConsumption(new UpdateBuildingConsumptionInput()
                {
                    Id = guid, BuildingId = BC.Key, Date = DateTime.Now.ToString("yyyy-MM-dd"),
                    PowerConsumption = BC.Value
                });
            }
        }

        SetValuesForConsumptionAmount(buildingConsumption);
    }

    public void ResetValuesInPw()
    {
        foreach (var powerSwitchsDto in TCP_serviceManagement.PowerSwitchs)
        {
            powerSwitchsDto.EnergyConsumption = 0;
        }
    }

    public async void SetValuesForPowerSwitchs()
    {
        TCP_serviceManagement.PowerSwitchs = await _powerSwitchsManager.GetAllPowerSwitchs();
    }

    public async void SetValuesForConsumptionAmount(Dictionary<Guid, double> buildingConsumption)
    {
        double sum = 0;
        foreach (var item in buildingConsumption)
        {
            sum += item.Value;
        }

        var ttt = await _dailyTotalConsumptionManager.GetAllDailyTotalConsumption();
        foreach (var consumptionAmountOutputDto in ttt.AllDailyTotalConsumptionOutput)
        {
            if (consumptionAmountOutputDto.Date.Equals(DateTime.Now.ToString("yyyy-MM-dd")))
            {
                await _dailyTotalConsumptionManager.UpdateDailyTotalConsumption(new AllDailyTotalConsumptionDto(){Id = 
                    consumptionAmountOutputDto.Id,Date = DateTime.Now.ToString("yyyy-MM-dd"),PowerConsumption = sum});
            }
            else
            {
                await _dailyTotalConsumptionManager.CreateDailyTotalConsumption(new CreateDailyTotalConsumptionInput()
                {
                    Date =
                        DateTime.Now.ToString("yyyy-MM-dd"),
                    PowerConsumption = sum
                });
            }
        }
    }
    
    public void Test()
    {
        Console.WriteLine("这是一次测试！");
    }
    
    private class buildingAndPws
    {
        public Guid BuildingId { get; set; }
        public List<PowerSwitchsDto> PowerSwitchsDtoList { get; set; }
    }
}