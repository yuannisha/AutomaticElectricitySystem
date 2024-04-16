using Yuannisha.AutomaticElectricitySystem.PowerConsumptionIAppservice;

namespace Yuannisha.AutomaticElectricitySystem.PowerConsumptionController;

[Route("BuildingConsumption")]
public class BuildingConsumptionsController : AbpController , IBuildingConsumptionAppservice
{
    private readonly IBuildingConsumptionAppservice _buildingConsumptionAppservice;

    public BuildingConsumptionsController(IBuildingConsumptionAppservice buildingConsumptionsController)
    {
        _buildingConsumptionAppservice = buildingConsumptionsController;
    }
    
    [HttpPost("GetAll")]
    [SwaggerOperation(summary: "获取所有楼栋消耗数据", Tags = new[] { "BuildingConsumption" })]
    public async Task<GetAllBuildingConsumptionOutput> GetAllBuildingConsumption()
    {
        return await _buildingConsumptionAppservice.GetAllBuildingConsumption();
    }

    [HttpPost("Create")]
    [SwaggerOperation(summary: "创建一条楼栋消耗数据", Tags = new[] { "BuildingConsumption" })]
    public async Task CreateBuildingConsumption(CreateBuildingConsumptionInput input)
    {
        await _buildingConsumptionAppservice.CreateBuildingConsumption(input);
    }
    [HttpPost("Update")]
    [SwaggerOperation(summary: "更新一条楼栋消耗数据", Tags = new[] { "BuildingConsumption" })]
    public async Task<BuildingConsumptionOutputDto> UpdateBuildingConsumption(UpdateBuildingConsumptionInput input)
    {
        return await _buildingConsumptionAppservice.UpdateBuildingConsumption(input);
    }

    [HttpPost("Delete")]
    [SwaggerOperation(summary: "删除一条楼栋消耗数据", Tags = new[] { "BuildingConsumption" })]
    public async Task DeleteBuildingConsumption(Guid Id)
    {
        await _buildingConsumptionAppservice.DeleteBuildingConsumption(Id);
    }
    [HttpPost("GetBuildingConsumptionRankOutput")]
    [SwaggerOperation(summary: "获取当天的楼栋消耗数据排名", Tags = new[] { "BuildingConsumption" })]
    public async Task<GetBuildingConsumptionRankOutput> GetBuildingConsumptionRank()
    {
        return await _buildingConsumptionAppservice.GetBuildingConsumptionRank();
    }
    [HttpPost("GetTodayDatasCount")]
    [SwaggerOperation(summary: "获取当天消耗数据概览", Tags = new[] { "BuildingConsumption" })]
    public async Task<GetDatasCountOutput> GetTodayDatasCount()
    {
        return await _buildingConsumptionAppservice.GetTodayDatasCount();
    }
}