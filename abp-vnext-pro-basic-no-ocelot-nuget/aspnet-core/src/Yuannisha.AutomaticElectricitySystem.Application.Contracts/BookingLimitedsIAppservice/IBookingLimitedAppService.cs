using Volo.Abp.Application.Dtos;
using Yuannisha.AutomaticElectricitySystem.BookingLimitedsShared;

namespace Yuannisha.AutomaticElectricitySystem.BookingLimitedsIAppservice
{
    /// <summary>
    /// 预约限制信息
    /// </summary>
    public interface IBookingLimitedAppService : IApplicationService
    {
        /// <summary>
        /// 分页查询预约限制信息
        /// </summary>
        /// <returns>PagedResultDto<PageBookingLimitedOutput></returns>        
        public Task<PagedResultDto<BookingLimitedDto>> PageAsync(PageBookingLimitedInput input);
        
    }
}


