namespace TomoPlan.Web.ViewModels;

public enum DayBadge
{
    Tomorrow,
    Today
}

public class TaskViewModel
{
    public Guid Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public TimeOnly Start { get; set; }   
    public TimeOnly End { get; set; }
    public DateTimeOffset Date { get; set; }
}

public class PlanViewModel
{
    public Guid Id { get; set; }
    public string DayOfWeek { get; set; } = string.Empty;
    public DateTimeOffset Date { get; set; }
    public DayBadge DayBadge { get; set; }
    public bool IsReadOnly { get; set; }
    public List<TaskViewModel> Tasks { get; set; } = new List<TaskViewModel>();

    public TaskViewModel NewTask { get; set; } = new();
}