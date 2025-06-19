using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace VisualVibe.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == "admin" && password == "admin123")
                return RedirectToAction("Home", "Admin");

            ViewBag.Error = "Invalid credentials";
            return View();
        }

        public IActionResult Signup() => View();

        [HttpPost]
        public IActionResult Signup(string fullname, string email, string username, string password)
        {
            // Normally save to DB
            return RedirectToAction("Login");
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // Sign out the user from the cookie authentication scheme
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Redirect to Login page after logout
            return RedirectToAction("Login", "Admin");
        }
    }
}
