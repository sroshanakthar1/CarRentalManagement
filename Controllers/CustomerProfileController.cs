using Microsoft.AspNetCore.Mvc;

namespace CarRentalManagement.Controllers
{
    public class CustomerProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
