using Volo.Abp.Application.Dtos;
using Yuannisha.AutomaticElectricitySystem.BookingLimitedsEntity;
using Yuannisha.AutomaticElectricitySystem.BookingLimitedsIAppservice;
using Yuannisha.AutomaticElectricitySystem.BookingLimitedsShared;

namespace Yuannisha.AutomaticElectricitySystem.BookingLimitedsAppservice;

/// <summary>
/// 预约限制信息
/// </summary>
[Authorize]
public class BookingLimitedAppService : ApplicationService, IBookingLimitedAppService
{
    private readonly BookingLimitedManager _bookingLimitedManager;

    public BookingLimitedAppService(BookingLimitedManager bookingLimitedManager)
    {
        _bookingLimitedManager = bookingLimitedManager;
    }
    
    /// <summary>
    /// 分页查询预约限制信息
    /// </summary>
    /// <returns>PagedResultDto<PageBookingLimitedOutput></returns>
    [Authorize(AutomaticElectricitySystemPermissions.BookingLimitedManagement.BookingLimited.View)] 
    public async Task<PagedResultDto<BookingLimitedDto>> PageAsync(PageBookingLimitedInput input)
    {
        var result = await _bookingLimitedManager.GetListAsync(input);
        // var result = new PagedResultDto<PageBookingLimitedOutput>();
        // var totalCount = await _bookingLimitedManager.GetCountAsync();
        // result.TotalCount = totalCount;
        // if (totalCount <= 0) return result;
        // var list = await _bookingLimitedManager.GetListAsync(input);
        // result.Items = ObjectMapper.Map<List<BookingLimitedDto>, List<PageBookingLimitedOutput>>(list);
        return new PagedResultDto<BookingLimitedDto>(result.TotalCount,result.BookingLimitedDto);
    }  
}