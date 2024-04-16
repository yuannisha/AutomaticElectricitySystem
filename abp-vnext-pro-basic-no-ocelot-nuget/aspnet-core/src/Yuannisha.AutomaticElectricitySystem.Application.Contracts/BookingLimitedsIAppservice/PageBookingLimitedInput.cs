using Lion.AbpPro.Core;

namespace Yuannisha.AutomaticElectricitySystem.BookingLimitedsIAppservice
{
    /// <summary>
    /// 创建预约限制信息
    /// </summary>
    public class PageBookingLimitedInput : PagingBase
    {
        /// <summary>
        /// 学号
        /// </summary>
        public string StudentId { get;  set; }
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName { get;  set; }
        /// <summary>
        /// 预约日期
        /// </summary>
        public string Date { get;  set; }
        /// <summary>
        /// 已预约小时数
        /// </summary>
        public int BookedHours { get;  set; }
    }
}

