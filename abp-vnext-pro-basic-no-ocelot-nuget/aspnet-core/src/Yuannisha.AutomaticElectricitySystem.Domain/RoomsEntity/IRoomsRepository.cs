using Volo.Abp.Domain.Repositories;

namespace Yuannisha.AutomaticElectricitySystem.RoomsEntity
{
    public interface IRoomsRepository : IBasicRepository<RoomsEntity.Rooms, Guid>
    {
        Task<List<RoomsEntity.Rooms>> GetListAsync(int maxResultCount = 10, int skipCount = 0);

        Task<long> GetCountAsync();
    }
}

