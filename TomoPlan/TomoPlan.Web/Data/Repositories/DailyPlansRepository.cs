using Microsoft.EntityFrameworkCore;
using TomoPlan.Web.Data.Entities;

namespace TomoPlan.Web.Data.Repositories;

public class DailyPlansRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<DailyPlan?> Get(Guid ownerId, DateOnly date)
    {
        // TODO: do not load all in memory. figure out EF Core issue with dates and load just the one
        return await _context.DailyPlans
            .AsAsyncEnumerable()
            .FirstOrDefaultAsync(dp => 
                dp.OwnerId == ownerId &&
                dp.Date.DateTime == date.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc));
    }

    public async Task<DailyPlan> Create(DateOnly date, Guid ownerId)
    {
        var newPlan = new DailyPlan
        {
            Date = new DateTimeOffset(date.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc)),
            OwnerId = ownerId
        };
        
        await _context.DailyPlans.AddAsync(newPlan);
        await _context.SaveChangesAsync();

        return newPlan;
    }
}