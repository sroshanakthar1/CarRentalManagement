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

        

        public IActionResult Index(int carId, DateTime pickupDate, DateTime returnDate, decimal totalcoast)
        {
            // You can pass these values to a view model
            var model = new PaymentViewModel
            {
                CarId = carId,
                PickupDate = pickupDate,
                ReturnDate = returnDate,
                TotalCost=totalcoast,
            };

            return View(model);

        }
    }
}