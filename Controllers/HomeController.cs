using CarRentalManagement.Data;
using CarRentalManagement.Filters;
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
    }
}
