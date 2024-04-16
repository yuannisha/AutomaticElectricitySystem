namespace Yuannisha.AutomaticElectricitySystem.PowerConsumptionIAppservice;

public class CreateBuildingConsumptionInput
{
    public Guid BuildingId { get; set; }
    
    public string Date { get; set; }
    
    public double PowerConsumption { get; set; }
}