using Abp.Application.Services.Dto;
using Yuannisha.AutomaticElectricitySystem.BookingInformationsShared;

namespace Yuannisha.AutomaticElectricitySystem.BookingInformationsIAppservice
{
    /// <summary>
    /// 预约信息
    /// </summary>
    public interface IBookingInformationAppService : IApplicationService
    {
        ///// <summary>
        ///// 分页查询预约信息
        ///// </summary>
        //Task<PagedResultDto<PageBookingInformationOutput>> PageAsync(PageBookingInformationInput input);

        ///// <summary>
        ///// 创建预约信息
        ///// </summary>    
        //Task CreateAsync(CreateBookingInformationInput input);

        ///// <summary>
        ///// 编辑预约信息
        ///// </summary>
        //Task UpdateAsync(UpdateBookingInformationInput input);

        ///// <summary>
        ///// 删除预约信息
        ///// </summary>
        //Task DeleteAsync(DeleteBookingInformationInput input);




        Task<Volo.Abp.Application.Dtos.PagedResultDto<BookingInformationDto>> GetAllBookingInfor(GetAllBookingInforInput input);

        Task<StoreAndInsertNewInforOutput> StoreAndInsertNewInfor(CreateOrUpdateClassroomBookingInput input);

        AvailableTimespanOutput GetAvailableTimespanInfor(AvailableTimespanInput input);

        Task<BookingInWeekendOutput> BookingInWeekend(BookingInWeekendInput input);

        List<TimespansInWeekend> GetAvailableTimespanInforInWeekend(AvailableTimespanInput input);

        /// <summary>
        /// 获取编辑信息
        /// </summary>
        /// <param name="input">编辑Dto</param>
        /// <returns>编辑Dto</returns>
        Task<GetClassroomBookingEditOutput> GetClassroomBookingEdit(NullableIdDto<Guid> input);

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input">创建Dto</param>
        Task CreateClassroomBooking(CreateOrUpdateClassroomBookingInput input);

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input">修改Dto</param>
        Task UpdateClassroomBooking(CreateOrUpdateClassroomBookingInput input);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input">需要删除的列表</param>
        Task DeleteClassroomBooking(Volo.Abp.Application.Dtos.EntityDto<List<Guid>> input);

        /// <summary>
        /// 身份信息确认
        /// </summary>
        /// <param name="StudentId"></param>
        /// <param name="Name"></param>
        /// <param name="Class"></param>
        /// <returns></returns>
        bool IdentityVerify(string StudentId, string Name, string ClassName);
        
        /// <summary>
        /// 无条件获取所有信息
        /// </summary>
        /// <param name="input"></param>
        Task<List<BookingInformationDto>> GetAllInformation();
        
        /// <summary>
        /// 获取教室使用情况
        /// </summary>
        /// <param name="input"></param>
        Task<GetRoomsUsedConditionOutput> GetRoomsUsedCondition();


        ///// <summary>
        ///// 获取周末的教室预约时间表
        ///// </summary>
        ///// <param name="input">需要删除的列表</param>
        //Task<List<TimespansInWeekend>> GetAvailableTimespanInforInWeekend(AvailableTimespanInput input);
    }
}


