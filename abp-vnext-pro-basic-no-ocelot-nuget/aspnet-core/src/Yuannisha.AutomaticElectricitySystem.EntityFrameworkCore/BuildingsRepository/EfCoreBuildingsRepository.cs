using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Yuannisha.AutomaticElectricitySystem.BuildingsEntity;
using Yuannisha.AutomaticElectricitySystem.EntityFrameworkCore;

namespace Yuannisha.AutomaticElectricitySystem.BuildingsRepository
{
    /// <summary>
    /// 教学楼 仓储Ef core 实现
    /// </summary>
    public class EfCoreBuildingsRepository :
        EfCoreRepository<IAutomaticElectricitySystemDbContext, BuildingsEntity.Buildings, Guid>,
        IBuildingsRepository
    {
        public EfCoreBuildingsRepository(IDbContextProvider<IAutomaticElectricitySystemDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<BuildingsEntity.Buildings>> GetListAsync(int maxResultCount = 10, int skipCount = 0)
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

