using CarRentalManagement.Data;
using CarRentalManagement.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

public class BookingsController : Controller
{
    private readonly ApplicationDbContext _db;

    public BookingsController(ApplicationDbContext db)
    {
        _db = db;
    }

    // GET: Bookings/Create
    public async Task<IActionResult> Create(int carId)
    {
        // Find the car that was clicked
        var car = await _db.Cars.FirstOrDefaultAsync(c => c.CarID == carId);
        if (car == null)
            return NotFound();

        // Create a booking model with CarID pre-filled
        var booking = new Booking
        {
            CarID = car.CarID,
            Car = car
        };

        return View(booking);
    }

    // POST: Bookings/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Booking booking)
    {
        if (ModelState.IsValid)
        {
            // Attach username from session
            booking.Username = HttpContext.Session.GetString("Username");

            // Calculate cost (simple: days × price)
            var car = await _db.Cars.FindAsync(booking.CarID);
            if (car != null)
            {
                var days = (booking.ReturnDate - booking.PickupDate).Days;
                booking.TotalCost = days * car.PricePerDay;

                // Mark car as unavailable
                car.IsAvailable = false;
            }

            _db.Bookings.Add(booking);
            await _db.SaveChangesAsync();

            TempData["SuccessMessage"] = "Booking created successfully!";
            return RedirectToAction(nameof(Index));
        }

        return View(booking);
    }

    // GET: Bookings/Index
    public async Task<IActionResult> Index()
    {
        var bookings = await _db.Bookings
            .Include(b => b.Car)
            .Include(b => b.Customer)
            .ToListAsync();

        return View(bookings);
    }


}


