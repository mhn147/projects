using TomoPlan.Web.Data.Entities;
using TomoPlan.Web.Data.Repositories;
using TomoPlan.Web.ViewModels;

namespace TomoPlan.Web.Core;

public class DailyPlansService(DailyPlansRepository dailyPlanRepo)
{
    private readonly DailyPlansRepository _dailyPlanRepo = dailyPlanRepo;

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
        return await _dailyPlanRepo.Update(dailyPlan);
    }
}