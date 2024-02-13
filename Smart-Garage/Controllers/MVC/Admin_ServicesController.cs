using Microsoft.AspNetCore.Mvc;
using Smart_Garage.Helpers;

namespace Smart_Garage.Controllers.MVC
{
    public class Admin_ServicesController : Controller
    {
        [HttpGet]
        [IsAuthenticated]
        public IActionResult Index()
        {
            return View();
        }
    }
}
