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

    public async Task<DailyPlan> GetOrCreatePlan(Guid userId, DateTime date)
    {
        var plan = await _repo.GetPlan(userId, date) ?? await _repo.AddPlan(userId, date);
        return plan;
    }

    public async Task<DailyPlan> AddTask(Guid planId, TimeOnly startTime, TimeOnly endTime, string body, string title)
    {
        await _repo.AddTask(planId, startTime, endTime, body, title);

        var x = await _repo.GetPlan(planId);

        return x;
    }
}
