using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using VisualVibe.Models;
using VisualVibe.Data;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace VisualVibe.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Login Page
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login Form Submit
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == "admin" && password == "admin123")
            {
                return RedirectToAction("Dashboard");
            }

            ViewBag.Error = "Invalid username or password.";
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        // GET: Signup Page
        public IActionResult Signup() => View();

        // POST: Signup Form Submit
        [HttpPost]
        public IActionResult Signup(string fullname, string email, string username, string password)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Name = fullname,
                    Email = email,
                    Position = "Administrator",
                    Address = "N/A",
                    Number = "N/A"
                };

                // Hash the password
                using (var sha256 = SHA256.Create())
                {
                    var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                    user.PasswordHash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                }

                _context.Users.Add(user);
                _context.SaveChanges();

                return RedirectToAction("Login");
            }

            return View();
        }

        public IActionResult Users()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(User user, string PasswordHash)
        {
            // Para ma-encrypt password
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(PasswordHash);

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Users");
        }


        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                using (var sha256 = SHA256.Create())
                {
                    var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(user.PasswordHash));
                    user.PasswordHash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                }

                _context.Users.Add(user);

                Console.WriteLine("Saving user: " + user.Name);

                _context.SaveChanges();

                TempData["Success"] = "Admin registered successfully!";
                return RedirectToAction("Users");
            }

            return View(user);
        }

        // GET
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();
            return View(user);
        }

        // POST
        [HttpPost]
        public IActionResult Edit(User updatedUser)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == updatedUser.Id);
            if (user == null) return NotFound();

            user.Name = updatedUser.Name;
            user.Address = updatedUser.Address;
            user.Email = updatedUser.Email;
            user.Number = updatedUser.Number;
            user.Position = updatedUser.Position;

            _context.SaveChanges();
            return RedirectToAction("Users");
        }

        public IActionResult Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
            return RedirectToAction("Users");
        }

        public IActionResult Home() => View();
        public IActionResult Edit() => View();
    }
}
