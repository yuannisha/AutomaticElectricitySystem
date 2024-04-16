using Yuannisha.AutomaticElectricitySystem.DailyTotalConsumptionEntity;
using Yuannisha.AutomaticElectricitySystem.DailyTotalConsumptionIAppservice;
using Yuannisha.AutomaticElectricitySystem.DailyTotalConsumptionShared;

namespace Yuannisha.AutomaticElectricitySystem.DailyTotalConsumptionsAppservice;

public class DailyTotalConsumptionAppservice : IDailyTotalConsumptionAppservice
{
    private readonly DailyTotalConsumptionManager _dailyTotalConsumptionManager;

    public DailyTotalConsumptionAppservice(
        DailyTotalConsumptionManager dailyTotalConsumptionManager
    )
    {
        _dailyTotalConsumptionManager = dailyTotalConsumptionManager;
    }
    public async Task<GetAllDailyTotalConsumptionOutput> GetAllDailyTotalConsumption()
    {
        return await _dailyTotalConsumptionManager.GetAllDailyTotalConsumption();
    }

    public async Task CreateDailyTotalConsumption(CreateDailyTotalConsumptionInput input)
    {
        await _dailyTotalConsumptionManager.CreateDailyTotalConsumption(input);
    }

    public async Task<AllDailyTotalConsumptionDto> UpdateDailyTotalConsumption(AllDailyTotalConsumptionDto input)
    {
        return await _dailyTotalConsumptionManager.UpdateDailyTotalConsumption(input);
    }

    public async Task DeleteDailyTotalConsumption(Guid id)
    {
        await _dailyTotalConsumptionManager.DeleteDailyTotalConsumption(id);
    }

    public async Task<GetDailyTotalConsumptionWithLastSevenDaysOutput> GetDailyTotalConsumptionWithLastSevenDays()
    {
        return await _dailyTotalConsumptionManager.GetDailyTotalConsumptionWithLastSevenDays();
    }
}