using Volo.Abp.Application.Dtos;
using Yuannisha.AutomaticElectricitySystem.PowerSwitchsShared;

namespace Yuannisha.AutomaticElectricitySystem.PowerSwitchsIAppservice
{
    /// <summary>
    /// 智能空开
    /// </summary>
    public interface IPowerSwitchsAppService : IApplicationService
    {
        /// <summary>
        /// 分页查询智能空开
        /// </summary>
        Task<PagedResultDto<PowerSwitchsDto>> PageAsync(PagePowerSwitchsInput input);

        /// <summary>
        /// 创建智能空开
        /// </summary>    
        Task<PowerSwitchsDto> CreateAsync(CreatePowerSwitchsInput input);

        /// <summary>
        /// 编辑智能空开
        /// </summary>
        Task<PowerSwitchsDto> UpdateAsync(UpdatePowerSwitchsInput input);

        /// <summary>
        /// 删除智能空开
        /// </summary>
        Task DeleteAsync(DeletePowerSwitchsInput input);

        /// <summary>
        /// 无条件获取所有智能空开
        /// </summary>
        public Task<List<PowerSwitchsDto>> GetAllPowerSwitchs();
    }
}


