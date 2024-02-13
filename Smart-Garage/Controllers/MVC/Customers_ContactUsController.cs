using Microsoft.AspNetCore.Mvc;
using Smart_Garage.Helpers;

namespace Smart_Garage.Controllers.MVC
{
    public class Customers_ContactUsController : Controller
    {
        [HttpGet]
        [IsAuthenticated]
        public IActionResult Index()
        {
            return View();
        }
    }
}
