using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Yuannisha.AutomaticElectricitySystem.EntityFrameworkCore;
using Yuannisha.AutomaticElectricitySystem.PowerSwitchsEntity;

namespace Yuannisha.AutomaticElectricitySystem.PowerSwitchsRepository
{
    /// <summary>
    /// 智能空开 仓储Ef core 实现
    /// </summary>
    public class EfCorePowerSwitchsRepository :
        EfCoreRepository<IAutomaticElectricitySystemDbContext, PowerSwitchsEntity.PowerSwitchs, Guid>,
        IPowerSwitchsRepository
    {
        public EfCorePowerSwitchsRepository(IDbContextProvider<IAutomaticElectricitySystemDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<PowerSwitchsEntity.PowerSwitchs>> GetListAsync(int maxResultCount = 10, int skipCount = 0)
        {
            return await (await GetDbSetAsync())
                .OrderByDescending(e => e.CreationTime)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync();
        }

        public async Task<long> GetCountAsync()
        {
            return await (await GetDbSetAsync()).CountAsync();
        }
    }
}

