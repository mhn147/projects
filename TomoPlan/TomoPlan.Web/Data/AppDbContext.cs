using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TomoPlan.Web.Data.Entities;

namespace TomoPlan.Web.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext(options)
{
    public DbSet<DailyPlan> DailyPlans { get; set; }
}