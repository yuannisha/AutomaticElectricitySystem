using Volo.Abp.Application.Dtos;

namespace Yuannisha.AutomaticElectricitySystem.BookingInformationsShared
{
    public class ClassroomBookingEditDto : IEntityDto<Guid?>
    {

        /// <summary>
        /// 标识
        /// </summary>
        public virtual Guid? Id { get; set; } // Id (Primary key)
        //[GenField(StoreAppServiceType = typeof(IClassroomBookingAppService),
        //    FieldType = FieldType.ComboBox,
        //    DisplayFieldName = nameof(BookingInformation.CreationTime),
        //    ValueFieldName = nameof(BookingInformation.Id))]


        [Required]
        public virtual string StudentId { get; set; } //学号



        [Required]
        public virtual string StudentName { get; set; } //学生姓名



        [Required]
        public virtual string StudentClass { get; set; } //学生班级



        [Required]
        public virtual string UsingClassroom { get; set; } //使用教室



        [Required]
        public virtual string UsingPurpose { get; set; } //使用用途



        [Required]
        public virtual string BookingTimespan { get; set; } //使用时间段，例如："8:00-9:00","13:00-14:00"


        [Required]
        public virtual string TelephoneNumber { get; set; } //使用用途
    }
}
