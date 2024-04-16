using Yuannisha.AutomaticElectricitySystem.DailyTotalConsumptionIAppservice;
using Yuannisha.AutomaticElectricitySystem.DailyTotalConsumptionShared;

namespace Yuannisha.AutomaticElectricitySystem.DailyTotalConsumptionsController;

[Route("DailyTotalConsumption")]
public class DailyTotalConsumptionController : AbpController,IDailyTotalConsumptionAppservice
{
    private readonly IDailyTotalConsumptionAppservice _dailyTotalConsumptionAppservice;

    public DailyTotalConsumptionController(
        IDailyTotalConsumptionAppservice dailyTotalConsumptionAppservice
    )
    {
        _dailyTotalConsumptionAppservice = dailyTotalConsumptionAppservice;
    }
    [HttpPost("GetAllDailyTotalConsumption")]
    [SwaggerOperation(summary: "获取所有的总消耗数据", Tags = new[] { "DailyTotalConsumption" })]
    public async Task<GetAllDailyTotalConsumptionOutput> GetAllDailyTotalConsumption()
    {
        return await _dailyTotalConsumptionAppservice.GetAllDailyTotalConsumption();
    }
    [HttpPost("UpdateDailyTotalConsumption")]
    [SwaggerOperation(summary: "更新一条总消耗数据", Tags = new[] { "DailyTotalConsumption" })]
    public async Task<AllDailyTotalConsumptionDto> UpdateDailyTotalConsumption(AllDailyTotalConsumptionDto input)
    {
        return await _dailyTotalConsumptionAppservice.UpdateDailyTotalConsumption(input);
    }

    [HttpPost("CreateDailyTotalConsumption")]
    [SwaggerOperation(summary: "创建一条总消耗数据", Tags = new[] { "DailyTotalConsumption" })]
    public async Task CreateDailyTotalConsumption(CreateDailyTotalConsumptionInput input)
    {
        await _dailyTotalConsumptionAppservice.CreateDailyTotalConsumption(input);
    }
    [HttpPost("DeleteDailyTotalConsumption")]
    [SwaggerOperation(summary: "删除一条总消耗数据", Tags = new[] { "DailyTotalConsumption" })]
    public async Task DeleteDailyTotalConsumption(Guid id)
    {
        await _dailyTotalConsumptionAppservice.DeleteDailyTotalConsumption(id);
    }
    [HttpPost("GetDailyTotalConsumptionWithLastSevenDays")]
    [SwaggerOperation(summary: "获得过去七天的总消耗数据", Tags = new[] { "DailyTotalConsumption" })]
    public async Task<GetDailyTotalConsumptionWithLastSevenDaysOutput> GetDailyTotalConsumptionWithLastSevenDays()
    {
        return await _dailyTotalConsumptionAppservice.GetDailyTotalConsumptionWithLastSevenDays();
    }
}