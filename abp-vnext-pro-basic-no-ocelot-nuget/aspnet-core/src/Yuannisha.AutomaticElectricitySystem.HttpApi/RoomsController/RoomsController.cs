using Volo.Abp.Application.Dtos;
using Yuannisha.AutomaticElectricitySystem.RoomsIAppservice;
using Yuannisha.AutomaticElectricitySystem.RoomsShared;

namespace Yuannisha.AutomaticElectricitySystem.RoomsController;

[Route("Rooms")]
public class RoomsController : AbpController, IRoomsAppService
{
    private readonly IRoomsAppService _roomsAppService;

    public RoomsController(IRoomsAppService roomsAppService)
    {
        _roomsAppService = roomsAppService;
    }

    [HttpPost("Page")]
    [SwaggerOperation(summary: "分页查询教室", Tags = new[] { "Rooms" })]     
    public async Task<PagedResultDto<RoomList>> PageAsync(PageRoomsInput input)
    {
        return await _roomsAppService.PageAsync(input);
    }  

    [HttpPost("Create")]
    [SwaggerOperation(summary: "创建教室", Tags = new[] { "Rooms" })]        
    public async Task<RoomsDto> CreateAsync(CreateRoomsInput input)
    {
        return await _roomsAppService.CreateAsync(input);
    }

    [HttpPost("Update")]
    [SwaggerOperation(summary: "编辑教室", Tags = new[] { "Rooms" })]         
    public async Task<RoomsDto> UpdateAsync(UpdateRoomsInput input)
    {
       return await _roomsAppService.UpdateAsync(input);
    }

    [HttpPost("Delete")]
    [SwaggerOperation(summary: "删除教室", Tags = new[] { "Rooms" })]         
    public async Task DeleteAsync(DeleteRoomsInput input)
    {
        await _roomsAppService.DeleteAsync(input);
    }

    [HttpPost("GetAllClassrooms")]
    [SwaggerOperation(summary: "无条件获取所有教室", Tags = new[] { "Rooms" })]    
    public async Task<List<GetAllClassroomsOutPutDto>> GetAllClassrooms()
    {
        return await _roomsAppService.GetAllClassrooms();
    }
    [HttpPost("test")]
    [SwaggerOperation(summary: "测试", Tags = new[] { "Rooms" })] 
    public async Task test()
    {
        await _roomsAppService.test();
    }
}