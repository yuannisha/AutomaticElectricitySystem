using System.ComponentModel.DataAnnotations;

namespace Yuannisha.AutomaticElectricitySystem.BookingLimitedsIAppservice
{
    /// <summary>
    /// 删除预约限制信息
    /// </summary>
    public class UpdateBookingLimitedInput
    {
        /// <summary>
        /// 预约限制信息Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 学号
        /// </summary>
        [Required(ErrorMessage = "学号不能为空")]
        public string StudentId { get; set; }
        /// <summary>
        /// 学生姓名
        /// </summary>
        [Required(ErrorMessage = "学生姓名不能为空")]
        public string StudentName { get; set; }
        /// <summary>
        /// 预约日期
        /// </summary>
        [Required(ErrorMessage = "预约日期不能为空")]
        public string Date { get; set; }
        /// <summary>
        /// 已预约小时数
        /// </summary>
        [Required(ErrorMessage = "已预约小时数不能为空")]
        public int BookedHours { get; set; }
    }
}

