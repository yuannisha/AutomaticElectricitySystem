namespace Yuannisha.AutomaticElectricitySystem.BookingLimitedsShared
{
    /// <summary>
    /// 预约限制信息
    /// </summary>
    public class BookingLimitedDto
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 学号
        /// </summary>
        public string StudentId { get; set; }
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName { get; set; }
        /// <summary>
        /// 预约日期
        /// </summary>
        public string Date { get; set; }
        /// <summary>
        /// 已预约小时数
        /// </summary>
        public int BookedHours { get; set; }
    }
}

