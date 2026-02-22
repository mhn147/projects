using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TomoPlan.Web.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            var now = DateTime.Now;
            var planDate = now.Hour >= 22
                ? DateOnly.FromDateTime(now.AddDays(1))
                : DateOnly.FromDateTime(now);
            return RedirectToPage("/Plan", new { date = planDate.ToString("yyyy-MM-dd") });
        }
    }
}