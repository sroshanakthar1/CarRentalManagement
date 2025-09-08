using CarRentalManagement.Data;
using CarRentalManagement.Filters;
using CarRentalManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalManagement.Controllers
{
    [AuthorizeRole("Admin")]
    public class CarsController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CarsController(ApplicationDbContext db) => _db = db;

        // GET: Cars
        public async Task<IActionResult> Index(string filter)
        {
            var cars = await _db.Cars.AsNoTracking().ToListAsync();

            // Stats logic
            ViewBag.TotalCars = cars.Count;
            ViewBag.AvailableCars = cars.Count(c => c.IsAvailable);
            ViewBag.BookedCars = cars.Count(c => !c.IsAvailable); // or use BookedCar if it's a property



            // Apply filter
            if (filter == "available")
            {
                cars = cars.Where(c => c.IsAvailable).ToList();
            }
            else if (filter == "booked")
            {
                cars = cars.Where(c => !c.IsAvailable).ToList();
            }

            return View(cars);
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var car = await _db.Cars.AsNoTracking().FirstOrDefaultAsync(c => c.CarID == id);
            if (car == null) return NotFound();
            return View(car);
        }

        // GET: Cars/Create
        [HttpGet]
        public IActionResult Create() => View(new Car());

        // POST: Cars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Car car)
        {
            if (!ModelState.IsValid) return View(car);

            try
            {
                _db.Cars.Add(car);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error saving car: {ex.Message}");
                return View(car);
            }
        }

        // GET: Cars/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var car = await _db.Cars.FindAsync(id);
            if (car == null) return NotFound();
            return View(car);
        }

        // POST: Cars/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Car car)
        {
            if (!ModelState.IsValid) return View(car);

            try
            {
                _db.Cars.Update(car);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_db.Cars.Any(e => e.CarID == car.CarID))
                    return NotFound();

                throw;
            }
        }

        // GET: Cars/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var car = await _db.Cars.AsNoTracking().FirstOrDefaultAsync(c => c.CarID == id);
            if (car == null) return NotFound();
            return View(car);
        }

        // POST: Cars/DeleteConfirmed/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _db.Cars.FindAsync(id);
            if (car == null) return NotFound();

            try
            {
                _db.Cars.Remove(car);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error deleting car: {ex.Message}");
                return View(car);
            }
        }
    }
}
