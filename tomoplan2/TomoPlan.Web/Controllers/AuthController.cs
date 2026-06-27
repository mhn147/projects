using Microsoft.AspNetCore.Mvc;
using TomoPlan.Core.Core;

namespace TomoPlan.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("/auth/login")]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost("/auth/login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _authService.GetUser(email, password);
            await _authService.Login(HttpContext, user);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost("/auth/signup")]
        public async Task<IActionResult> SignUp(string email, string password)
        {
            var user = await _authService.SignUp(email, password);
            await _authService.Login(HttpContext, user);
            return RedirectToAction("Index", "Home");
        }
        

        [HttpPost("/auth/signout")]
        public async Task<IActionResult> Logout(string email, string password)
        {
            await _authService.Logout(HttpContext);
            return RedirectToAction("Index", "Home");
        }
    }
}
