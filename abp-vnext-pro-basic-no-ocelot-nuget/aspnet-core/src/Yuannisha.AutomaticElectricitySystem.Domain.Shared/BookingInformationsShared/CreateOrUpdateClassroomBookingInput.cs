namespace Yuannisha.AutomaticElectricitySystem.BookingInformationsShared
{
    public class CreateOrUpdateClassroomBookingInput
    {
        /// <summary>
        ///  修改数据传输类
        /// </summary>
        [Required]
        public ClassroomBookingEditDto ClassroomBooking { get; set; }
    }
}
