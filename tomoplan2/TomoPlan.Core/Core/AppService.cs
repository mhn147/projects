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
        var plan = await _repo.GetPlan(userId, date) ?? await _repo.AddPlan(userId, date);
        return plan;
    }

    public async Task<DailyPlan> AddTask(Guid planId, TimeOnly startTime, TimeOnly endTime, string body)
    {
        await _repo.AddTask(planId, startTime, endTime, body);

        var x = await _repo.GetPlan(planId);

        return x;
    }
}
