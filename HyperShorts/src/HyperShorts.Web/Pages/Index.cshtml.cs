using System.ComponentModel.DataAnnotations;
using HyperShorts.Web.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HyperShorts.Web.Pages;

public class IndexModel(HyperShortsService service) : PageModel
{
    private readonly HyperShortsService _service = service;

    [BindProperty]
    [Required]
    [Url]
    public string? LongUrl { get; set; }
    public string? ShortUrl { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        // TODO: validation
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var shortCode = await _service.ShortenLongUrl(LongUrl);

        ShortUrl = $"{Request.Scheme}://{Request.Host}/s/{shortCode}";

        return Page();
    }
}
