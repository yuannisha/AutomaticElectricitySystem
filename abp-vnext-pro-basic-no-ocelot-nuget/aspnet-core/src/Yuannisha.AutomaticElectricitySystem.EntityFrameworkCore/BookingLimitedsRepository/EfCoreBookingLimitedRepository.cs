using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Yuannisha.AutomaticElectricitySystem.BookingLimitedsEntity;
using Yuannisha.AutomaticElectricitySystem.EntityFrameworkCore;

namespace Yuannisha.AutomaticElectricitySystem.BookingLimitedsRepository
{
    /// <summary>
    /// 预约限制信息 仓储Ef core 实现
    /// </summary>
    public class EfCoreBookingLimitedRepository :
        EfCoreRepository<IAutomaticElectricitySystemDbContext, BookingLimited, Guid>,
        IBookingLimitedRepository
    {
        public EfCoreBookingLimitedRepository(IDbContextProvider<IAutomaticElectricitySystemDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<BookingLimited>> GetListAsync(int maxResultCount = 10, int skipCount = 0)
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

