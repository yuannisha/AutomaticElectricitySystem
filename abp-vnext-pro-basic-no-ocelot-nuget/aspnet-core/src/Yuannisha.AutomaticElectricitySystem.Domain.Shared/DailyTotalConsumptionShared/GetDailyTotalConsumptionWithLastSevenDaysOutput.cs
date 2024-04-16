namespace Yuannisha.AutomaticElectricitySystem.DailyTotalConsumptionShared;

public class GetDailyTotalConsumptionWithLastSevenDaysOutput
{
    public List<string> Dates { get; set; }
    public List<double> PowerConsumption { get; set; }
}