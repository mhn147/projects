using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using TomoPlan.Core.Core;
using TomoPlan.Core.Data;
using TomoPlan.Web.Models;
using TomoPlan.Web.ViewModels;

namespace TomoPlan.Web.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly AppService _appService;

    public HomeController(AppService appService)
    {
        _appService = appService;
    }

    public async Task<IActionResult> Index(DateTimeOffset? date)
    {
        date ??= DateTimeOffset.UtcNow;

        var userId = Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var plan = await _appService.GetOrCreatePlan(userId, date.Value);
        
        return View(plan);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddTask(AddTaskViewModel model)
    {
        // the date portion is in UTC
        var plan = await _appService.AddTask(model.PlanId, model.StartTime, model.EndTime, model.Body);
        
        return View("Index", plan);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
