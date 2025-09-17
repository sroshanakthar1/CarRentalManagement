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
        public IActionResult Create(int carId, DateTime pickupDate, DateTime returnDate)
        {
            var days = (returnDate - pickupDate).Days;
            if (days <= 0)
            {
                return View(new BookingViewModel { Error = "Invalid booking dates." });
            }

            var car = _db.Cars.Find(carId);
            if (car == null)
            {
                return NotFound();
            }

            var model = new BookingViewModel
            {
                CarID = carId,
                PickupDate = pickupDate,
                ReturnDate = returnDate,
                TotalCost = car.PricePerDay * days,
                SelectedCarId = carId
            };

            return View(model);
        }


    }
}
