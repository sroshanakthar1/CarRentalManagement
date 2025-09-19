using CarRentalManagement.Data;
using CarRentalManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalManagement.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            var user = new User { Role = "Customer" };
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            if (_context.Users.Any(u => u.Username == user.Username))
            {
                ViewBag.Error = "Username already exists.";
                return View(user);
            }

            user.Role = "Customer"; // default role
            _context.Add(user);
            await _context.SaveChangesAsync();

            // Use TempData to show success after redirect
            TempData["Success"] = $"🎉 Account created successfully! You can now login with username: {user.Username}";

            // Redirect to Login page
            return RedirectToAction("Login", "User");
        }

        // Optional: Details view if needed later
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserID == id);
            if (user == null)
                return NotFound();

            return View(user);
        }
        

         public IActionResult Login()
        {
            return View();
        }

        // POST: Users/Login
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password && u.Role == "Customer");

            if (user == null)
            {
                ViewBag.Error = "❌ Invalid username or password.";
                return View();
            }

            // Save session
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Role);

            TempData["Success"] = $"✅ Welcome {user.Username}, login successful!";

            return RedirectToAction("Index", "Home"); // Redirect to customer home/dashboard
        }
    }
}

