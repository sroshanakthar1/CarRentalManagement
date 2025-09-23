using CarRentalManagement.Data;
using CarRentalManagement.Services;
using CarRentalManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalManagement.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IAuthService _auth;
        private readonly ApplicationDbContext _db;

        public PaymentController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: /Payment
        public IActionResult Index()
        {
           
            
            return View();
        }

        public async Task<IActionResult> Create(int carId, DateTime pickupDate, DateTime returnDate)
        {
            // Save the booking info in Session so navbar button can always use it
            HttpContext.Session.SetInt32("CarId", carId);
            HttpContext.Session.SetString("PickupDate", pickupDate.ToString("yyyy-MM-dd"));
            HttpContext.Session.SetString("ReturnDate", returnDate.ToString("yyyy-MM-dd"));




            var car = await _db.Cars.FirstOrDefaultAsync(c => c.CarID == carId);
            if (car == null) return NotFound();

            int totalDays = (returnDate - pickupDate).Days;
            decimal totalCost = totalDays * car.PricePerDay;

            var model = new PaymentViewModel
            {
                CarId = car.CarID,
                CarName = car.CarName,
                CarModel = car.CarModel,
                PickupDate = pickupDate,
                ReturnDate = returnDate,
                TotalCost = totalCost,
                CarPricePerDay = car.PricePerDay
            };

            return View(model);
        }


        [HttpPost]
        public IActionResult ProcessPayment(int CarId, decimal TotalCost, string CardNumber, string CardHolderName, string ExpiryDate, string CVV, decimal Amount)
        {
            // Here you can add real payment logic, e.g., call a payment API.
            // For now, we will assume payment is always successful.

            // Dummy booking ID
            int bookingId = new Random().Next(1000, 9999);

            // Get username from session
            string username = HttpContext.Session.GetString("Username") ?? "Guest";

            // Get car info from DB (optional)
            var car = _db.Cars.FirstOrDefault(c => c.CarID == CarId);
            string carName = car?.CarName ?? "Unknown";
            string carModel = car?.CarModel ?? "";

            // Redirect to Success page
            return RedirectToAction("Success", new
            {
                bookingId,
                username,
                carName,
                carModel,
                pickupDate = DateTime.Today.ToString("yyyy-MM-dd"),
                returnDate = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd"),
                totalCost = TotalCost
            });
        }
        public IActionResult Success(int bookingId, string username, string carName, string carModel, DateTime pickupDate, DateTime returnDate, decimal totalCost)
        {
            var model = new PaymentSuccessViewModel
            {
                BookingId = bookingId,
                Username = username,
                CarName = carName,
                CarModel = carModel,
                PickupDate = pickupDate,
                ReturnDate = returnDate,
                TotalCost = totalCost,
                CollectionAddress = "123 Main Street, City, Country",
                ContactNumber = "+94 77 123 4567"
            };

            return View(model);
        }
    }
}