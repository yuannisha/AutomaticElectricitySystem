using Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Yuannisha.AutomaticElectricitySystem.PowerConsumption;

public class BuildingConsumption : FullAuditedAggregateRoot<Guid>, IEntity<Guid>
{
    public BuildingConsumption(Guid id,Guid buildingId,string date,double powerConsumption)
    {
        SetId(id);
        SetDate(date);
        SetBuildingId(buildingId);
        SetPowerConsumption(powerConsumption);
    }
    
    public void Update(Guid id,Guid buildingId,string date,double powerConsumption)
    {
        SetId(id);
        SetBuildingId(buildingId);
        SetDate(date);
        SetPowerConsumption(powerConsumption);
    }

    public Guid Id { get; set; }
    
    public Guid BuildingId { get; set; }
    
    public string Date { get; set; }
    public double PowerConsumption { get; set; }

    public BuildingsEntity.Buildings Building { get; set; } // 导航属性
    
    private void SetId(Guid id)
    {
        Id = id;
    }

    private void SetDate(string date)
    {
        Date = date;
    }
    private void SetBuildingId(Guid buildingId)
    {
        BuildingId = buildingId;
    }
    
    private void SetPowerConsumption(double powerConsumption)
    {
        PowerConsumption = powerConsumption;
    }
    
    public bool IsTransient()
        {
            throw new NotImplementedException();
        }
}