using NUglify.Helpers;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Yuannisha.AutomaticElectricitySystem.PowerSwitchsEntity;
using Yuannisha.AutomaticElectricitySystem.RoomsIAppservice;
using Yuannisha.AutomaticElectricitySystem.RoomsShared;

// using Volo.Abp.Domain.Repositories;

namespace Yuannisha.AutomaticElectricitySystem.RoomsEntity
{
    public class RoomsManager : DomainService
    {
        private readonly IRoomsRepository _roomsRepository;
        private readonly IObjectMapper _objectMapper;
        private readonly IRepository<Rooms, Guid> _roomRepository;
        private readonly IRepository<PowerSwitchs,Guid> _repositoryOfPowerSwitchs;

        public RoomsManager(IRoomsRepository roomsRepository,
            IObjectMapper objectMapper,
            IRepository<Rooms, Guid> roomRepository,
            IRepository<PowerSwitchs,Guid> repositoryOfPowerSwitchs
            )
        {
            _roomsRepository = roomsRepository;
            _objectMapper = objectMapper;
            _roomRepository = roomRepository;
            _repositoryOfPowerSwitchs = repositoryOfPowerSwitchs;
        }

        public async Task<PageRoomsOutput> GetListAsync(PageRoomsInput input)
        {
            var queryable =await _roomRepository.GetQueryableAsync();
            // var query = queryable.WhereIf(
            //     !AbpStringExtensions.IsNullOrEmpty(input.BuildingId), x => x.BuildingId.Equals(input
            //         .BuildingId)).WhereIf(
            //     !input.Floor.ToString().IsNullOrEmpty(), x => x.Floor.ToString().Equals(input
            //         .Floor.ToString())).WhereIf(
            //     !AbpStringExtensions.IsNullOrEmpty(input.No), x => x.No.Contains
            //         (input.No)).WhereIf(
            //     !AbpStringExtensions.IsNullOrEmpty(input.IsUsingOrNot), x => x.IsUsingOrNot.ToString().Equals(input
            //         .IsUsingOrNot)).WhereIf(
            //     !AbpStringExtensions.IsNullOrEmpty(input.RoomType), x => x.RoomType.ToString().Contains(input
            //         .RoomType)).WhereIf(
            //     !AbpStringExtensions.IsNullOrEmpty(input.ControlType), x => x.ControlType.ToString().Contains(input
            //         .ControlType));

            var query = queryable.WhereIf(
                !AbpStringExtensions.IsNullOrEmpty(input.BuildingId), x => x.BuildingId.Equals(input
                    .BuildingId)).WhereIf(
                !input.Floor.Equals(0), x => x.Floor.Equals(input.Floor)).WhereIf(
                !AbpStringExtensions.IsNullOrEmpty(input.No), x => x.No.Contains
                    (input.No)).WhereIf(
                !input.IsUsingOrNot.Equals(UsingOrNot.DefaultValue), x => x.IsUsingOrNot.Equals(input
                    .IsUsingOrNot)).WhereIf(
                !input.RoomType.Equals(RoomTypeEnum.DefaultValue), x => x.RoomType.Equals(input
                    .RoomType)).WhereIf(
                !input.ControlType.Equals(ControlTypeEnum.DefaultValue), x => x.ControlType.Equals(input
                    .ControlType));
                
            var totalCount = await AsyncExecuter.CountAsync(query);

            var items = await AsyncExecuter.ToListAsync(query
                .OrderByDescending(b => b.CreationTime) // 示例：根据创建时间排序
                .Skip(input.SkipCount)
                .Take(input.PageSize));
            var res1 = _objectMapper.Map<List<Rooms>, List<RoomList>>(items);
            return new PageRoomsOutput() { RoomList = res1, TotalCount = totalCount };


            // var list = await _roomsRepository.GetListAsync(maxResultCount, skipCount);
            // return _objectMapper.Map<List<Rooms>, List<RoomList>>(list);
        }

        public async Task<long> GetCountAsync()
        {
            return await _roomsRepository.GetCountAsync();
        }

        /// <summary>
        /// 创建教室
        /// </summary>
        public async Task<RoomsDto> CreateAsync(
            Guid id,
            Guid buildingId,
            int floor,
            string no,
            UsingOrNot usingOrNot,
            RoomTypeEnum roomType,
            ControlTypeEnum controlType
        )
        {
            var isExist  = await _roomRepository.AnyAsync(room=>room.No == no);
            if (isExist)
            {
                // 如果存在，返回提示信息或抛出一个异常
                throw new UserFriendlyException("教室已存在，请勿重复创建。");
            }
            var entity = new RoomsEntity.Rooms(id, buildingId, floor, no, usingOrNot, roomType, controlType);
            entity = await _roomsRepository.InsertAsync(entity);
            return _objectMapper.Map<RoomsEntity.Rooms, RoomsDto>(entity);
        }

        /// <summary>
        /// 更新教室
        /// </summary>
        public async Task<RoomsDto> UpdateAsync(
            Guid id,
            Guid buildingId,
            int floor,
            string no,
            UsingOrNot usingOrNot,
            RoomTypeEnum roomType,
            ControlTypeEnum controlType
        )
        {
            var entity = await _roomsRepository.FindAsync(id) ?? throw new UserFriendlyException($"教室不存在");
            var isExist  = await _roomRepository.AnyAsync(room=>room.Id != id && room.No == no);
            if (isExist)
            {
                // 如果存在，返回提示信息或抛出一个异常
                throw new UserFriendlyException("教室已存在，请勿重复更新数据。");
            }
            entity.Update(buildingId, floor, no, usingOrNot, roomType, controlType);
            entity = await _roomsRepository.UpdateAsync(entity);
            return _objectMapper.Map<Rooms, RoomsDto>(entity);
        }

        /// <summary>
        /// 删除教室
        /// </summary>
        public async Task DeleteAsync(Guid id)
        {
            var entity = await _roomsRepository.FindAsync(id) ?? throw new UserFriendlyException($"教室不存在");
            var isExist = await _repositoryOfPowerSwitchs.AnyAsync(switchs => switchs.RoomId == entity.Id);
            if (isExist)
            {
                // 如果存在，返回提示信息或抛出一个异常
                throw new UserFriendlyException("不能删除包含有空开的教室,请先删除在此教室的空开！");
            }
            await _roomsRepository.DeleteAsync(entity);
        }
        
        /// <summary>
        /// 无条件获取所有教室
        /// </summary>
        public async Task<List<GetAllClassroomsOutPutDto>> GetAllClassrooms()
        {
            var rooms = await _roomRepository.GetListAsync();
            return _objectMapper.Map<List<RoomsEntity.Rooms>,List<GetAllClassroomsOutPutDto>>(rooms);
        }

        public async Task<List<Rooms>>  TTTTT()
        {
            var ss = await _roomRepository.WithDetailsAsync(x => x.PowerSwitches);
            return ss.ToList();
            // return _roomRepository1111.GetAllIncluding(x => x.PowerSwitches).ToList();
        }
    }
}

