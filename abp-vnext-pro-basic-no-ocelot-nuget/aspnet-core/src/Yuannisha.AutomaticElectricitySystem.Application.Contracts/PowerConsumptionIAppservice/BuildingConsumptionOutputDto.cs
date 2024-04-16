namespace Yuannisha.AutomaticElectricitySystem.PowerConsumptionIAppservice;

public class BuildingConsumptionOutputDto
{
    public Guid Id { get; set; }
    
    public string Date { get; set; }
    
    public Guid BuildingId { get; set; }
    
    public double PowerConsumption { get; set; }
}