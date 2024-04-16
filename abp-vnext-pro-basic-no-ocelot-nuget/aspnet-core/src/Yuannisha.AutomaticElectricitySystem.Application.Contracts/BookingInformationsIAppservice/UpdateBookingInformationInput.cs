using System.ComponentModel.DataAnnotations;

namespace Yuannisha.AutomaticElectricitySystem.BookingInformationsIAppservice
{
    /// <summary>
    /// 删除预约信息
    /// </summary>
    public class UpdateBookingInformationInput
    {
        /// <summary>
        /// 预约信息Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 学号
        /// </summary>
        [Required(ErrorMessage = "学号不能为空")]
        public string StudentId { get; set; }
        /// <summary>
        /// 学生名字
        /// </summary>
        [Required(ErrorMessage = "学生名字不能为空")]
        public string StudentName { get; set; }
        /// <summary>
        /// 学生班级
        /// </summary>
        [Required(ErrorMessage = "学生班级不能为空")]
        public string StudentClass { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        [Required(ErrorMessage = "电话号码不能为空")]
        public string TelephoneNumber { get; set; }
        /// <summary>
        /// 使用教室
        /// </summary>
        [Required(ErrorMessage = "使用教室不能为空")]
        public string UsingClassroom { get; set; }
        /// <summary>
        /// 使用用途
        /// </summary>
        [Required(ErrorMessage = "使用用途不能为空")]
        public string UsingPurpose { get; set; }
        /// <summary>
        /// 预约时间段
        /// </summary>
        [Required(ErrorMessage = "预约时间段不能为空")]
        public string BookingTimespan { get; set; }
    }
}

