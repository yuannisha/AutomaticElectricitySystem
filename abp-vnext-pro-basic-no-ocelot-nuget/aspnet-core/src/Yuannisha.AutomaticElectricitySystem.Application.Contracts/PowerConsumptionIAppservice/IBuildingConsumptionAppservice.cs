namespace Yuannisha.AutomaticElectricitySystem.PowerConsumptionIAppservice;

/// <summary>
/// 楼栋消耗总计
/// </summary>
public interface IBuildingConsumptionAppservice
{
    /// <summary>
    /// 获取所有楼栋消耗数据                                                                                     
    /// </summary>
    Task<GetAllBuildingConsumptionOutput> GetAllBuildingConsumption();
    
    /// <summary>
    /// 创建楼栋消耗数据                                                                                     
    /// </summary>
    Task CreateBuildingConsumption(CreateBuildingConsumptionInput input);
    
    /// <summary>
    /// 更新楼栋消耗数据                                                                                     
    /// </summary>
    Task<BuildingConsumptionOutputDto> UpdateBuildingConsumption(UpdateBuildingConsumptionInput input);
    
    /// <summary>
    /// 删除楼栋消耗数据                                                                                     
    /// </summary>
    Task DeleteBuildingConsumption(Guid Id);
    
    /// <summary>
    /// 获取当日楼栋消耗排名数据                                                                                     
    /// </summary>
    Task<GetBuildingConsumptionRankOutput> GetBuildingConsumptionRank();
    
    /// <summary>
    /// 获取当日数据概览                                                                                  
    /// </summary>
    Task<GetDatasCountOutput> GetTodayDatasCount();
}