namespace Yuannisha.AutomaticElectricitySystem.PowerConsumptionIAppservice;

public class UpdateBuildingConsumptionInput
{
    public Guid Id { get; set; }
    public Guid BuildingId { get; set; }
    
    public string Date { get; set; }
    
    public double PowerConsumption { get; set; }
}