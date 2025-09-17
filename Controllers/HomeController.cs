using CarRentalManagement.Data;
using CarRentalManagement.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        // Landing page (guest)
        public async Task<IActionResult> Index()
        {
            var cars = await _db.Cars
           .AsNoTracking()
           .ToListAsync();

            return View(cars);
        }

        // Admin Dashboard (protected)
        [AuthorizeRole("Admin")]
        public async Task<IActionResult> AdminDashboard()
        {
            var totalCars = await _db.Cars.CountAsync();
            var availableCars = await _db.Cars.CountAsync(c => c.IsAvailable);
            var totalBookings = await _db.Bookings.CountAsync();

            ViewBag.TotalCars = totalCars;
            ViewBag.AvailableCars = availableCars;
            ViewBag.TotalBookings = totalBookings;

            return View();
        }

        // Customer Dashboard (protected)
        [AuthorizeRole("Customer")]
        public IActionResult CustomerDashboard()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> SearchAvailable(DateTime pickupDate, DateTime dropoffDate)
        {
            if (pickupDate >= dropoffDate)
            {
                ModelState.AddModelError("", "Drop-off date must be after pickup date.");
                return RedirectToAction("Index");
            }

            Console.WriteLine($"Search: {pickupDate:yyyy-MM-dd} ? {dropoffDate:yyyy-MM-dd}");

            // Find cars booked in this time range
            var bookedCarIds = await _db.Bookings
                .Where(b => pickupDate < b.ReturnDate && dropoffDate > b.PickupDate)
                .Select(b => b.CarID)
                .ToListAsync();

            Console.WriteLine("Booked Car IDs: " + string.Join(", ", bookedCarIds));

            // Find cars NOT booked in that time range
            var availableCars = await _db.Cars
                .Where(c => !bookedCarIds.Contains(c.CarID)) // Removed IsAvailable for debugging
                .ToListAsync();

            Console.WriteLine("Available Cars:");
            foreach (var car in availableCars)
            {
                Console.WriteLine($"- {car.CarName} ({car.CarID})");
            }

            return View("Available", availableCars);
        }


    }
}
