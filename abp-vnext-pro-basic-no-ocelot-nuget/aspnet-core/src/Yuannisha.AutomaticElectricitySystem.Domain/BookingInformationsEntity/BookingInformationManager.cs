using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Yuannisha.AutomaticElectricitySystem.BookingInformationsShared;

namespace Yuannisha.AutomaticElectricitySystem.BookingInformationsEntity
{
    public class BookingInformationManager : DomainService
    {
        private readonly IBookingInformationRepository _bookingInformationRepository;
        private readonly IObjectMapper _objectMapper;
        private readonly IRepository<BookingInformation, Guid> _repositoryForbookingInformation;

        public BookingInformationManager(IBookingInformationRepository bookingInformationRepository, 
            IObjectMapper objectMapper,
            IRepository<BookingInformation, Guid> repositoryForbookingInformation
            )
        {
            _bookingInformationRepository = bookingInformationRepository;
            _objectMapper = objectMapper;
            _repositoryForbookingInformation = repositoryForbookingInformation;
        }

        public async Task<BookingInforDto> GetListAsync(GetAllBookingInforInput input)
        {
            var queryable =await _repositoryForbookingInformation.GetQueryableAsync();
            var query = queryable.WhereIf<BookingInformation, IQueryable<BookingInformation>>(
                !input.StudentId.IsNullOrEmpty(), x => x.StudentId.Contains(input.StudentId)).WhereIf(
                !input.StudentClass.IsNullOrEmpty(), x => x
                    .StudentClass.Contains(input.StudentClass)).WhereIf(!input.CreationTime.IsNullOrEmpty(), x => x
                .CreationTime.ToString().Contains(input.CreationTime)).WhereIf(!input.StudentName.IsNullOrEmpty(), x =>
                x.StudentName.Contains(input.StudentName)).WhereIf(!input.UsingClassroom.IsNullOrEmpty(), x => x
                .UsingClassroom.Contains(input.UsingClassroom)).WhereIf(!input.BookingTimespan.IsNullOrEmpty(), x => x
                .BookingTimespan.Contains(input.BookingTimespan));
            
            var totalCount = await AsyncExecuter.CountAsync(query);

            var items = await AsyncExecuter.ToListAsync(query
                .OrderByDescending(booking => booking.CreationTime) // 示例：根据创建时间排序
                .Skip(input.SkipCount)
                .Take(input.PageSize));
            var res1 = _objectMapper.Map<List<BookingInformation>, List<BookingInformationDto>>(items);
            return new BookingInforDto() { BookingInformationDtoList = res1,TotalCount = totalCount};
                
        }

        public async Task<BookingInformation> GetAsync(Guid x)
        {
            return await _bookingInformationRepository.GetAsync(x);
        }

        public async Task<long> GetCountAsync()
        {
            return await _bookingInformationRepository.GetCountAsync();
        }

        /// <summary>
        /// 创建预约信息
        /// </summary>
        public async Task<BookingInformationDto> CreateAsync(
            Guid id,
            string studentId,
            string studentName,
            string studentClass,
            string telephoneNumber,
            string usingClassroom,
            string usingPurpose,
            string bookingTimespan
        )
        {
            var entity = new BookingInformation(id, studentId, studentName, studentClass, telephoneNumber, usingClassroom, usingPurpose, bookingTimespan);
            entity = await _bookingInformationRepository.InsertAsync(entity);
            return _objectMapper.Map<BookingInformation, BookingInformationDto>(entity);
        }

        /// <summary>
        /// 更新预约信息
        /// </summary>
        public async Task<BookingInformationDto> UpdateAsync(
            BookingInformation booking
        )
        {
            var entity = await _bookingInformationRepository.FindAsync(booking.Id) ?? throw new UserFriendlyException($"预约信息不存在");
            entity.Update(booking.StudentId, booking.StudentName, booking.StudentClass, booking.TelephoneNumber, booking.UsingClassroom, booking.UsingPurpose, booking.BookingTimespan);
            entity = await _bookingInformationRepository.UpdateAsync(entity);
            return _objectMapper.Map<BookingInformation, BookingInformationDto>(entity);
        }

        /// <summary>
        /// 删除预约信息
        /// </summary>
        public async Task DeleteAsync(Guid id)
        {
            var entity = await _bookingInformationRepository.FindAsync(id) ?? throw new UserFriendlyException($"预约信息不存在");
            await _bookingInformationRepository.DeleteAsync(entity);
        }

        public async Task BatchDeleteAsync(EntityDto<List<Guid>> input)
        {
            //var dbContext = await GetDbContextAsync();
            //var persons = await dbContext.Persons.Where(person => ids.Contains(person.Id)).ToListAsync();
            //dbContext.Persons.RemoveRange(persons);
            //await dbContext.SaveChangesAsync();
            await _bookingInformationRepository.DeleteManyAsync(input.Id);
        }

        public async Task<List<BookingInformationDto>> GetAll()
        {
            var res = await _repositoryForbookingInformation.GetListAsync();
            return _objectMapper.Map<List<BookingInformation>, List<BookingInformationDto>>(res);
        }
    }
}

