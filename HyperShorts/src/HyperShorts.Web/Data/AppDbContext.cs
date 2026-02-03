using Microsoft.EntityFrameworkCore;

namespace HyperShorts.Web.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<HyperShort> HyperShorts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HyperShort>(entity =>
        {
            entity.HasIndex(x => x.Code);
        });
    }
}
