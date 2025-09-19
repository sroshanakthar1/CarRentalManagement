using Microsoft.AspNetCore.Mvc;

namespace CarRentalManagement.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
