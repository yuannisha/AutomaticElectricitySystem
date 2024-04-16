using Volo.Abp.Application.Dtos;
using Yuannisha.AutomaticElectricitySystem.BuildingsShared;

namespace Yuannisha.AutomaticElectricitySystem.BuildingsIAppservice
{
    /// <summary>
    /// 教学楼
    /// </summary>
    public interface IBuildingsAppService : IApplicationService
    {
        /// <summary>
        /// 分页查询教学楼
        /// </summary>
        Task<PagedResultDto<BuildingsDto>> PageAsync(PageBuildingsInput input);

        /// <summary>
        /// 创建教学楼
        /// </summary>    
        Task CreateAsync(CreateBuildingsInput input);

        /// <summary>
        /// 编辑教学楼
        /// </summary>
        Task UpdateAsync(UpdateBuildingsInput input);

        /// <summary>
        /// 删除教学楼
        /// </summary>
        Task DeleteAsync(DeleteBuildingsInput input);
        
        /// <summary>
        /// 根据Id查询教学楼
        /// </summary>
        Task<GetBuildingOutPutDto> GetBuildingAsync(Guid buildingId);
        
        /// <summary>
        /// 无条件获取全部教学楼
        /// </summary>
        Task<List<GetBuildingOutPutDto>> GetAllBuildingAsync();
    }
}


