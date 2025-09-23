using CarRentalManagement.Data;
using CarRentalManagement.Models;
using CarRentalManagement.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalManagement.Controllers
{
    public class CustomerProfileController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IAuthService _auth;
        public CustomerProfileController(ApplicationDbContext db)
        {
            _db = db;
        }

        // Show all cars to customers
        public async Task<IActionResult> Index()
        {
            var cars = await _db.Cars.AsNoTracking().ToListAsync();
            return View(cars); // Views/CustomerProfile/Index.cshtml
        }


        public async Task<IActionResult> ViewProfile(int id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserID == id);
            if (user == null)
                return NotFound();

            return View(user); // opens Details.cshtml
        }

        // Show car details
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0) return BadRequest();

            var car = await _db.Cars.AsNoTracking().FirstOrDefaultAsync(c => c.CarID == id);
            if (car == null) return NotFound();

            return View(car); // Views/CustomerProfile/Details.cshtml
        }

        public async Task<IActionResult> Settings()
        {
            int? userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null) return RedirectToAction("Login", "Account");

            var customer = await _db.Users.FirstOrDefaultAsync(u => u.UserID == userId && u.Role == "Customer");
            if (customer == null) return RedirectToAction("Login", "Account");

            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Settings(Models.User model)
        {
            int? userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null) return RedirectToAction("Login", "Account");

            var customer = await _db.Users.FirstOrDefaultAsync(u => u.UserID == userId && u.Role == "Customer");
            if (customer == null) return RedirectToAction("Login", "Account");

            // Update editable fields
            customer.FirstName = model.FirstName;
            customer.LastName = model.LastName;
            customer.Email = model.Email;
            customer.Phone = model.Phone;

            // If password field is filled, update password
            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                customer.Password = model.Password;
            }

            await _db.SaveChangesAsync();
            ViewBag.Message = "Profile updated successfully!";
            return View(customer);
        }

        [HttpGet]
        public IActionResult Login(string? error = null)
        {
            ViewBag.Error = error;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _auth.ValidateUserAsync(username, password);
            if (user == null)
            {
                ViewBag.Error = "❌ Invalid username or password";
                return View();
            }

            // Store session
            HttpContext.Session.SetInt32("UserID", user.UserID);
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Role);
            return RedirectToAction("Index", "Home");
        }

       

        // Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }

}

