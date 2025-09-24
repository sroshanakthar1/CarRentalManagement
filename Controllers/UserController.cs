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
            _auth = auth;
            _db = db;
        }

        // ---------------- LOGIN ----------------
        [HttpGet]
        public IActionResult Login(int? carId, DateTime? pickupDate, DateTime? returnDate, string? error = null)
        {
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password, int? carId, DateTime? pickupDate, DateTime? returnDate)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
            if (user == null)
            {
                ViewBag.Error = "❌ Invalid username or password.";
                ViewBag.CarId = carId;
                ViewBag.PickupDate = pickupDate?.ToString("yyyy-MM-dd");
                ViewBag.ReturnDate = returnDate?.ToString("yyyy-MM-dd");
                return View();
            }

            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Role);

            // Redirect to Payment if coming from booking, else home/dashboard
            if (carId.HasValue && pickupDate.HasValue && returnDate.HasValue)
            {
                return RedirectToAction("Create", "Payment", new
                {
                    carId,
                    pickupDate = pickupDate?.ToString("yyyy-MM-dd"),
                    returnDate = returnDate?.ToString("yyyy-MM-dd")
                });
            }

            return RedirectToAction("Index", "Home");
        }

        // ---------------- REGISTER ----------------
        // GET: show registration form
        [HttpGet]
        public IActionResult Create(int? carId, string pickupDate, string returnDate)
        {
            ViewBag.CarId = carId;
            ViewBag.PickupDate = pickupDate;
            ViewBag.ReturnDate = returnDate;
            return View();
        }

        // POST: submit registration
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user, int? carId, DateTime? pickupDate, DateTime? returnDate)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CarId = carId;
                ViewBag.PickupDate = pickupDate?.ToString("yyyy-MM-dd");
                ViewBag.ReturnDate = returnDate?.ToString("yyyy-MM-dd");
                return View(user);
            }

            var exists = await _db.Users.AnyAsync(u => u.Username == user.Username);
            if (exists)
            {
                ViewBag.Error = "❌ Username already exists.";
                ViewBag.CarId = carId;
                ViewBag.PickupDate = pickupDate?.ToString("yyyy-MM-dd");
                ViewBag.ReturnDate = returnDate?.ToString("yyyy-MM-dd");
                return View(user);
            }

            user.Role = "Customer";
            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            TempData["Success"] = $"🎉 Registration successful! Please login with username: {user.Username}";

            // Redirect to Login page WITH booking parameters
            return RedirectToAction("Login", "User", new
            {
                carId,
                pickupDate = pickupDate?.ToString("yyyy-MM-dd"),
                returnDate = returnDate?.ToString("yyyy-MM-dd")
            });
        }

        // ---------------- LIST USERS ----------------
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _db.Users.ToListAsync();
            return View(users);
        }

        // ---------------- DETAILS ----------------
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserID == id);
            if (user == null) return NotFound();
            return View(user);
        }

        // ---------------- DELETE ----------------
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserID == id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user == null) return NotFound();

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







