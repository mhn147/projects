namespace TomoPlan.Web.Data.Entities;

public class DailyPlan
{
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public DateTimeOffset Date { get; set; }
    public bool IsComplete { get; set; }
    public short Rating { get; set; }
    public ICollection<DailyPlanTask> Tasks { get; set; } = new List<DailyPlanTask>();
}