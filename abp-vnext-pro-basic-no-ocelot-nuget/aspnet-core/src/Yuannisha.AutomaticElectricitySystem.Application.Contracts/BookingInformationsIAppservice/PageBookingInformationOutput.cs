namespace Yuannisha.AutomaticElectricitySystem.BookingInformationsIAppservice
{
    /// <summary>
    /// 创建预约信息
    /// </summary>
    public class PageBookingInformationOutput
    {
        /// <summary>
        /// 预约信息Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 学号
        /// </summary>
        public string StudentId { get; set; }
        /// <summary>
        /// 学生名字
        /// </summary>
        public string StudentName { get; set; }
        /// <summary>
        /// 学生班级
        /// </summary>
        public string StudentClass { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        public string TelephoneNumber { get; set; }
        /// <summary>
        /// 使用教室
        /// </summary>
        public string UsingClassroom { get; set; }
        /// <summary>
        /// 使用用途
        /// </summary>
        public string UsingPurpose { get; set; }
        /// <summary>
        /// 预约时间段
        /// </summary>
        public string BookingTimespan { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>       
        public DateTime CreationTime { get; set; }
    }
}

