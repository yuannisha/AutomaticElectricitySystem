namespace Yuannisha.AutomaticElectricitySystem.DailyTotalConsumptionShared;

public class AllDailyTotalConsumptionDto
{
    public Guid Id { get; set; }
    
    public double PowerConsumption { get; set; }
    
    public string Date { get; set; }
}