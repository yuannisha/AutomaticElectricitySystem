using Yuannisha.AutomaticElectricitySystem.BookingLimitedsShared;

namespace Yuannisha.AutomaticElectricitySystem.BookingLimitedsIAppservice
{
    /// <summary>
    /// 创建预约限制信息
    /// </summary>
    public class PageBookingLimitedOutput
    {
        // /// <summary>
        // /// 预约限制信息Id
        // /// </summary>
        // public Guid Id { get; set; }
        //
        // /// <summary>
        // /// 学号
        // /// </summary>
        // public string StudentId { get; set; }
        // /// <summary>
        // /// 学生姓名
        // /// </summary>
        // public string StudentName { get; set; }
        // /// <summary>
        // /// 预约日期
        // /// </summary>
        // public string Date { get; set; }
        // /// <summary>
        // /// 已预约小时数
        // /// </summary>
        // public int BookedHours { get; set; }
        //
        // /// <summary>
        // /// 创建时间
        // /// </summary>       
        // public DateTime CreationTime { get; set; }
        
        public List<BookingLimitedDto> BookingLimitedDto { get; set; }
        
        public int TotalCount { get; set; }
    }
}

