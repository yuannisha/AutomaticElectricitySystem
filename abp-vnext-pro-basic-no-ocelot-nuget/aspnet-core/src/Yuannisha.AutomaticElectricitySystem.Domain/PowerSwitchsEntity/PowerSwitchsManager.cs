using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Yuannisha.AutomaticElectricitySystem.PowerSwitchsIAppservice;
using Yuannisha.AutomaticElectricitySystem.PowerSwitchsShared;

namespace Yuannisha.AutomaticElectricitySystem.PowerSwitchsEntity
{
    public class PowerSwitchsManager : DomainService
    {
        private readonly IPowerSwitchsRepository _powerSwitchsRepository;
        private readonly IObjectMapper _objectMapper;
        private readonly IRepository<PowerSwitchsEntity.PowerSwitchs,Guid> _repositoryOfPowerSwitchs;

        public PowerSwitchsManager(IPowerSwitchsRepository powerSwitchsRepository, 
            IObjectMapper objectMapper,
            IRepository<PowerSwitchsEntity.PowerSwitchs,Guid> repositoryOfPowerSwitchs
            )
        {
            _powerSwitchsRepository = powerSwitchsRepository;
            _objectMapper = objectMapper;
            _repositoryOfPowerSwitchs = repositoryOfPowerSwitchs;
        }

        public async Task<PagePowerSwitchsOutput> GetListAsync(PagePowerSwitchsInput input)
        {
            var queryable =await _repositoryOfPowerSwitchs.GetQueryableAsync();
            var query = queryable.WhereIf(
                !AbpStringExtensions.IsNullOrEmpty(input.RoomId), x => x.RoomId.ToString().Equals(input
                    .RoomId)).WhereIf(
                !AbpStringExtensions.IsNullOrEmpty(input.SerialNumber), x => x.SerialNumber.Contains(input
                    .SerialNumber)).WhereIf(
                !AbpStringExtensions.IsNullOrEmpty(input.ControlledMachineName), x => x.ControlledMachineName.Contains
                    (input.ControlledMachineName)).WhereIf(!input.IsOnline.Equals(IsOnline.DefaultValue), x => x.IsOnline.Equals
                (input
                    .IsOnline)).WhereIf(!input.Status.Equals(Status.DefaultValue), x => x.Status.Equals(input
                    .Status)).WhereIf(
                !AbpStringExtensions.IsNullOrEmpty(input.CreationTime), x => x.CreationTime.ToString().Contains(input
                    .CreationTime)).WhereIf(!input.IsAbnormal.Equals(IsAbnormalOrNot.DefaultValue), x => x
                .IsAbnormal.Equals(input.IsAbnormal));
            var totalCount = await AsyncExecuter.CountAsync(query);

            var items = await AsyncExecuter.ToListAsync(query
                .OrderByDescending(b => b.CreationTime) // 示例：根据创建时间排序
                .Skip(input.SkipCount)
                .Take(input.PageSize));
            var res1 = _objectMapper.Map<List<PowerSwitchsEntity.PowerSwitchs>, List<PowerSwitchsDto>>(items);
            return new PagePowerSwitchsOutput() { PowerSwitchsDto = res1,TotalCount = totalCount};


            // var list = await _powerSwitchsRepository.GetListAsync(maxResultCount, skipCount);
            // return _objectMapper.Map<List<PowerSwitchs>, List<PowerSwitchsDto>>(list);
        }

        public async Task<long> GetCountAsync()
        {
            return await _powerSwitchsRepository.GetCountAsync();
        }

        /// <summary>
        /// 创建智能空开
        /// </summary>
        public async Task<PowerSwitchsDto> CreateAsync(
            Guid id,
            Guid roomId,
            string serialNumber,
            string controlledMachineName,
            IsOnline isOnline,
            Status status
        )
        {
            var isExist  = await _repositoryOfPowerSwitchs.AnyAsync(switchs => switchs.SerialNumber== serialNumber ||
                switchs.ControlledMachineName == controlledMachineName);
            if (isExist)
            {
                // 如果存在，返回提示信息或抛出一个异常
                throw new UserFriendlyException("空开已存在，请勿重复创建。");
            }

            if (serialNumber.Length != 12 || serialNumber.Contains(" "))
            {
                // 如果存在，返回提示信息或抛出一个异常
                throw new UserFriendlyException("序列号长度为12位数字且不包含空格！");
            }
            var entity = new PowerSwitchsEntity.PowerSwitchs(id, roomId, serialNumber, controlledMachineName, isOnline, status);
            entity = await _powerSwitchsRepository.InsertAsync(entity);
            return _objectMapper.Map<PowerSwitchsEntity.PowerSwitchs, PowerSwitchsDto>(entity);
        }

        /// <summary>
        /// 更新智能空开
        /// </summary>
        public async Task<PowerSwitchsDto> UpdateAsync(
            Guid id,
            Guid roomId,
            string serialNumber,
            string controlledMachineName,
            IsOnline isOnline,
            Status status,
            IsAbnormalOrNot isAbnormalOrNot,
            double energyConsumption
        )
        {
            var isExist  = await _repositoryOfPowerSwitchs.AnyAsync(switchs => switchs.Id != id && 
                                                                               (switchs.SerialNumber == serialNumber ||
                                                                                   switchs.ControlledMachineName == controlledMachineName));
            if (isExist)
            {
                // 如果存在，返回提示信息或抛出一个异常
                throw new UserFriendlyException("空开序列号或控制器械名已存在，请勿重复更新数据。");
            }
            if (serialNumber.Length != 12 || serialNumber.Contains(" "))
            {
                // 如果存在，返回提示信息或抛出一个异常
                throw new UserFriendlyException("序列号长度为12位数字且不包含空格！");
            }
            var entity = await _powerSwitchsRepository.FindAsync(id) ?? throw new UserFriendlyException($"智能空开不存在");
            entity.Update(roomId, serialNumber, controlledMachineName, isOnline, status ,isAbnormalOrNot, energyConsumption);
            entity = await _powerSwitchsRepository.UpdateAsync(entity);
            return _objectMapper.Map<PowerSwitchsEntity.PowerSwitchs, PowerSwitchsDto>(entity);
        }

        /// <summary>
        /// 删除智能空开
        /// </summary>
        public async Task DeleteAsync(Guid id)
        {
            var entity = await _powerSwitchsRepository.FindAsync(id) ?? throw new UserFriendlyException($"智能空开不存在");
            await _powerSwitchsRepository.DeleteAsync(entity);
        }
        
        /// <summary>
        /// 无条件获取所有智能空开
        /// </summary>
        public async Task<List<PowerSwitchsDto>> GetAllPowerSwitchs()
        {
            var powerSwitchs = await _repositoryOfPowerSwitchs.GetListAsync();
            return _objectMapper.Map<List<PowerSwitchsEntity.PowerSwitchs>,List<PowerSwitchsDto>>(powerSwitchs);
        }
        
    }
}

