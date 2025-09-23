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


        // GET: /User/Login
        [HttpGet]
        public IActionResult Login(int? carId, DateTime? pickupDate, DateTime? returnDate, string? error = null)
        {
            // Already logged in → redirect to Payment/Create
            var username = HttpContext.Session.GetString("Username");
            if (!string.IsNullOrEmpty(username) && carId.HasValue && pickupDate.HasValue && returnDate.HasValue)
            {
                return RedirectToAction("Create", "Payment", new
                {
                    carId,
                    pickupDate = pickupDate?.ToString("yyyy-MM-dd"),
                    returnDate = returnDate?.ToString("yyyy-MM-dd")
                });
            }

            ViewBag.Error = error;
            ViewBag.CarId = carId;
            ViewBag.PickupDate = pickupDate;
            ViewBag.ReturnDate = returnDate;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, int? carId, DateTime? pickupDate,  DateTime? returnDate)
        {
            var user = await _auth.ValidateUserAsync(username, password);
            if (user == null)
            {
                ViewBag.Error = "❌ Invalid username or password";
                ViewBag.CarId = carId;
                ViewBag.PickupDate = pickupDate;
                ViewBag.ReturnDate = returnDate;
              
                return View();
            }

            HttpContext.Session.SetInt32("UserID", user.UserID);
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Role);

            return RedirectToAction("Create", "Payment", new
            {
                carId = carId,
                pickupDate = pickupDate?.ToString("yyyy-MM-dd"),
                returnDate = returnDate?.ToString("yyyy-MM-dd")
            });
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

        public async Task<IActionResult> Details(int id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserID == id);
            if (user == null)
                return NotFound();

            return View(user); // opens Details.cshtml
        }

        // ---------------- DELETE ----------------
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserID == id);
            if (user == null)
                return NotFound();

            return View(user); // opens Delete.cshtml
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            try
            {
                _db.Users.Remove(user);
                await _db.SaveChangesAsync();
                TempData["Success"] = "🗑️ User deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error deleting user: {ex.Message}");
                return View(user);
            }
        }
    }
}








