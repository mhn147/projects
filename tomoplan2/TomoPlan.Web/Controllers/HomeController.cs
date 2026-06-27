using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TomoPlan.Core.Data;
using TomoPlan.Web.Models;

namespace TomoPlan.Web.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly AppRepository _repo;

    public HomeController(AppRepository repo)
    {
        _repo = repo;
    }

    public async Task<IActionResult> Index()
    {
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
