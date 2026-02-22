namespace TomoPlan.Web.ViewModels;

public enum DayBadge
{
    Tomorrow,
    Today
}

public class DailyTask
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public TimeOnly Start { get; set; }   
    public TimeOnly End { get; set; }   
}

public class DailyPlanViewModel
{
    public string DayOfWeek { get; set; } = string.Empty;
    public DateTimeOffset Date { get; set; }
    public DayBadge DayBadge { get; set; }
    public bool IsReadOnly { get; set; }
    public List<DailyTask> Tasks { get; set; } = new List<DailyTask>();
}