using CarRentalManagement.Data;
using CarRentalManagement.Filters;
using CarRentalManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CarRentalManagement.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private const decimal RatePerDay = 50m; // per assignment
        public BookingsController(ApplicationDbContext db) => _db = db;


        [AuthorizeRole("Customer")]
        [HttpGet]
        public async Task<IActionResult> Create(int? carId)
        {
            var cars = await _db.Cars.Where(c => c.IsAvailable).AsNoTracking().ToListAsync();
            ViewBag.Cars = cars;
            ViewBag.SelectedCarId = carId;
            return View();
        }

        [AuthorizeRole("Customer")]
        [HttpPost]
        public async Task<IActionResult> Create(int carId, DateTime pickupDate, DateTime returnDate)
        {
            // Validations per assignment
            if (pickupDate.Date < DateTime.Today)
            {
                ViewBag.Error = "Pickup date must be today or later.";
                return await Create(carId);
            }
            if (returnDate.Date <= pickupDate.Date)
            {
                ViewBag.Error = "Return date must be after pickup date.";
                return await Create(carId);
            }


            var car = await _db.Cars.FindAsync(carId);
            if (car == null || !car.IsAvailable)
            {
                ViewBag.Error = "Selected car is not available.";
                return await Create(carId);
            }


            var days = (returnDate.Date - pickupDate.Date).Days;
            var total = days * RatePerDay;


            var booking = new Booking
            {
                CarID = carId,
                CustomerID = HttpContext.Session.GetInt32("UserID")!.Value,
                PickupDate = pickupDate.Date,
                ReturnDate = returnDate.Date,
                TotalCost = total
            };


            _db.Bookings.Add(booking);
            car.IsAvailable = false; // mark unavailable on booking creation
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(MyBookings));
        }

        [AuthorizeRole("Customer")]
        public async Task<IActionResult> MyBookings()
        {
            var uid = HttpContext.Session.GetInt32("UserID")!.Value;
            var bookings = await _db.Bookings
            .Include(b => b.Car)
            .Where(b => b.CustomerID == uid)
            .OrderByDescending(b => b.PickupDate)
            .ToListAsync();
            return View(bookings);
        }


        [AuthorizeRole("Admin")]
        public async Task<IActionResult> All()
        {
            var bookings = await _db.Bookings
            .Include(b => b.Car)
            .Include(b => b.Customer)
            .OrderByDescending(b => b.PickupDate)
            .ToListAsync();
            return View(bookings);
        }
    }
}