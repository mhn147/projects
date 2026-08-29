using MySqlConnector;
using TomoPlan.Core.Data;
using TomoPlan.Core.Data.Entities;

namespace TomoPlan.Core.Core;

public class AppService
{
    private readonly AppRepository _repo;

    public AppService(AppRepository repo)
    {
        _repo = repo;
    }

    public async Task<DailyPlan[]> GetPlan(Guid userId, DateOnly date)
    {
        var x = await GetOrCreatePlan(userId, date);
        var tomo = await GetOrCreatePlan(userId, date.AddDays(1));
        var yesterday = await GetOrCreatePlan(userId, date.AddDays(-1));

        return [yesterday, x, tomo];
    }

    private async Task<DailyPlan> GetOrCreatePlan(Guid userId, DateOnly date)
    {
        var x = await _repo.GetPlan(userId, date);

        if (x == null)
        {
            x = await _repo.AddPlan(userId, date);
        }

        return x;
    }
}
