using Microsoft.EntityFrameworkCore;
using TomoPlan.Web.Data.Entities;

namespace TomoPlan.Web.Data.Repositories;

public class DailyPlansRepository(AppDbContext context)
{
    public async Task<DailyPlan?> GetPlan(Guid ownerId, DateOnly date)
    {
        // TODO: do not load all in memory. figure out EF Core issue with dates and load just the one
        return await context.DailyPlans
            .Include(dp => dp.Tasks)
            .AsAsyncEnumerable()
            .FirstOrDefaultAsync(dp => 
                dp.OwnerId == ownerId &&
                dp.Date.DateTime == date.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc));
    }

    public async Task<DailyPlan?> GetPlanById(Guid planId)
    {
        return await context.DailyPlans
            .Include(dp => dp.Tasks)
            .FirstOrDefaultAsync(dp => dp.Id == planId);
    }

    public async Task DeleteTask(DailyPlan plan, DailyPlanTask task)
    {
        plan.Tasks.Remove(task);
        await context.SaveChangesAsync();
    }

    public async Task<DailyPlan> Create(Guid ownerId, DateOnly date)
    {
        var newPlan = new DailyPlan
        {
            Date = new DateTimeOffset(date.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc)),
            OwnerId = ownerId
        };
        
        await context.DailyPlans.AddAsync(newPlan);
        await context.SaveChangesAsync();

        return newPlan;
    }
    
    public async Task<DailyPlan> Update(DailyPlan plan)
    {
        context.DailyPlans.Update(plan);
        await context.SaveChangesAsync();
        return plan;
    }
}