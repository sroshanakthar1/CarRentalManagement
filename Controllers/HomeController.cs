using CarRentalManagement.Data;
using CarRentalManagement.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CarRentalManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public HomeController(ApplicationDbContext db) => _db = db;


        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role == "Admin") return RedirectToAction("AdminDashboard");
            if (role == "Customer") return RedirectToAction("CustomerDashboard");
            return View();
        }


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


        [AuthorizeRole("Customer")]
        public IActionResult CustomerDashboard()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            return View();
        }
    }
}