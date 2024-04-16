using Volo.Abp.Application.Dtos;

namespace Yuannisha.AutomaticElectricitySystem.BookingInformationsShared
{
    public class BookingInforDto : AuditedEntityDto<Guid>
    {
        // /// <summary>
        // /// 学号
        // /// </summary>
        // [Required]
        // public virtual string StudentId { get; set; }
        //
        // /// <summary>
        // /// 姓名
        // /// </summary>
        // [Required]
        // public virtual string StudentName { get; set; }
        //
        // /// <summary>
        // /// 班级
        // /// </summary>
        // [Required]
        // public virtual string StudentClass { get; set; }
        //
        // /// <summary>
        // /// 使用的教室
        // /// </summary>
        // [Required]
        // public virtual string UsingClassroom { get; set; }
        //
        // /// <summary>
        // /// 使用用途
        // /// </summary>
        // [Required]
        // public virtual string UsingPurpose { get; set; }
        //
        // /// <summary>
        // /// 使用开始时间
        // /// </summary>
        // [Required]
        // public virtual string BookingTimespan { get; set; }
        //
        // /// <summary>
        // /// 使用开始时间
        // /// </summary>
        // [Required]
        // public virtual string TelephoneNumber { get; set; }
        
        /// <summary>
        /// 预约信息查询结果列表
        /// </summary>
        [Required]
        public virtual List<BookingInformationDto> BookingInformationDtoList { get ; set; }
        
        
        /// <summary>
        /// 预约信息查询结果计数
        /// </summary>
        [Required]
        public virtual int TotalCount { get; set; }
    }
}
