using Volo.Abp.Domain.Repositories;

namespace Yuannisha.AutomaticElectricitySystem.BookingInformationsEntity;

public interface IBookingInformationRepository : IBasicRepository<BookingInformation, Guid>
{
    Task<List<BookingInformation>> GetListAsync(int maxResultCount = 10, int skipCount = 0);

    Task<long> GetCountAsync();
}