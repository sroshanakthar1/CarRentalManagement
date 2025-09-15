using CarRentalManagement.Data;
using CarRentalManagement.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

public class BookingsController : Controller
{
    private readonly ApplicationDbContext _db;


    // GET: Bookings
    public BookingsController(ApplicationDbContext db)
    {
        _db = db;
        

    }

    // GET: Bookings/Create
    public IActionResult Create(int? carId)
    {
        ViewBag.Cars = _db.Cars.ToList();
        ViewBag.SelectedCarId = carId;
        return View();
    }

    // POST: Bookings/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(int carId, DateTime pickupDate, DateTime returnDate)
    {
        if (pickupDate >= returnDate)
        {
            ViewBag.Error = "Return date must be after pickup date.";
            ViewBag.Cars = _db.Cars.ToList();
            ViewBag.SelectedCarId = carId;
            return View();
        }

        var booking = new Booking
        {
            CarID = carId,
            PickupDate = pickupDate,
            ReturnDate = returnDate
        };
        _db.Bookings.Add(booking);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
