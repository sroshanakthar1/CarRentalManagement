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
        public async Task<IActionResult> Index()
        {
            var cars = await _db.Cars.ToListAsync();
            return View(cars);  // ✅ send a list to the view
        }


        public async Task<IActionResult> Details(int id)
        {
            var car = await _db.Cars
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CarID == id);

            if (car == null) return NotFound();

            return View(car);
        }
    }
}
