using Volo.Abp.Application.Dtos;
using Yuannisha.AutomaticElectricitySystem.BuildingsEntity;
using Yuannisha.AutomaticElectricitySystem.BuildingsIAppservice;
using Yuannisha.AutomaticElectricitySystem.BuildingsShared;

namespace Yuannisha.AutomaticElectricitySystem.BuildingsAppservice
{
    /// <summary>
    /// 教学楼
    /// </summary>
    [Authorize]
    public class BuildingsAppService : ApplicationService, IBuildingsAppService
    {
        private readonly BuildingsManager _buildingsManager;

        public BuildingsAppService(BuildingsManager buildingsManager)
        {
            _buildingsManager = buildingsManager;
        }

        /// <summary>
        /// 分页查询教学楼
        /// </summary>
        /// <returns>PagedResultDto<PageBuildingsOutput></returns>
        [Authorize(AutomaticElectricitySystemPermissions.BuildingsManagement.Buildings.View)] 
        public async Task<PagedResultDto<BuildingsDto>> PageAsync(PageBuildingsInput input)
        {
            var result = await _buildingsManager.GetListAsync(input);
            // var result = new PagedResultDto<PageBuildingsOutput>();
            // var totalCount = await _buildingsManager.GetCountAsync();
            // result.TotalCount = totalCount;
            // if (totalCount <= 0) return result;
            // var list = await _buildingsManager.GetListAsync(input);
            // result.Items = ObjectMapper.Map<List<BuildingsDto>, List<PageBuildingsOutput>>(list);
            return new PagedResultDto<BuildingsDto>(result.TotalCount,result.BuildingsDto);
        }

        /// <summary>
        /// 创建教学楼
        /// </summary>
        [Authorize(AutomaticElectricitySystemPermissions.BuildingsManagement.Buildings.Create)]
        public Task CreateAsync(CreateBuildingsInput input)
        {
            return _buildingsManager.CreateAsync(
             GuidGenerator.Create(),
             input.Name,
             input.DisplayOrder
                    );
        }

        /// <summary>
        /// 编辑教学楼
        /// </summary>
        [Authorize(AutomaticElectricitySystemPermissions.BuildingsManagement.Buildings.Edit)]
        public Task UpdateAsync(UpdateBuildingsInput input)
        {
            return _buildingsManager.UpdateAsync(
             input.Id,
             input.Name,
             input.DisplayOrder
             );
        }

        /// <summary>
        /// 删除教学楼
        /// </summary>
        [Authorize(AutomaticElectricitySystemPermissions.BuildingsManagement.Buildings.Delete)]
        public Task DeleteAsync(DeleteBuildingsInput input)
        {
            return _buildingsManager.DeleteAsync(input.Id);
        }
        
        /// <summary>
        /// 根据Id查询教学楼
        /// </summary>
        /// <param name="buildingId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<GetBuildingOutPutDto> GetBuildingAsync(Guid buildingId)
        {
            return _buildingsManager.GetBuilding(buildingId);
        }

        public async Task<List<GetBuildingOutPutDto>> GetAllBuildingAsync()
        {
            return await _buildingsManager.GetAllBuildingsAsyncUnConditional();
        }
    }
}

