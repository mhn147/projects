using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using TomoPlan.Core.Core;
using TomoPlan.Core.Data;
using TomoPlan.Web.Models;

namespace TomoPlan.Web.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly AppService _appService;

    public HomeController(AppService appService)
    {
        _appService = appService;
    }

    public async Task<IActionResult> Index(DateOnly? planDate)
    {
        // if date provided use its plan
        var target = planDate ?? DateOnly.FromDateTime(DateTime.UtcNow);

        // else create today's, yesterday, and tomorrow plans using UTC to avoid timezone differences and send them all back
        var userId = Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var x = await _appService.GetPlan(userId, target);

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
