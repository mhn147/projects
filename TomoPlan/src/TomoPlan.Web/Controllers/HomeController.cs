using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using System.Security.Claims;
using TomoPlan.Web.Core;
using TomoPlan.Web.Data.Entities;
using TomoPlan.Web.ViewModels;

namespace TomoPlan.Web.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly DailyPlansService service;

    public HomeController(DailyPlansService service)
    {
        this.service = service;
    }

    public async Task<IActionResult> Index(DateTime? date)
    {
        var targetDate = service.GetDateToPlan(date);
        var userId = Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty);

        var dailyPlan = await service.GetOrCreatePlan(userId, targetDate);

        var model = new PlanViewModel
        {
            Id = dailyPlan.Id,
            Date = dailyPlan.Date,
            DayOfWeek = dailyPlan.Date.DayOfWeek.ToString(),
            DayBadge = DayBadge.Tomorrow, // TODO
            IsReadOnly = false,
            Tasks = dailyPlan.Tasks.Select(t => new TaskViewModel
            {
                Text = t.Text,
                Start = t.Start,
                End = t.End,
                Id = t.Id
            }).ToList()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddTask(PlanViewModel plan)
    {
        if (!ModelState.IsValid)
        {
            // TODO: validation
            return View("Index", new PlanViewModel());
        }

        var userId = Guid.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty);
        var dateOnly = DateOnly.FromDateTime(plan.NewTask.Date.DateTime);

        var dailyPlan = await service.GetPlan(userId, dateOnly);

        // TODO: throw/return error if dailyPlan is null
        if (dailyPlan == null)
            throw new Exception("foo");

        // check if available
        var conflict = service.TimeBlockConflict(dailyPlan, plan.NewTask);
        if (conflict)
        {
            // todo
            var model = new PlanViewModel
            {
                Id = dailyPlan.Id,
                Date = dailyPlan.Date,
                DayOfWeek = dailyPlan.Date.DayOfWeek.ToString(),
                DayBadge = DayBadge.Tomorrow, // TODO
                IsReadOnly = false,
                Tasks = dailyPlan.Tasks.Select(t => new TaskViewModel
                {
                    Text = t.Text,
                    Start = t.Start,
                    End = t.End,
                    Id = t.Id
                }).ToList()
            };
            return View("Index", model);
        }

        dailyPlan = await service.AddTimeBlock(dailyPlan, plan.NewTask);

        return View("Index", new PlanViewModel
        {
            Id = dailyPlan.Id,
            Date = dailyPlan.Date,
            DayOfWeek = dailyPlan.Date.DayOfWeek.ToString(),
            DayBadge = DayBadge.Tomorrow, // TODO
            IsReadOnly = false,
            Tasks = dailyPlan.Tasks.Select(t => new TaskViewModel
            {
                Text = t.Text,
                Start = t.Start,
                End = t.End,
                Id = t.Id
            }).ToList()
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteTask(Guid planId, Guid taskId)
    {
        var dailyPlan = await service.DeleteTask(planId, taskId);

        if (dailyPlan == null)
        {
            // todo
            return NotFound();
        }

        var model = MapToViewModel(dailyPlan);
        return View("Index", model);
    }

    private PlanViewModel MapToViewModel(DailyPlan dailyPlan)
    {
        return new PlanViewModel
        {
            Id = dailyPlan.Id,
            Date = dailyPlan.Date,
            DayOfWeek = dailyPlan.Date.DayOfWeek.ToString(),
            DayBadge = DayBadge.Tomorrow, // TODO
            IsReadOnly = false,
            Tasks = dailyPlan.Tasks.Select(t => new TaskViewModel
            {
                Text = t.Text,
                Start = t.Start,
                End = t.End,
                Id = t.Id
            }).ToList()
        };
    }
}
