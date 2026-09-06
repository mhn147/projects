using TomoPlan.Core.Data.Entities;

namespace TomoPlan.Web.ViewModels;

public class DailyPlanViewModel
{
    public required DailyPlan Today { get; set; }
    public required DailyPlan Tomorrow { get; set; }
}