namespace TomoPlan.Core.Data.Entities;

public class DailyTask
{
    public Guid Id { get; set; }
    public Guid DailyPlanId { get; set; }
    public string Text { get; set; } = string.Empty;
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    public DateTimeOffset UpdatedAt { get; set; }
    public bool IsComplete { get; set; }
}
