using Volo.Abp.Domain.Repositories;

namespace Yuannisha.AutomaticElectricitySystem.PowerSwitchsEntity
{
    public interface IPowerSwitchsRepository : IBasicRepository<PowerSwitchs, Guid>
    {
        Task<List<PowerSwitchs>> GetListAsync(int maxResultCount = 10, int skipCount = 0);

        Task<long> GetCountAsync();
    }
}

