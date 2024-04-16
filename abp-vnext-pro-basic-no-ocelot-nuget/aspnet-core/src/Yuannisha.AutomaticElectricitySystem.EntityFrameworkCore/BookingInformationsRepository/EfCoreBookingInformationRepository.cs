using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Yuannisha.AutomaticElectricitySystem.BookingInformationsEntity;
using Yuannisha.AutomaticElectricitySystem.EntityFrameworkCore;

namespace Yuannisha.AutomaticElectricitySystem.BookingInformationsRepository
{
    /// <summary>
    /// 预约信息 仓储Ef core 实现
    /// </summary>
    public class EfCoreBookingInformationRepository :
        EfCoreRepository<IAutomaticElectricitySystemDbContext, BookingInformation, Guid>,
        IBookingInformationRepository
    {
        public EfCoreBookingInformationRepository(IDbContextProvider<IAutomaticElectricitySystemDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<BookingInformation>> GetListAsync(int maxResultCount = 10, int skipCount = 0)
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

