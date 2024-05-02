using Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace Yuannisha.AutomaticElectricitySystem.DailyTotalConsumptionEntity;

public class DailyTotalConsumption : FullAuditedAggregateRoot<Guid>,IEntity<Guid>
{
    public bool IsTransient()
    {
        throw new NotImplementedException();
    }

    public Guid Id { get; set; }
    
    public double PowerConsumption { get; set; }
    
    public string Date { get; set; }

    public void Update(string date, double powerConsumption)
    {
        Date = date;
        PowerConsumption = powerConsumption;
    }
}