using Microsoft.AspNetCore.Mvc;

namespace CarRentalManagement.Controllers
{
    public class BookingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
