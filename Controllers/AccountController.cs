using CarRentalManagement.Data;
using CarRentalManagement.Models;
using CarRentalManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CarRentalManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _auth;
        private readonly ApplicationDbContext _db;
        public AccountController(IAuthService auth, ApplicationDbContext db)
        {
            _auth = auth; _db = db;
        }


        [HttpGet]
        public IActionResult Login(string? error = null)
        {
            ViewBag.Error = error;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _auth.ValidateUserAsync(username, password);
            if (user == null)
            {
                ViewBag.Error = "Invalid username or password";
                return View();
            }
            HttpContext.Session.SetInt32("UserID", user.UserID);
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Role);
            return RedirectToAction("Index", "Home");
        }


       

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}