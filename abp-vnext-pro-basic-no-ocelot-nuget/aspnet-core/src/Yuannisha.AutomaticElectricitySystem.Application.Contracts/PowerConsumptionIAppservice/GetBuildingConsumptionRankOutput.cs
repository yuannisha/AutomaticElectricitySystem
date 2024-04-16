namespace Yuannisha.AutomaticElectricitySystem.PowerConsumptionIAppservice;

public class GetBuildingConsumptionRankOutput
{
    public class BuildingConsumptionRankOutputDto
    {
        public Guid Id { get; set; }
        public Guid BuildingId { get; set; }
        public string BuildingName { get; set; }
        public double PowerConsumption { get; set; }
    }
    
    public List<BuildingConsumptionRankOutputDto> BuildingConsumptionRankOutput { get; set; }
}