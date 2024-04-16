using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Yuannisha.AutomaticElectricitySystem.EntityFrameworkCore;
using Yuannisha.AutomaticElectricitySystem.RoomsEntity;

namespace Yuannisha.AutomaticElectricitySystem.RoomsRepository
{
    /// <summary>
    /// 教室 仓储Ef core 实现
    /// </summary>
    public class EfCoreRoomsRepository :
        EfCoreRepository<IAutomaticElectricitySystemDbContext, RoomsEntity.Rooms, Guid>,
        IRoomsRepository
    {
        public EfCoreRoomsRepository(IDbContextProvider<IAutomaticElectricitySystemDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<RoomsEntity.Rooms>> GetListAsync(int maxResultCount = 10, int skipCount = 0)
        {
            return await (await GetDbSetAsync())
                .OrderByDescending(e=>e.CreationTime)
                .PageBy<RoomsEntity.Rooms>(skipCount, maxResultCount)
                .ToListAsync();
            // var dbSet = await GetDbSetAsync();
            // var query = dbSet.OrderByDescending(e => e.CreationTime);
            // var pagedQuery = query.PageBy(skipCount, maxResultCount);
            // return await pagedQuery.ToListAsync();
        }

        public async Task<long> GetCountAsync()
        {
            return await (await GetDbSetAsync()).CountAsync();
        }
    }
}

