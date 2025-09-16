using CarRentalManagement.Data;
using CarRentalManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalManagement.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BookingsController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Bookings/Create
        public async Task<IActionResult> Create()
        {
            // ✅ Check login session
            var username = HttpContext.Session.GetString("Username");
            if (username == null)
            {
                return RedirectToAction("Login", "Users");
            }

            // ✅ Load cars for dropdown
            ViewBag.Cars = await _db.Cars.ToListAsync();

            return View();
        }

        // POST: Bookings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Booking booking)
        {
            var username = HttpContext.Session.GetString("Username");
            if (username == null)
            {
                return RedirectToAction("Login", "Users");
            }

            // ✅ Find the logged-in user
            var customer = await _db.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (customer == null)
            {
                return RedirectToAction("Login", "Users");
            }

            if (ModelState.IsValid)
            {
                booking.CustomerID = customer.UserID;  // link to logged-in user
                booking.Username = customer.Username;  // store username for easy viewing

                // ❗ You can calculate cost here if needed
                // booking.TotalCost = (booking.ReturnDate - booking.PickupDate).Days * dailyRate;

                _db.Bookings.Add(booking);
                await _db.SaveChangesAsync();

                TempData["SuccessMessage"] = "Booking created successfully!";
                return RedirectToAction("Index", "Bookings");
            }

            ViewBag.Cars = await _db.Cars.ToListAsync();
            return View(booking);
        }

        // Show all bookings for the logged-in user
        public async Task<IActionResult> Index()
        {
            var username = HttpContext.Session.GetString("Username");
            if (username == null)
            {
                return RedirectToAction("Login", "Users");
            }

            var bookings = await _db.Bookings
                .Include(b => b.Car)
                .Include(b => b.Customer)
                .Where(b => b.Username == username)
                .ToListAsync();

            return View(bookings);
        }
    }
}
