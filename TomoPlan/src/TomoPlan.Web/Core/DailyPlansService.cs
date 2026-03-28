using TomoPlan.Web.Data.Entities;
using TomoPlan.Web.Data.Repositories;
using TomoPlan.Web.ViewModels;

namespace TomoPlan.Web.Core;

public class DailyPlansService
{
    private readonly DailyPlansRepository _repo;

    public DailyPlansService(DailyPlansRepository repo)
    {
        _repo = repo;
    }

    public DateOnly GetDateToPlan(DateTime? date)
    {
        if (date != null)
        {
            return DateOnly.FromDateTime(date.Value);
        }

        // TODO: use the user's local timezone
        var now = DateTime.Now;
        return now.Hour >= 22
            ? DateOnly.FromDateTime(now.AddDays(1))
            : DateOnly.FromDateTime(now);
    }

    public async Task<DailyPlan> GetOrCreatePlan(Guid ownerId, DateOnly date)
    {
        var dailyPlan = await _repo.Get(ownerId, date);

        if (dailyPlan == null)
        {
            dailyPlan = await _repo.Create(ownerId, date);
        }

        return dailyPlan;
    }

    public async Task<DailyPlan?> GetPlan(Guid ownerId, DateOnly date)
    {
        var dailyPlan = await _repo.Get(ownerId, date);
        return dailyPlan;
    }

    public bool TimeBlockConflict(DailyPlan dailyPlan, DailyTaskViewModel newTask)
    {
        if (dailyPlan.Tasks.Count == 0)
        {
            return false;
        }
        
        foreach (var task in dailyPlan.Tasks)
        {
            if (newTask.Start < task.End)
            {
                return true;
            }
        }
        
        return false;
    }
    
    public async Task<DailyPlan> AddTimeBlock(DailyPlan dailyPlan, DailyTaskViewModel newTask)
    {
        dailyPlan.Tasks.Add(new DailyPlanTask
        {
            Start = newTask.Start,
            End = newTask.End,
            Text = newTask.Text
        });
        return await _repo.Update(dailyPlan);
    }
}