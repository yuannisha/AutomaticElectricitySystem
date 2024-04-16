using Lion.AbpPro.Core;

namespace Yuannisha.AutomaticElectricitySystem.BookingInformationsShared
{
    public class GetAllBookingInforInput : PagingBase
    {
        /// <summary>
        /// 学号
        /// </summary>
        public string StudentId { get;  set; }
        /// <summary>
        /// 学生名字
        /// </summary>
        public string StudentName { get;  set; }
        /// <summary>
        /// 学生班级
        /// </summary>
        public string StudentClass { get;  set; }
        /// <summary>
        /// 使用教室
        /// </summary>
        public string UsingClassroom { get;  set; }
        /// <summary>
        /// 预约时间段
        /// </summary>
        public string BookingTimespan { get;  set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreationTime { get;  set; }
    }
}
