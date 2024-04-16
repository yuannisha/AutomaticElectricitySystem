using Volo.Abp.Application.Dtos;
using Yuannisha.AutomaticElectricitySystem.PowerSwitchsIAppservice;
using Yuannisha.AutomaticElectricitySystem.PowerSwitchsShared;

namespace Yuannisha.AutomaticElectricitySystem.PowerSwitchsController;

[Route("PowerSwitchs")]
public class PowerSwitchsController : AbpController, IPowerSwitchsAppService
{
    private readonly IPowerSwitchsAppService _powerSwitchsAppService;

    public PowerSwitchsController(IPowerSwitchsAppService powerSwitchsAppService)
    {
        _powerSwitchsAppService = powerSwitchsAppService;
    }

    [HttpPost("Page")]
    [SwaggerOperation(summary: "分页查询智能空开", Tags = new[] { "PowerSwitchs" })]     
    public async Task<PagedResultDto<PowerSwitchsDto>> PageAsync(PagePowerSwitchsInput input)
    {
        return await _powerSwitchsAppService.PageAsync(input);
    }  

    [HttpPost("Create")]
    [SwaggerOperation(summary: "创建智能空开", Tags = new[] { "PowerSwitchs" })]        
    public async Task<PowerSwitchsDto> CreateAsync(CreatePowerSwitchsInput input)
    {
       return await _powerSwitchsAppService.CreateAsync(input);
    }

    [HttpPost("Update")]
    [SwaggerOperation(summary: "编辑智能空开", Tags = new[] { "PowerSwitchs" })]         
    public async Task<PowerSwitchsDto> UpdateAsync(UpdatePowerSwitchsInput input)
    {
       return await _powerSwitchsAppService.UpdateAsync(input);
    }

    [HttpPost("Delete")]
    [SwaggerOperation(summary: "删除智能空开", Tags = new[] { "PowerSwitchs" })]         
    public async Task DeleteAsync(DeletePowerSwitchsInput input)
    {
       await _powerSwitchsAppService.DeleteAsync(input);
    }
    
    [HttpPost("GetAllPowerSwitchs")]
    [SwaggerOperation(summary: "无条件获取所有智能空开", Tags = new[] { "PowerSwitchs" })]  
    public async Task<List<PowerSwitchsDto>> GetAllPowerSwitchs()
    {
        return await _powerSwitchsAppService.GetAllPowerSwitchs();
    }

    // Task<PagedResultDto<PowerSwitchsDto>> IPowerSwitchsAppService.PageAsync(PagePowerSwitchsInput input)
    // {
    //     throw new NotImplementedException();
    // }
}