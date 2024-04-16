using Volo.Abp.Domain.Repositories;

namespace Yuannisha.AutomaticElectricitySystem.BuildingsEntity
{
    public interface IBuildingsRepository : IBasicRepository<BuildingsEntity.Buildings, Guid>
    {
        Task<List<BuildingsEntity.Buildings>> GetListAsync(int maxResultCount = 10, int skipCount = 0);

        Task<long> GetCountAsync();
        
    }
}

