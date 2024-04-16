using Volo.Abp.Application.Dtos;
using Yuannisha.AutomaticElectricitySystem.RoomsEntity;
using Yuannisha.AutomaticElectricitySystem.RoomsIAppservice;
using Yuannisha.AutomaticElectricitySystem.RoomsShared;

namespace Yuannisha.AutomaticElectricitySystem.RoomsAppservice
{
    /// <summary>
    /// 教室
    /// </summary>
    public class RoomsAppService : ApplicationService, IRoomsAppService
    {
        private readonly RoomsManager _roomsManager;

        public RoomsAppService(RoomsManager roomsManager)
        {
            _roomsManager = roomsManager;
        }

        /// <summary>
        /// 分页查询教室
        /// </summary>
        /// <returns>PagedResultDto<PageRoomsOutput></returns>
        [Authorize(AutomaticElectricitySystemPermissions.RoomsManagement.Rooms.View)] 
        public async Task<PagedResultDto<RoomList>> PageAsync(PageRoomsInput input)
        {
            var result = await _roomsManager.GetListAsync(input);
            // var result = new PagedResultDto<PageRoomsOutput>();
            // var totalCount = await _roomsManager.GetCountAsync();
            // result.TotalCount = totalCount;
            // if (totalCount <= 0) return result;
            // var list = await _roomsManager.GetListAsync(input);
            // result.Items = ObjectMapper.Map<List<RoomList>, List<PageRoomsOutput>>(list);
            return new PagedResultDto<RoomList>(result.TotalCount,result.RoomList);
        }

        /// <summary>
        /// 创建教室
        /// </summary>
        [Authorize(AutomaticElectricitySystemPermissions.RoomsManagement.Rooms.Create)]
        public async Task<RoomsDto> CreateAsync(CreateRoomsInput input)
        {
            return await _roomsManager.CreateAsync(
             GuidGenerator.Create(),
             input.BuildingId,
             input.Floor,
             input.No,
             input.UsingOrNot,
             input.RoomType,
             input.ControlType
                    );
        }

        /// <summary>
        /// 编辑教室
        /// </summary>
        [Authorize(AutomaticElectricitySystemPermissions.RoomsManagement.Rooms.Edit)]
        public async Task<RoomsDto> UpdateAsync(UpdateRoomsInput input)
        {
            return await _roomsManager.UpdateAsync(
             input.Id,
             input.BuildingId,
             input.Floor,
             input.No,
             input.UsingOrNot,
             input.RoomType,
             input.ControlType
             );
        }

        /// <summary>
        /// 删除教室
        /// </summary>
        [Authorize(AutomaticElectricitySystemPermissions.RoomsManagement.Rooms.Delete)]
        public async Task DeleteAsync(DeleteRoomsInput input)
        {
            await _roomsManager.DeleteAsync(input.Id);
        }

        public async Task<List<GetAllClassroomsOutPutDto>> GetAllClassrooms()
        {
            return await _roomsManager.GetAllClassrooms();
        }

        public async Task test()
        {
            var rooms =await _roomsManager.TTTTT();
            foreach (var room in rooms)
            {
                foreach (var roomPowerSwitch in room.PowerSwitches)
                {
                    Console.WriteLine(roomPowerSwitch.SerialNumber);
                }
            }
        }
    }
}

