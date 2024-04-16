using NUglify.Helpers;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Linq;
using Yuannisha.AutomaticElectricitySystem.DailyTotalConsumptionShared;

namespace Yuannisha.AutomaticElectricitySystem.DailyTotalConsumptionEntity;

public class DailyTotalConsumptionManager : DomainService
{
    private readonly IRepository<DailyTotalConsumption, Guid> _dailyTotalConsumptionRepository;
    private readonly IObjectMapper _objectMapper;
    private readonly IGuidGenerator _guidGenerator;
    private readonly IAsyncQueryableExecuter _asyncQueryableExecuter;

    public DailyTotalConsumptionManager(
        IRepository<DailyTotalConsumption, Guid> dailyTotalConsumptionRepository,
        IObjectMapper objectMapper,
        IGuidGenerator guidGenerator,
        IAsyncQueryableExecuter asyncQueryableExecuter
        )
    {
        _dailyTotalConsumptionRepository = dailyTotalConsumptionRepository;
        _objectMapper = objectMapper;
        _guidGenerator = guidGenerator;
        _asyncQueryableExecuter = asyncQueryableExecuter;
    }
    
    public async Task<GetAllDailyTotalConsumptionOutput> GetAllDailyTotalConsumption()
    {
        // throw new UserFriendlyException("test");
        var list = await _dailyTotalConsumptionRepository.GetListAsync();
        var res = _objectMapper.Map<List<DailyTotalConsumption>, List<AllDailyTotalConsumptionDto>>(list);
        return new GetAllDailyTotalConsumptionOutput() { AllDailyTotalConsumptionOutput = res};
    }
    
    public async Task CreateDailyTotalConsumption(CreateDailyTotalConsumptionInput input)
    {
        await _dailyTotalConsumptionRepository.InsertAsync(new DailyTotalConsumption()
        {
            Id = _guidGenerator.Create(),
            Date    = input.Date,
            PowerConsumption = input.PowerConsumption
                
        });
        // await _repositoryOfConsumptionAmount.InsertAsync(new ConsumptionAmount
        // {
        //     Id =_guidGenerator.Create(),
        //     Date=input.Date,
        //     PowerConsumption = input.PowerConsumption
        // });
    }

    public async Task<AllDailyTotalConsumptionDto> UpdateDailyTotalConsumption(AllDailyTotalConsumptionDto input)
    {
        var entity = await _dailyTotalConsumptionRepository.GetAsync(input.Id) ?? throw new UserFriendlyException
            ($"楼栋消耗数据不存在");
        entity.Update(input.Date,input.PowerConsumption);
        entity = await _dailyTotalConsumptionRepository.UpdateAsync(entity);
        return  _objectMapper.Map<DailyTotalConsumption,AllDailyTotalConsumptionDto>(entity);
    }
    
    public async Task DeleteDailyTotalConsumption(Guid id)
    {
        var entity = await _dailyTotalConsumptionRepository.GetAsync(id)?? throw new UserFriendlyException
            ($"楼栋消耗数据不存在");
        await _dailyTotalConsumptionRepository.DeleteAsync(entity);
    }
    
    public async Task<GetDailyTotalConsumptionWithLastSevenDaysOutput> GetDailyTotalConsumptionWithLastSevenDays()
    {
        var dates = GetLastSevenDays();
        Dictionary<string, double> TotalConsumptionWithLastSevenDays = new Dictionary<string, double>();
        dates.ForEach(x =>
        {
            TotalConsumptionWithLastSevenDays[x] = 0;
        });
        var alldatas = await GetAllDailyTotalConsumption();
        var list =new List<AllDailyTotalConsumptionDto>();
        foreach (var date in dates)
        {
            alldatas.AllDailyTotalConsumptionOutput.ForEach(x =>
            {
                if (x.Date.Equals(date))
                    list.Add(x);
            });
        }
        
        list.ForEach(x =>
        {
            TotalConsumptionWithLastSevenDays[x.Date] = x.PowerConsumption;
        });
        
        return new GetDailyTotalConsumptionWithLastSevenDaysOutput(){Dates = TotalConsumptionWithLastSevenDays.Keys
            .ToList(),PowerConsumption = TotalConsumptionWithLastSevenDays.Values.ToList()};
    }
    
    private List<string> GetLastSevenDays()
    {
        var datesList = new List<string>();
        var currentDate = DateTime.Now.AddDays(-1);

        for (int i = 0; i < 7; i++)
        {
            var date = currentDate.AddDays(-i);
            var formattedDate = date.ToString("yyyy/MM/dd");
            datesList.Add(formattedDate);
        }

        datesList.Reverse(); // Reverse to have the oldest date first
        return datesList;
    }
}