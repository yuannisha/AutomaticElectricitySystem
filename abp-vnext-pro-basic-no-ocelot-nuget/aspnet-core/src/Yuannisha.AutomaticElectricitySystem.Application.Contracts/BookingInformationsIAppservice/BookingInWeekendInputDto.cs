using System.ComponentModel.DataAnnotations;

namespace Yuannisha.AutomaticElectricitySystem.BookingInformationsIAppservice
{
    public class BookingInWeekendInputDto
    {
        /// <summary>
        /// 学号
        /// </summary>
        [Required]
        public virtual string StudentId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Required]
        public virtual string StudentName { get; set; }

        /// <summary>
        /// 班级
        /// </summary>
        [Required]
        public virtual string StudentClass { get; set; }

        /// <summary>
        /// 使用用途
        /// </summary>
        [Required]
        public virtual string UsingPurpose { get; set; }

        [Required]
        public virtual string TelephoneNumber { get; set; } //使用用途

        /// <summary>
        /// 使用开始时间
        /// </summary>
        [Required]
        public virtual List<BookingDatas> BookingDatas { get; set; }
    }
    public class BookingDatas
    {
        /// <summary>
        /// 使用的教室
        /// </summary>
        [Required]
        public virtual string UsingClassroom { get; set; }

        /// <summary>
        /// 使用的教室
        /// </summary>
        [Required]
        public virtual string Date { get; set; }

        /// <summary>
        /// 使用的教室
        /// </summary>
        [Required]
        public virtual string BookingTimespan { get; set; }
    }
}
