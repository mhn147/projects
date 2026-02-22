namespace TomoPlan.Web.Data.Entities;

public class DailyPlanTask
{
    public Guid Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public TimeOnly Start { get; set; }
    public TimeOnly End { get; set; }
    public bool IsComplete { get; set; }
}