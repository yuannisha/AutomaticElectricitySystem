using Volo.Abp.Application.Dtos;
using Yuannisha.AutomaticElectricitySystem.BookingInformationsIAppservice;
using Yuannisha.AutomaticElectricitySystem.BookingInformationsShared;

namespace Yuannisha.AutomaticElectricitySystem.BookingInformationsController;

[Route("BookingInformations")]
public class BookingInformationController : AbpController, IBookingInformationAppService
{
    private readonly IBookingInformationAppService _bookingInformationAppService;

    public BookingInformationController(IBookingInformationAppService bookingInformationAppService)
    {
        _bookingInformationAppService = bookingInformationAppService;
    }

    [HttpPost("GetAllBookingInfor")]
    [SwaggerOperation(summary: "获取所有的预约信息", Tags = new[] { "BookingInformations" })]
    public async Task<PagedResultDto<BookingInformationDto>> GetAllBookingInfor(GetAllBookingInforInput input)
    {
        return await _bookingInformationAppService.GetAllBookingInfor(input);
    }

    [HttpPost("StoreAndInsertNewInfor")]
    [SwaggerOperation(summary: "插入预约信息", Tags = new[] { "BookingInformations" })]
    public async Task<StoreAndInsertNewInforOutput> StoreAndInsertNewInfor(CreateOrUpdateClassroomBookingInput input)
    {
        return await _bookingInformationAppService.StoreAndInsertNewInfor(input);
    }

    [HttpPost("BookingInWeekend")]
    [SwaggerOperation(summary: "周末预约", Tags = new[] { "BookingInformations" })]
    public async Task<BookingInWeekendOutput> BookingInWeekend(BookingInWeekendInput input)
    {
        return await _bookingInformationAppService.BookingInWeekend(input);
    }

    [HttpPost("GetAvailableTimespanInforInWeekend")]
    [SwaggerOperation(summary: "获得指定教室周末空闲时间段", Tags = new[] { "BookingInformations" })]
    public List<TimespansInWeekend> GetAvailableTimespanInforInWeekend(AvailableTimespanInput input)
    {
        return _bookingInformationAppService.GetAvailableTimespanInforInWeekend(input);
    }

    [HttpPost("GetClassroomBookingEdit")]
    [SwaggerOperation(summary: "获取教室编辑", Tags = new[] { "BookingInformations" })]
    public async Task<GetClassroomBookingEditOutput> GetClassroomBookingEdit(Abp.Application.Services.Dto.NullableIdDto<Guid> input)
    {
        return await _bookingInformationAppService.GetClassroomBookingEdit(input);
    }

    [HttpPost("CreateClassroomBooking")]
    [SwaggerOperation(summary: "创建预约信息", Tags = new[] { "BookingInformations" })]
    public async Task CreateClassroomBooking(CreateOrUpdateClassroomBookingInput input)
    {
        await _bookingInformationAppService.CreateClassroomBooking(input);
    }

    [HttpPost("UpdateClassroomBooking")]
    [SwaggerOperation(summary: "更新预约信息", Tags = new[] { "BookingInformations" })]
    public async Task UpdateClassroomBooking(CreateOrUpdateClassroomBookingInput input)
    {
        await _bookingInformationAppService.UpdateClassroomBooking(input);
    }

    [HttpPost("DeleteClassroomBooking")]
    [SwaggerOperation(summary: "删除预约信息", Tags = new[] { "BookingInformations" })]
    public async Task DeleteClassroomBooking(EntityDto<List<Guid>> input)
    {
        await _bookingInformationAppService.DeleteClassroomBooking(input);
    }

    [HttpPost("GetAvailableTimespanInfor")]
    [SwaggerOperation(summary: "获得指定教室空闲时间段", Tags = new[] { "BookingInformations" })]
    public AvailableTimespanOutput GetAvailableTimespanInfor(AvailableTimespanInput input)
    {
        return _bookingInformationAppService.GetAvailableTimespanInfor(input) ;
    }

    [HttpPost("IdentityVerify")]
    [SwaggerOperation(summary: "身份信息确认", Tags = new[] { "BookingInformations" })]
    public bool IdentityVerify(string StudentId, string Name, string ClassName)
    {
        return _bookingInformationAppService.IdentityVerify(StudentId, Name, ClassName) ;
    }

    [HttpPost("GetAllInformation")]
    [SwaggerOperation(summary: "无条件获取所有预约信息", Tags = new[] { "BookingInformations" })]
    public async Task<List<BookingInformationDto>> GetAllInformation()
    {
        return await _bookingInformationAppService.GetAllInformation();
    }
    [HttpPost("GetRoomsUsedCondition")]
    [SwaggerOperation(summary: "获取过去12小时的教室使用情况", Tags = new[] { "BookingInformations" })]
    public async Task<GetRoomsUsedConditionOutput> GetRoomsUsedCondition()
    {
        return await _bookingInformationAppService.GetRoomsUsedCondition();
    }
}