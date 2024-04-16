using System.ComponentModel.DataAnnotations;

namespace Yuannisha.AutomaticElectricitySystem.BookingLimitedsIAppservice
{
    /// <summary>
    /// 创建预约限制信息
    /// </summary>
    public class CreateBookingLimitedInput
    {
        /// <summary>
        /// 标识
        /// </summary>
        public virtual Guid? Id { get; set; }
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
        /// 日期
        /// </summary>
        [Required]
        public virtual string Date { get; set; }
        /// <summary>
        /// 已预约时长
        /// </summary>
        public int BookedHours { get; set; }
    }
}

