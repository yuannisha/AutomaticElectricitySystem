using Volo.Abp.Application.Dtos;
using Yuannisha.AutomaticElectricitySystem.BookingLimitedsIAppservice;
using Yuannisha.AutomaticElectricitySystem.BookingLimitedsShared;

namespace Yuannisha.AutomaticElectricitySystem.BookingLimitedsController;

[Route("BookingLimiteds")]
public class BookingLimitedController : AbpController, IBookingLimitedAppService
{
    private readonly IBookingLimitedAppService _bookingLimitedAppService;

    public BookingLimitedController(IBookingLimitedAppService bookingLimitedAppService)
    {
        _bookingLimitedAppService = bookingLimitedAppService;
    }

    [HttpPost("Page")]
    [SwaggerOperation(summary: "分页查询预约限制信息", Tags = new[] { "BookingLimiteds" })]     
    public async Task<PagedResultDto<BookingLimitedDto>> PageAsync(PageBookingLimitedInput input)
    {
        return await _bookingLimitedAppService.PageAsync(input);
    } 
}