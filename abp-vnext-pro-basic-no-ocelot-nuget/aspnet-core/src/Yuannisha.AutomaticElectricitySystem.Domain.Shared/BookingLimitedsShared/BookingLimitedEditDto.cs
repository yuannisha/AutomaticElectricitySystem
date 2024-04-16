using Volo.Abp.Application.Dtos;

namespace Yuannisha.AutomaticElectricitySystem.BookingLimitedsShared
{
    public class BookingLimitedEditDto : IEntityDto<Guid?>
    {
        /// <summary>
        /// 标识
        /// </summary>
        public virtual Guid? Id { get; set; }

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
        /// 学号
        /// </summary>
        [Required]
        public virtual string Date { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Required]
        public virtual int BookedHours { get; set; }
    }
}
