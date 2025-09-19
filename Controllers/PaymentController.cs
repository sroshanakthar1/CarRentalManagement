using CarRentalManagement.Data;
using CarRentalManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CarRentalManagement.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // List all Payments
        public IActionResult Index()
        {
            var payments = _context.Payments.ToList();
            return View(payments);
        }

        // Add (GET)
        public IActionResult Create()
        {
            return View();
        }

        // Add (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Payment payment)
        {
            if (ModelState.IsValid)
            {
                _context.Payments.Add(payment);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(payment);
        }

        // Edit (GET)
        public IActionResult Edit(int id)
        {
            var payment = _context.Payments.Find(id);
            if (payment == null) return NotFound();
            return View(payment);
        }

        // Edit (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Payment payment)
        {
            if (ModelState.IsValid)
            {
                _context.Payments.Update(payment);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(payment);
        }

        // Delete (GET)
        public IActionResult Delete(int id)
        {
            var payment = _context.Payments.Find(id);
            if (payment == null) return NotFound();
            return View(payment);
        }

        // Delete (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var payment = _context.Payments.Find(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
