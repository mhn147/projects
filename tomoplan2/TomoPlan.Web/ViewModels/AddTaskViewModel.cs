namespace TomoPlan.Web.ViewModels;

public class AddTaskViewModel
{
    public Guid PlanId { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public string Body { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
}