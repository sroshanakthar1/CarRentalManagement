using CarRentalManagement.Data;
using CarRentalManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalManagement.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PaymentController(ApplicationDbContext db)
        {
            _db = db;
        }

        // Show Payment Page
        public async Task<IActionResult> Index()
        {
            return View();

        }
    }

}