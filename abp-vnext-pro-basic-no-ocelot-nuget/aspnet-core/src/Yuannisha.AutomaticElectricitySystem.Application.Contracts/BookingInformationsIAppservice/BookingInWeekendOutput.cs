namespace Yuannisha.AutomaticElectricitySystem.BookingInformationsIAppservice
{
    public class BookingInWeekendOutput
    {
        public bool SuccessfullyOrNot { get; set; }
        public List<TimespansInWeekend> NewTimeSpansss { get; set; }
        public string BookingResult { get; set; }
    }
}
