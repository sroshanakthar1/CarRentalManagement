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


        public async Task<IActionResult> Index()
        {
            var cars = await _db.Cars.AsNoTracking().ToListAsync();
            return View(cars);
        }


        [HttpGet]
        public IActionResult Create() => View(new Car());


        [HttpPost]
        public async Task<IActionResult> Create(Car car)
        {
            if (!ModelState.IsValid) return View(car);
            _db.Cars.Add(car);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var car = await _db.Cars.FindAsync(id);
            if (car == null) return NotFound();
            return View(car);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Car car)
        {
            if (!ModelState.IsValid) return View(car);
            _db.Cars.Update(car);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var car = await _db.Cars.FindAsync(id);
            if (car == null) return NotFound();
            return View(car);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _db.Cars.FindAsync(id);
            if (car == null) return NotFound();
            _db.Cars.Remove(car);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}