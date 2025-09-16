using CarRentalManagement.Data;
using CarRentalManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalManagement.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UsersController(ApplicationDbContext db)
        {
            _db = db;
        }

        // Show all users
        public async Task<IActionResult> Index()
        {
            var users = await _db.Users.ToListAsync();
            return View(users);
        }

        // GET: Create form
        public IActionResult Create()
        {
            return View();
        }

        // POST: Save user to DB
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Users user)
        {
            if (ModelState.IsValid)
            {
                _db.Add(user);
                await _db.SaveChangesAsync();

                // ✅ Show success message (TempData keeps it for 1 redirect)
                TempData["SuccessMessage"] = "You have registered successfully. Please log in.";

                // ✅ Redirect to Login page instead of Index
                return RedirectToAction("Login", "Users");
            }

            return View(user);
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _db.Users
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                // ✅ Successful login
                HttpContext.Session.SetString("Username", user.Username);

                // Redirect to Bookings/Create page
                return RedirectToAction("Create", "Bookings");
            }

            // ❌ Invalid login
            ViewBag.Error = "Invalid username or password";
            return View();
        }
    }
}
