using Configure3S.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Configure3S.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private UserRepository userRepository = new UserRepository();

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(IFormCollection collection)
        {
            var result = userRepository.Login(collection);
            string notification = string.Empty;

            if (result != null)
            {
                var claims = new List<Claim> { };
                claims.Add(new Claim("UserId", result.UserId));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, result.UserId));
                claims.Add(new Claim(ClaimTypes.Role, result.CompanyId));
                claims.Add(new Claim("CompanyId", result.CompanyId));
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPricipal = new ClaimsPrincipal(claimsIdentity);
                HttpContext.SignInAsync(claimsPricipal);
                return RedirectToAction("Index", "APIConfigure");
            }
            else
            {
                notification = "Login failed. You may have entered the wrong username or password.";
            }
            TempData["lLoti"] = notification;
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return Redirect("/User/Login");
        }
    }
}
