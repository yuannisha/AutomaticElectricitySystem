using Volo.Abp.Application.Dtos;
using Yuannisha.AutomaticElectricitySystem.RoomsShared;

namespace Yuannisha.AutomaticElectricitySystem.RoomsIAppservice
{
    /// <summary>
    /// 教室
    /// </summary>
    public interface IRoomsAppService : IApplicationService
    {
        /// <summary>
        /// 分页查询教室                                                                                     
        /// </summary>
        Task<PagedResultDto<RoomList>> PageAsync(PageRoomsInput input);

        /// <summary>
        /// 创建教室
        /// </summary>    
        Task<RoomsDto> CreateAsync(CreateRoomsInput input);

        /// <summary>
        /// 编辑教室
        /// </summary>
        Task<RoomsDto> UpdateAsync(UpdateRoomsInput input);

        /// <summary>
        /// 删除教室
        /// </summary>
        Task DeleteAsync(DeleteRoomsInput input);
        
        /// <summary>
        /// 无条件查询所有教室
        /// </summary>
        Task<List<GetAllClassroomsOutPutDto>> GetAllClassrooms();

        Task test();
    }
}


