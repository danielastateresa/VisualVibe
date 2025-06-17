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

    }
}
