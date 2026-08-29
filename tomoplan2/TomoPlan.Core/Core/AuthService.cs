
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TomoPlan.Core.Data;
using TomoPlan.Core.Data.Entities;

namespace TomoPlan.Core.Core
{
    public class AuthService
    {
        private readonly AppRepository _repo;

        public AuthService(AppRepository repo)
        {
            _repo = repo;
        }

        public async Task<User> SignUp(string email, string password)
        {
            var x = await _repo.UserExists(email);

            if (x)
            {
                throw new Exception("foo");
            }

            // TODO: security
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            var id = Guid.NewGuid();

            await _repo.AddUser(id, email, passwordHash);

            return new User
            {
                Id = id,
                Email = email
            };
        }

        public async Task<User> GetUser(string email, string password)
        {
            var x = await _repo.GetUser(email);

            if (x == null)
            {
                throw new Exception("foo");
            }

            // TODO: security
            var isValid = BCrypt.Net.BCrypt.Verify(password, x.PasswordHash);

            if (!isValid)
            {
                throw new Exception("nah");
            }

            return x;
        }

        public async Task Logout(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task Login(HttpContext httpContext, User user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Name, user.Email)
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new Microsoft.AspNetCore.Authentication.AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30)
                });
        }

        public bool IsLogedIn(ClaimsPrincipal principal)
        {
            return principal.Identities.Any(i =>
                i.IsAuthenticated &&
                i.AuthenticationType == CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
