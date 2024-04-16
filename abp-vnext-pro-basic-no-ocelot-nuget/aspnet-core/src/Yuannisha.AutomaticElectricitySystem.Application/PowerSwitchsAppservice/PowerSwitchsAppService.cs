using Volo.Abp.Application.Dtos;
using Yuannisha.AutomaticElectricitySystem.PowerSwitchsEntity;
using Yuannisha.AutomaticElectricitySystem.PowerSwitchsIAppservice;
using Yuannisha.AutomaticElectricitySystem.PowerSwitchsShared;

namespace Yuannisha.AutomaticElectricitySystem.PowerSwitchsAppservice
{
    /// <summary>
    /// 智能空开
    /// </summary>
    [Authorize]
    public class PowerSwitchsAppService : ApplicationService, IPowerSwitchsAppService
    {
        private readonly PowerSwitchsManager _powerSwitchsManager;

        public PowerSwitchsAppService(PowerSwitchsManager powerSwitchsManager)
        {
            _powerSwitchsManager = powerSwitchsManager;
        }

        /// <summary>
        /// 分页查询智能空开
        /// </summary>
        /// <returns>PagedResultDto<PagePowerSwitchsOutput></returns>
        [Authorize(AutomaticElectricitySystemPermissions.PowerSwitchsManagement.PowerSwitchs.View)] 
        public async Task<PagedResultDto<PowerSwitchsDto>> PageAsync(PagePowerSwitchsInput input)
        {
            var result = await _powerSwitchsManager.GetListAsync(input);
            // var result = new PagedResultDto<PagePowerSwitchsOutput>();
            // var totalCount = await _powerSwitchsManager.GetCountAsync();
            // result.TotalCount = totalCount;
            // if (totalCount <= 0) return result;
            // var list = await _powerSwitchsManager.GetListAsync(input);
            // result.Items = ObjectMapper.Map<List<PowerSwitchsDto>, List<PagePowerSwitchsOutput>>(list);
            return new PagedResultDto<PowerSwitchsDto>(result.TotalCount,result.PowerSwitchsDto);
        }

        /// <summary>
        /// 创建智能空开
        /// </summary>
        [Authorize(AutomaticElectricitySystemPermissions.PowerSwitchsManagement.PowerSwitchs.Create)] 
        public async Task<PowerSwitchsDto> CreateAsync(CreatePowerSwitchsInput input)
        {
            return await _powerSwitchsManager.CreateAsync(
             GuidGenerator.Create(),
             input.RoomId,
             input.SerialNumber,
             input.ControlledMachineName,
             input.IsOnline,
             input.Status
                    );
        }

        /// <summary>
        /// 编辑智能空开
        /// </summary>
        [Authorize(AutomaticElectricitySystemPermissions.PowerSwitchsManagement.PowerSwitchs.Edit)] 
        public async Task<PowerSwitchsDto> UpdateAsync(UpdatePowerSwitchsInput input)
        {
            return await _powerSwitchsManager.UpdateAsync(
             input.Id,
             input.RoomId,
             input.SerialNumber,
             input.ControlledMachineName,
             input.IsOnline,
             input.Status,
             input.IsAbnormalOrNot,
             input.EnergyConsumption
             );
        }

        /// <summary>
        /// 删除智能空开
        /// </summary>
        [Authorize(AutomaticElectricitySystemPermissions.PowerSwitchsManagement.PowerSwitchs.Delete)] 
        public async Task DeleteAsync(DeletePowerSwitchsInput input)
        {
            await _powerSwitchsManager.DeleteAsync(input.Id);
        }
        
        /// <summary>
        /// 无条件获取所有智能空开
        /// </summary>
        public async Task<List<PowerSwitchsDto>> GetAllPowerSwitchs()
        {
            return await _powerSwitchsManager.GetAllPowerSwitchs();
        }
    }
}

