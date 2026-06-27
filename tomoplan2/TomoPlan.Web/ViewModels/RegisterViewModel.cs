using System.ComponentModel.DataAnnotations;

namespace TomoPlan.Web.ViewModels;

public class RegisterViewModel
{
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
