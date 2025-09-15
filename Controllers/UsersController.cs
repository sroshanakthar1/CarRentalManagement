using CarRentalManagement.Data;
using CarRentalManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalManagement.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UsersController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Users/Index
        public async Task<IActionResult> Index()
        {
            var users = await _db.Users.ToListAsync();
            return View(users);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost]

        
        public async Task<IActionResult> Create(Users user)
        {
            if (ModelState.IsValid)
            {
                // Handle NationalID / Passport
                if (string.IsNullOrEmpty(user.NationalID) && !string.IsNullOrEmpty(user.PassportNumber))
                    user.NationalID = string.Empty;
                else if (!string.IsNullOrEmpty(user.NationalID))
                    user.PassportNumber = string.Empty;

                _db.Users.Add(user);      // <-- username and password saved here
                await _db.SaveChangesAsync();

                return RedirectToAction("Login", "Account");  // send user to login page
            }

            return View(user);
        }
    }
}
