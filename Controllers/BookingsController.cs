using CarRentalManagement.Data;
using CarRentalManagement.Models;
using CarRentalManagement.ViewModels;
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

        // GET: Booking Create page
        [HttpGet]
        public IActionResult Create(int carId, DateTime? pickupDate, DateTime? returnDate)
        {
            var car = _db.Cars.FirstOrDefault(c => c.CarID == carId);
            if (car == null)
                return NotFound();

            var pickup = pickupDate ?? DateTime.Today;
            var ret = returnDate ?? DateTime.Today.AddDays(1);

            if ((ret - pickup).Days <= 0)
                return View(new BookingViewModel { Error = "Invalid booking dates." });

            var model = new BookingViewModel
            {
                CarID = car.CarID,
                CarName = car.CarName,
                CarModel = car.CarModel,
                CarPrice = car.PricePerDay,
                PickupDate = pickup,
                ReturnDate = ret,
                TotalCost = car.PricePerDay * (ret - pickup).Days,
                SelectedCarId = car.CarID,
                CarImageUrl = car.ImageUrl,
                Transmission = car.Transmission,
                FuelType = car.FuelType,
                SeatingCapacity = car.SeatingCapacity
            };

            return View(model);
        }

        // POST: Confirm Booking
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmBooking(BookingViewModel model)
        {
            // If user is not logged in, redirect to Login
            var userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            // Otherwise, save booking to DB (optional)
            // Example:
            /*
            var booking = new Booking
            {
                CarID = model.CarID,
                UserID = userId.Value,
                PickupDate = model.PickupDate,
                ReturnDate = model.ReturnDate,
                TotalCost = model.TotalCost
            };
            _db.Bookings.Add(booking);
            _db.SaveChanges();
            */

            // Redirect to a "Booking Success" page
            return RedirectToAction("Success");
        }
        public async Task<IActionResult> Create(int carId)
        {
            var car = await _db.Cars.FirstOrDefaultAsync(c => c.CarID == carId);
            if (car == null) return NotFound();

            var model = new BookingViewModel
            {
                CarID = car.CarID,
                CarName = car.CarName,
                CarModel = car.CarModel,
                CarImageUrl = car.ImageUrl,
                Transmission = car.Transmission,
                SeatingCapacity = car.SeatingCapacity,
                FuelType = car.FuelType,
                CarPrice = car.PricePerDay,
                PickupDate = DateTime.Today,
                ReturnDate = DateTime.Today.AddDays(1),
                TotalCost = car.PricePerDay // default 1 day
            };

            return View(model);
        }
        // Optional: Booking Success page
        public IActionResult Success()
        {
            return View();
        }
    }
}
