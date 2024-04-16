namespace Yuannisha.AutomaticElectricitySystem.BookingInformationsIAppservice;

public class GetRoomsUsedConditionOutput
{
    public List<string> LastTwelveHours { get; set; }
    public List<int> UsedCountAmount { get; set; }
    public List<double> UseRatesOfRooms { get; set; }
}