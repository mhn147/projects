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
        var dailyPlan = await _repo.GetPlan(ownerId, date);

        if (dailyPlan == null)
        {
            dailyPlan = await _repo.Create(ownerId, date);
        }

        return dailyPlan;
    }

    public async Task<DailyPlan?> GetPlan(Guid ownerId, DateOnly date)
    {
        var dailyPlan = await _repo.GetPlan(ownerId, date);
        return dailyPlan;
    }

    public async Task<DailyPlan?> DeleteTask(Guid planId, Guid taskId)
    {
        var plan = await _repo.GetPlanById(planId);
        var task = plan?.Tasks.FirstOrDefault(t => t.Id == taskId);
        if (task != null && plan != null)
        {
            await _repo.DeleteTask(plan, task);
        }
        return plan;
    }

    public bool TimeBlockConflict(DailyPlan dailyPlan, DayTaskViewModel newTask)
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
    
    public async Task<DailyPlan> AddTimeBlock(DailyPlan dailyPlan, DayTaskViewModel newTask)
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