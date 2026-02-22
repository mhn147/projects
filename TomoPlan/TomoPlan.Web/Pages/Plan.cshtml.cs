using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TomoPlan.Web.Data.Repositories;
using TomoPlan.Web.ViewModels;

namespace TomoPlan.Web.Pages;

[Authorize]
public class Plan(DailyPlansRepository dailyPlanRepo, UserManager<IdentityUser> userManager) : PageModel
{
    private readonly DailyPlansRepository _dailyPlanRepo = dailyPlanRepo;
    private readonly UserManager<IdentityUser> _userManager = userManager;

    public DailyPlanViewModel DailyPlanViewModel { get; set; } = new DailyPlanViewModel();

    /*
     * Day
     * Date
     * If tomorrow: IsTomo=true
     * If today: today=true
     *
     * If user has one already use it
     * Otherwise create new one
     */
    
    public async Task OnGet(string date)
    {
        var userId = Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty);
        var dateOnly = DateOnly.Parse(date, CultureInfo.InvariantCulture);

        var dailyPlan = await _dailyPlanRepo.Get(userId, dateOnly);

        if (dailyPlan == null)
        {
            dailyPlan = await _dailyPlanRepo.Create(dateOnly, userId);
        }

        DailyPlanViewModel = new DailyPlanViewModel
        {
            Date = dailyPlan.Date,
            DayOfWeek = dailyPlan.Date.DayOfWeek.ToString(),
            DayBadge = DayBadge.Tomorrow, // TODO
            IsReadOnly = false
        };
    }
}