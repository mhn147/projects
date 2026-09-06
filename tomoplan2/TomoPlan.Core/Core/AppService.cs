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

    public async Task<DailyPlan> GetOrCreatePlan(Guid userId, DateTimeOffset date)
    {
        // todo: fix comparison make it only for the date
        var x = await _repo.GetPlan(userId, date);

        if (x == null)
        {
            x = await _repo.AddPlan(userId, date);
        }

        return x;
    }
}
