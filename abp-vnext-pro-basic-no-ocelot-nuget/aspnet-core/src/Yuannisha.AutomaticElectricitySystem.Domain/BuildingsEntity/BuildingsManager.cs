using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Yuannisha.AutomaticElectricitySystem.BuildingsIAppservice;
using Yuannisha.AutomaticElectricitySystem.BuildingsShared;
using Yuannisha.AutomaticElectricitySystem.RoomsEntity;

namespace Yuannisha.AutomaticElectricitySystem.BuildingsEntity
{
    public class BuildingsManager : DomainService
    {
        private readonly IBuildingsRepository _buildingsRepository;
        private readonly IObjectMapper _objectMapper;
        private readonly IRepository<BuildingsEntity.Buildings,Guid> _repositoryOfBuildings;
        private readonly IRepository<Rooms, Guid> _roomRepository;

        public BuildingsManager(IBuildingsRepository buildingsRepository,
            IObjectMapper objectMapper,
            IRepository<BuildingsEntity.Buildings,Guid> repositoryOfBuildings,
            IRepository<Rooms, Guid> roomRepository
            )
        {
            _buildingsRepository = buildingsRepository;
            _objectMapper = objectMapper;
            _repositoryOfBuildings = repositoryOfBuildings;
            _roomRepository = roomRepository;
        }

        public async Task<PageBuildingsOutput> GetListAsync(PageBuildingsInput input)
        {
            var queryable =await _repositoryOfBuildings.GetQueryableAsync();
            var query = queryable.WhereIf(
                    !AbpStringExtensions.IsNullOrEmpty(input.Name), x => x.Name.Contains(input.Name));
            
            var totalCount = await AsyncExecuter.CountAsync(query);

            var items = await AsyncExecuter.ToListAsync(query
                .OrderBy(b => b.DisplayOrder) // 示例：根据创建时间排序
                .Skip(input.SkipCount)
                .Take(input.PageSize));
            var res1 = _objectMapper.Map<List<BuildingsEntity.Buildings>, List<BuildingsDto>>(items);
            return new PageBuildingsOutput() { BuildingsDto = res1,TotalCount = totalCount};



            // var list = await _buildingsRepository.GetListAsync(maxResultCount, skipCount);
            // return _objectMapper.Map<List<Buildings>, List<BuildingsDto>>(list);
        }

        public async Task<long> GetCountAsync()
        {
            return await _buildingsRepository.GetCountAsync();
        }

        /// <summary>
        /// 创建教学楼
        /// </summary>
        public async Task<BuildingsDto> CreateAsync(
            Guid id,
            string name,
            int displayOrder
        )
        {
            var isExist  = await _repositoryOfBuildings.AnyAsync(buildings => buildings.Name == name);
            if (isExist)
            {
                // 如果存在，返回提示信息或抛出一个异常
                throw new UserFriendlyException("楼栋已存在，请勿重复创建。");
            }
            var entity = new BuildingsEntity.Buildings(id, name, displayOrder);
            entity = await _buildingsRepository.InsertAsync(entity);
            return _objectMapper.Map<BuildingsEntity.Buildings, BuildingsDto>(entity);
        }

        /// <summary>
        /// 更新教学楼
        /// </summary>
        public async Task<BuildingsDto> UpdateAsync(
            Guid id,
            string name,
            int displayOrder
        )
        {
            var entity = await _buildingsRepository.FindAsync(id) ?? throw new UserFriendlyException($"教学楼不存在");
            var isExist  = await _repositoryOfBuildings.AnyAsync(buildings => buildings.Id != id && (buildings.Name == 
                name) );
            if (isExist)
            {
                // 如果存在，返回提示信息或抛出一个异常
                throw new UserFriendlyException("楼栋已存在，请勿重复更新数据。");
            }
            
            entity.Update(name, displayOrder);
            entity = await _buildingsRepository.UpdateAsync(entity);
            return _objectMapper.Map<Buildings, BuildingsDto>(entity);
        }

        /// <summary>
        /// 删除教学楼
        /// </summary>
        public async Task DeleteAsync(Guid id)
        {
            var entity = await _buildingsRepository.FindAsync(id) ?? throw new UserFriendlyException($"教学楼不存在");
            // var entity = await _buildingsRepository.GetAsync(id);
            var isExist = await _roomRepository.AnyAsync(rooms => rooms.BuildingId == entity.Id);
            if (isExist)
            {
                // 如果存在，返回提示信息或抛出一个异常
                throw new UserFriendlyException("不能删除包含有教室的楼栋,请先删除在此教学楼的教室！");
            }
            await _buildingsRepository.DeleteAsync(entity);
        }
        
        /// <summary>
        /// 根据Id查询教学楼
        /// </summary>
        public async Task<GetBuildingOutPutDto> GetBuilding(Guid id)
        {
            // var op =  _objectMapper.Map<await _buildingsRepository.GetAsync(id),GetBuildingOutPutDto>();
            var building = await _buildingsRepository.GetAsync(id);
            return _objectMapper.Map<BuildingsEntity.Buildings,GetBuildingOutPutDto>(building);
            // await _buildingsRepository.GetAsync(id);
        }
        
        /// <summary>
        /// 无条件查询所有教学楼
        /// </summary>
        public async Task<List<GetBuildingOutPutDto>> GetAllBuildingsAsyncUnConditional()
        {
            // var op =  _objectMapper.Map<await _buildingsRepository.GetAsync(id),GetBuildingOutPutDto>();
            var buildings = await _repositoryOfBuildings.GetListAsync();
            return _objectMapper.Map<List<BuildingsEntity.Buildings>,List<GetBuildingOutPutDto>>(buildings);
            // await _buildingsRepository.GetAsync(id);
        }
    }
}

