using CarRentalManagement.Data;
using CarRentalManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalManagement.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _db;  // ✅ define _db

        public BookingsController(ApplicationDbContext db)  // ✅ inject db
        {
            _db = db;
        }

        [HttpGet]

        [HttpGet]
        public IActionResult Create(int carId, DateTime pickupDate, DateTime returnDate)
        {
            var days = (returnDate - pickupDate).Days;
            if (days <= 0)
            {
                return View(new ViewModels.BookingViewModel { Error = "Invalid booking dates." });
            }

            var car = _db.Cars.FirstOrDefault(c => c.CarID == carId);
            if (car == null)
            {
                return NotFound();
            }

            var model = new ViewModels.BookingViewModel
            {
                CarID = car.CarID,
                CarName = car.CarName,
                CarModel = car.CarModel,
                CarPrice = car.PricePerDay,
                PickupDate = pickupDate,
                ReturnDate = returnDate,
                TotalCost = car.PricePerDay * days,
                SelectedCarId = car.CarID,
                CarImageUrl = car.ImageUrl,
                Transmission = car.Transmission,
                FuelType = car.FuelType,
                SeatingCapacity = car.SeatingCapacity
            };

            return View(model);
        }
    }
}