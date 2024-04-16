using Abp.Extensions;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Yuannisha.AutomaticElectricitySystem.BookingLimitedsIAppservice;
using Yuannisha.AutomaticElectricitySystem.BookingLimitedsShared;

namespace Yuannisha.AutomaticElectricitySystem.BookingLimitedsEntity
{
    public class BookingLimitedManager : DomainService
    {
        private readonly IBookingLimitedRepository _bookingLimitedRepository;
        private readonly IObjectMapper _objectMapper;
        private readonly IRepository<BookingLimited, Guid> _repositoryForBookingLimited;

        public BookingLimitedManager(IBookingLimitedRepository bookingLimitedRepository, 
            IObjectMapper objectMapper, IRepository<BookingLimited,Guid> repositoryForBookingLimited)
        {
            _bookingLimitedRepository = bookingLimitedRepository;
            _objectMapper = objectMapper;
            _repositoryForBookingLimited = repositoryForBookingLimited;
        }

        public async Task<PageBookingLimitedOutput> GetListAsync(PageBookingLimitedInput input)
        {
            
            var queryable =await _repositoryForBookingLimited.GetQueryableAsync();
            var query = queryable.WhereIf(
                    !AbpStringExtensions.IsNullOrEmpty(input.StudentId), x => x.StudentId.Contains(input.StudentId))
                .WhereIf(
                    !AbpStringExtensions.IsNullOrEmpty(input.StudentName),
                    x => x.StudentName.Contains(input.StudentName))
                .WhereIf(
                    !AbpStringExtensions.IsNullOrEmpty(input.Date), x => x.Date.Contains(input.Date))
                .WhereIf(
                    ComparableExtensions.IsBetween(input.BookedHours, 1, 2),
                    x => x.BookedHours.Equals(input.BookedHours));
            
            var totalCount = await AsyncExecuter.CountAsync(query);

            var items = await AsyncExecuter.ToListAsync(query
                .OrderByDescending(booking => booking.CreationTime) // 示例：根据创建时间排序
                .Skip(input.SkipCount)
                .Take(input.PageSize));
            var res1 = _objectMapper.Map<List<BookingLimited>, List<BookingLimitedDto>>(items);
            return new PageBookingLimitedOutput() { BookingLimitedDto = res1, TotalCount = totalCount };


            // var list = await _bookingLimitedRepository.GetListAsync(maxResultCount, skipCount);
            // return _objectMapper.Map<List<BookingLimited>, List<BookingLimitedDto>>(list);
        }

        public async Task<long> GetCountAsync()
        {
            return await _bookingLimitedRepository.GetCountAsync();
        }

        /// <summary>
        /// 创建预约限制信息
        /// </summary>
        public async Task<BookingLimitedDto> CreateAsync(
            Guid id,
            string studentId,
            string studentName,
            string date,
            int bookedHours
        )
        {
            var entity = new BookingLimited(id, studentId, studentName, date, bookedHours);
            entity = await _bookingLimitedRepository.InsertAsync(entity);
            return _objectMapper.Map<BookingLimited, BookingLimitedDto>(entity);
        }

        /// <summary>
        /// 更新预约限制信息
        /// </summary>
        public async Task<BookingLimitedDto> UpdateAsync(
            Guid id,
            string studentId,
            string studentName,
            string date,
            int bookedHours
        )
        {
            var entity = await _bookingLimitedRepository.FindAsync(id) ?? throw new UserFriendlyException($"预约限制信息不存在");
            entity.Update(studentId, studentName, date, bookedHours);
            entity = await _bookingLimitedRepository.UpdateAsync(entity);
            return _objectMapper.Map<BookingLimited, BookingLimitedDto>(entity);
        }

        /// <summary>
        /// 删除预约限制信息
        /// </summary>
        public async Task DeleteAsync(Guid id)
        {
            var entity = await _bookingLimitedRepository.FindAsync(id) ?? throw new UserFriendlyException($"预约限制信息不存在");
            await _bookingLimitedRepository.DeleteAsync(entity);
        }
    }
}

