using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using System.Security.Claims;
using TomoPlan.Web.Core;
using TomoPlan.Web.Data.Entities;
using TomoPlan.Web.ViewModels;

namespace TomoPlan.Web.Pages
{
    [Authorize]
    public class IndexModel(DailyPlansService dailyPlansService) : PageModel
    {
        private readonly DailyPlansService service = dailyPlansService;
        public DayPlanViewModel DailyPlan { get; set; } = new DayPlanViewModel();

        [BindProperty]
        public DayTaskViewModel DailyTask { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(DateTime? date)
        {
            var targetDate = service.GetDateToPlan(date);
            var userId = Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty);

            var dailyPlan = await service.GetOrCreatePlan(userId, targetDate);
            
            DailyPlan = new DayPlanViewModel
            {
                Id = dailyPlan.Id,
                Date = dailyPlan.Date,
                DayOfWeek = dailyPlan.Date.DayOfWeek.ToString(),
                DayBadge = DayBadge.Tomorrow, // TODO
                IsReadOnly = false,
                Tasks = dailyPlan.Tasks.Select(t => new DayTaskViewModel
                {
                    Text = t.Text,
                    Start = t.Start,
                    End = t.End,
                    Id = t.Id
                }).ToList()
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAddTimeBlock(DateTime? date)
        {
            if (!ModelState.IsValid)
            {
                // TODO: validation
                return Page();
            }

            var userId = Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty);
            var x = date.HasValue ? date.Value : DateTime.MinValue;
            var dateOnly = DateOnly.FromDateTime(x);

            var dailyPlan = await service.GetPlan(userId, dateOnly);

            // TODO: throw/return error if dailyPlan is null
            if (dailyPlan == null)
                throw new Exception("foo");

            // check if available
            var conflict = service.TimeBlockConflict(dailyPlan, DailyTask);
            if (conflict)
            {
                // todo
                DailyPlan = new DayPlanViewModel
                {
                    Id = dailyPlan.Id,
                    Date = dailyPlan.Date,
                    DayOfWeek = dailyPlan.Date.DayOfWeek.ToString(),
                    DayBadge = DayBadge.Tomorrow, // TODO
                    IsReadOnly = false,
                    Tasks = dailyPlan.Tasks.Select(t => new DayTaskViewModel
                    {
                        Text = t.Text,
                        Start = t.Start,
                        End = t.End,
                        Id = t.Id
                    }).ToList()
                };
                return Page();
            }

            dailyPlan = await service.AddTimeBlock(dailyPlan, DailyTask);

            DailyPlan = new DayPlanViewModel
            {
                Id = dailyPlan.Id,
                Date = dailyPlan.Date,
                DayOfWeek = dailyPlan.Date.DayOfWeek.ToString(),
                DayBadge = DayBadge.Tomorrow, // TODO
                IsReadOnly = false,
                Tasks = dailyPlan.Tasks.Select(t => new DayTaskViewModel
                {
                    Text = t.Text,
                    Start = t.Start,
                    End = t.End,
                    Id = t.Id
                }).ToList()
            };

            return Page();
        }
    
        public async Task<IActionResult> OnPostDeleteTimeBlock(Guid planId, Guid taskId)
        {
            var dailyPlan = await service.DeleteTask(planId, taskId);

            if (dailyPlan != null)
            {
                MapToViewModel(dailyPlan);
            }

            return Page();
        }

        private void MapToViewModel(DailyPlan dailyPlan)
        {

            DailyPlan = new DayPlanViewModel
            {
                Id = dailyPlan.Id,
                Date = dailyPlan.Date,
                DayOfWeek = dailyPlan.Date.DayOfWeek.ToString(),
                DayBadge = DayBadge.Tomorrow, // TODO
                IsReadOnly = false,
                Tasks = dailyPlan.Tasks.Select(t => new DayTaskViewModel
                {
                    Text = t.Text,
                    Start = t.Start,
                    End = t.End,
                    Id = t.Id
                }).ToList()
            };
        }
    }
}