using Volo.Abp.Application.Dtos;
using Yuannisha.AutomaticElectricitySystem.BuildingsIAppservice;
using Yuannisha.AutomaticElectricitySystem.BuildingsShared;

namespace Yuannisha.AutomaticElectricitySystem.BuildingsController;

[Route("Buildings")]
public class BuildingsController : AbpController, IBuildingsAppService
{
    private readonly IBuildingsAppService _buildingsAppService;

    public BuildingsController(IBuildingsAppService buildingsAppService)
    {
        _buildingsAppService = buildingsAppService;
    }

    [HttpPost("Page")]
    [SwaggerOperation(summary: "分页查询教学楼", Tags = new[] { "Buildings" })]     
    public async Task<PagedResultDto<BuildingsDto>> PageAsync(PageBuildingsInput input)
    {
        return await _buildingsAppService.PageAsync(input);
    }  

    [HttpPost("Create")]
    [SwaggerOperation(summary: "创建教学楼", Tags = new[] { "Buildings" })]        
    public async Task CreateAsync(CreateBuildingsInput input)
    {
        await _buildingsAppService.CreateAsync(input);
    }

    [HttpPost("Update")]
    [SwaggerOperation(summary: "编辑教学楼", Tags = new[] { "Buildings" })]         
    public async Task UpdateAsync(UpdateBuildingsInput input)
    {
        await _buildingsAppService.UpdateAsync(input);
    }

    [HttpPost("Delete")]
    [SwaggerOperation(summary: "删除教学楼", Tags = new[] { "Buildings" })]         
    public async Task DeleteAsync(DeleteBuildingsInput input)
    {
        await _buildingsAppService.DeleteAsync(input);
    }
    [HttpPost("GetBuildingById")]
    [SwaggerOperation(summary: "根据Id获取教学楼", Tags = new[] { "Buildings" })]   
    public async Task<GetBuildingOutPutDto> GetBuildingAsync(Guid buildingId)
    {
       return await _buildingsAppService.GetBuildingAsync(buildingId);
    }

    [HttpPost("GetAllBuildingAsync")]
    [SwaggerOperation(summary: "无条件获取所有教学楼", Tags = new[] { "Buildings" })]
    public async Task<List<GetBuildingOutPutDto>> GetAllBuildingAsync()
    {
        return await _buildingsAppService.GetAllBuildingAsync();
    }
}