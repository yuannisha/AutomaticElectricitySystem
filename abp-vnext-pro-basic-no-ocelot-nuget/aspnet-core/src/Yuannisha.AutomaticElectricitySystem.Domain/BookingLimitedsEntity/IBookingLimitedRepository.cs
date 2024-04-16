using Volo.Abp.Domain.Repositories;

namespace Yuannisha.AutomaticElectricitySystem.BookingLimitedsEntity
{
    public interface IBookingLimitedRepository : IBasicRepository<BookingLimited, Guid>
    {
        Task<List<BookingLimited>> GetListAsync(int maxResultCount = 10, int skipCount = 0);

        Task<long> GetCountAsync();
    }
}

