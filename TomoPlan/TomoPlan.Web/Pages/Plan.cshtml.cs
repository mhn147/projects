using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TomoPlan.Web.Core;
using TomoPlan.Web.Data.Repositories;
using TomoPlan.Web.ViewModels;

namespace TomoPlan.Web.Pages;

[Authorize]
public class Plan(
    DailyPlansRepository dailyPlanRepo,
    DailyPlansService dailyPlanService) : PageModel
{
    public DailyPlansService DailyPlanService { get; } = dailyPlanService;

    public DailyPlanViewModel DailyPlanViewModel { get; set; } = new DailyPlanViewModel();
    
    [BindProperty]
    public DailyTaskViewModel TimeBlock { get; set; } = new();
    
    public async Task OnGet(string date)
    {
        var userId = Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty);
        var dateOnly = DateOnly.Parse(date, CultureInfo.InvariantCulture);

        var dailyPlan = await dailyPlanRepo.Get(userId, dateOnly);

        if (dailyPlan == null)
        {
            dailyPlan = await dailyPlanRepo.Create(dateOnly, userId);
        }

        DailyPlanViewModel = new DailyPlanViewModel
        {
            Date = dailyPlan.Date,
            DayOfWeek = dailyPlan.Date.DayOfWeek.ToString(),
            DayBadge = DayBadge.Tomorrow, // TODO
            IsReadOnly = false,
            Tasks = dailyPlan.Tasks.Select(t => new DailyTaskViewModel
            {
                Text = t.Text,
                Start = t.Start,
                End = t.End,
                Id = t.Id
            }).ToList()
        };
    }

    public async Task<IActionResult> OnPostAddTimeBlock(string date)
    {
        if (!ModelState.IsValid)
        {
            // TODO: validation
            return Page();
        }
        
        // get plan
        var userId = Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty);
        var dateOnly = DateOnly.Parse(date, CultureInfo.InvariantCulture);

        var dailyPlan = await dailyPlanRepo.Get(userId, dateOnly);
        
        // TODO: throw/return error if dailyPlan is null
        
        // check if available
        var conflict = DailyPlanService.TimeBlockConflict(dailyPlan, TimeBlock);
        if (conflict)
        {
            // todo
            DailyPlanViewModel = new DailyPlanViewModel
            {
                Date = dailyPlan.Date,
                DayOfWeek = dailyPlan.Date.DayOfWeek.ToString(),
                DayBadge = DayBadge.Tomorrow, // TODO
                IsReadOnly = false,
                Tasks = dailyPlan.Tasks.Select(t => new DailyTaskViewModel
                {
                    Text = t.Text,
                    Start = t.Start,
                    End = t.End,
                    Id = t.Id
                }).ToList()
            };
            return Page();
        }
        
        dailyPlan = await DailyPlanService.AddTimeBlock(dailyPlan, TimeBlock);
        
        DailyPlanViewModel = new DailyPlanViewModel
        {
            Date = dailyPlan.Date,
            DayOfWeek = dailyPlan.Date.DayOfWeek.ToString(),
            DayBadge = DayBadge.Tomorrow, // TODO
            IsReadOnly = false,
            Tasks = dailyPlan.Tasks.Select(t => new DailyTaskViewModel
            {
                Text = t.Text,
                Start = t.Start,
                End = t.End,
                Id = t.Id
            }).ToList()
        };
        
        return Page();
    }
}