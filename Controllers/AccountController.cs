using CarRentalManagement.Data;
using CarRentalManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AccountController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Login Page
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login Form
        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            // Check database for a user with this username AND password
            var user = await _db.Users
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                // ✅ Login successful
                HttpContext.Session.SetInt32("UserID", user.UserID);
                HttpContext.Session.SetString("Username", user.Username);

                // Redirect to Bookings/Create page
                return RedirectToAction("Index", "Bookings");
            }

            // ❌ Login failed
            ViewBag.Error = "Invalid Username or Password";
            return View();
        }
    }
}
