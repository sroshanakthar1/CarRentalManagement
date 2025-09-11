using CarRentalManagement.Models;
using CarRentalManagement.Repostories;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalManagement.Controllers
{
    public class CustomerController : Controller
    {
        private readonly CustomerRepository _repository;

        public CustomerController(CustomerRepository repository)
        {
            _repository = repository;
        }

        // List all customers
        public async Task<IActionResult> Index()
        {
            var customers = await _repository.GetAllAsync();
            return View(customers);
        }

        // Create GET
        public IActionResult Create()
        {
            return View();
        }

        // Create POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // Edit GET
        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _repository.GetByIdAsync(id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        // Edit POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _repository.UpdateAsync(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // Delete GET
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _repository.GetByIdAsync(id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        // Delete POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
