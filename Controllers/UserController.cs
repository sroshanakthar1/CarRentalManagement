using CarRentalManagement.Data;
using CarRentalManagement.Models;
using CarRentalManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CarRentalManagement.Controllers
{
    public class UserController : Controller
    {
        private readonly IAuthService _auth;
        private readonly ApplicationDbContext _db;
        public UserController(IAuthService auth, ApplicationDbContext db)
        {
            _auth = auth; _db = db;
        }


        [HttpGet]
        public IActionResult Login(string? error = null)
        {
            ViewBag.Error = error;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _auth.ValidateUserAsync(username, password);
            if (user == null)
            {
                ViewBag.Error = "❌ Invalid username or password.";
                return View();
            }

            HttpContext.Session.SetInt32("UserID", user.UserID);
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Role);

            TempData["LoginSuccess"] = $"🎉 Welcome {user.Username}, login successful!";
            return RedirectToAction("Index", "Payment");
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _db.Users.ToListAsync();
            return View(users);
        }
        // ---------------- CREATE ----------------
        [HttpGet]
        public IActionResult Create()
        {
            return View(); // opens Create.cshtml
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            if (!ModelState.IsValid)
                return View(user);

            // Check username uniqueness
            var exists = await _db.Users.AnyAsync(u => u.Username == user.Username);
            if (exists)
            {
                ViewBag.Error = "❌ Username already exists.";
                return View(user);
            }

            // Default role
            user.Role = "Customer";

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            TempData["Success"] = $"🎉 Registration successful! Please login with username: {user.Username}";
            return RedirectToAction("Login");
        }
    }
}


    


        

