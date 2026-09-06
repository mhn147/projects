using TomoPlan.Core.Data.Entities;

namespace TomoPlan.Web.ViewModels;

public class DailyPlanViewModel
{
    public required DailyPlan Plan { get; set; }
    public bool SkipTomorrowRedirect { get; set; }
}