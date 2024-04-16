using Volo.Abp.Domain.Repositories;

namespace Yuannisha.AutomaticElectricitySystem.DailyTotalConsumptionEntity;

public interface IDailyTotalConsumptionRepository : IBasicRepository<BuildingsEntity.Buildings, Guid>
{
    
}