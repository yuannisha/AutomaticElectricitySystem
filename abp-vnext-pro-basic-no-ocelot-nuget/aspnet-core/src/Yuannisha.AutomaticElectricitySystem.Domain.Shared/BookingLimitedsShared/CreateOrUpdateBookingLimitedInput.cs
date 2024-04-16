namespace Yuannisha.AutomaticElectricitySystem.BookingLimitedsShared
{
    public class CreateOrUpdateBookingLimitedInput
    {
        /// <summary>
        ///  修改数据传输类
        /// </summary>
        [Required]
        public BookingLimitedEditDto BookingLimited { get; set; }
    }
}
