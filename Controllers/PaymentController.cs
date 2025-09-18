using CarRentalManagement.Data;
using CarRentalManagement.Models;
using CarRentalManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CarRentalManagement.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PaymentController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {

            return View(); // Views/CustomerProfile/Index.cshtml
        }


        public IActionResult Create() => View(new Car());

        [HttpPost]
        public IActionResult StartPayment(int carId, DateTime pickupDate, DateTime returnDate, decimal totalCost)
        {
            // Check if user is logged in
            var userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
            {
                // Save booking info temporarily in TempData
                TempData["CarID"] = carId;
                TempData["PickupDate"] = pickupDate.ToString("yyyy-MM-dd");
                TempData["ReturnDate"] = returnDate.ToString("yyyy-MM-dd");
                TempData["TotalCost"] = totalCost;
                return RedirectToAction("Login", "Account");
            }

            // If logged in, show payment page
            var car = _db.Cars.FirstOrDefault(c => c.CarID == carId);
            var username = HttpContext.Session.GetString("Username");

            var model = new PaymentViewModel
            {
                CarName = car?.CarName,
                CarModel = car?.CarModel,
                TotalCost = totalCost,
                Username = username,
                PickupDate = pickupDate,
                ReturnDate = returnDate
            };

            return View("Create", model);
        }


        [HttpPost]
        public IActionResult ConfirmPayment()
        {
            // You can save booking here if needed
            ViewBag.Message = "Payment Successful! Your booking is confirmed.";
            return View("Success");
        }


      
    }




}