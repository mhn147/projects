using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using System.Security.Claims;
using TomoPlan.Web.Core;
using TomoPlan.Web.ViewModels;

namespace TomoPlan.Web.Pages
{
    [Authorize]
    public class IndexModel(DailyPlansService dailyPlansService) : PageModel
    {
        private readonly DailyPlansService service = dailyPlansService;
        public DailyPlanViewModel DailyPlanViewModel { get; set; } = new DailyPlanViewModel();

        [BindProperty]
        public DailyTaskViewModel TimeBlock { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(DateTime? date)
        {
            var targetDate = service.GetDateToPlan(date);
            var userId = Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty);

            var dailyPlan = await service.GetOrCreatePlan(userId, targetDate);
            
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

        public async Task<IActionResult> OnPostAddTimeBlock(string date)
        {
            if (!ModelState.IsValid)
            {
                // TODO: validation
                return Page();
            }

            var userId = Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty);
            var dateOnly = DateOnly.Parse(date, CultureInfo.InvariantCulture);

            var dailyPlan = await service.GetPlan(userId, dateOnly);

            // TODO: throw/return error if dailyPlan is null
            if (dailyPlan == null)
                throw new Exception("foo");

            // check if available
            var conflict = service.TimeBlockConflict(dailyPlan, TimeBlock);
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

            dailyPlan = await service.AddTimeBlock(dailyPlan, TimeBlock);

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
}