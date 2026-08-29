namespace TomoPlan.Core.Data.Entities;

public class DailyPlan
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateOnly Date { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset UpdatedAt { get; set; }
    public bool IsComplete { get; set; }
    public List<DailyTask> Tasks { get; set; } = new();
}