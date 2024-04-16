using Yuannisha.AutomaticElectricitySystem.DailyTotalConsumptionShared;

namespace Yuannisha.AutomaticElectricitySystem.DailyTotalConsumptionIAppservice;

public interface IDailyTotalConsumptionAppservice
{
    public Task<GetAllDailyTotalConsumptionOutput> GetAllDailyTotalConsumption();
    public Task CreateDailyTotalConsumption(CreateDailyTotalConsumptionInput input);
    public Task<AllDailyTotalConsumptionDto> UpdateDailyTotalConsumption(AllDailyTotalConsumptionDto input);
    public Task DeleteDailyTotalConsumption(Guid id);
    public Task<GetDailyTotalConsumptionWithLastSevenDaysOutput> GetDailyTotalConsumptionWithLastSevenDays();
}