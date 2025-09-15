using CarRentalManagement.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalManagement.Controllers
{
    public class CustomerProfileController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CustomerProfileController(ApplicationDbContext db)
        {
            _db = db;
        }

        // Show all cars to customers
        public async Task<IActionResult> Index()
        {
            var cars = await _db.Cars.AsNoTracking().ToListAsync();
            return View(cars); // Views/CustomerProfile/Index.cshtml
        }

        // Show car details
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0) return BadRequest();

            var car = await _db.Cars.AsNoTracking().FirstOrDefaultAsync(c => c.CarID == id);
            if (car == null) return NotFound();

            return View(car); // Views/CustomerProfile/Details.cshtml
        }

    }
}
