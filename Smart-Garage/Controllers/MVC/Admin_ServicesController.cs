using Microsoft.AspNetCore.Mvc;

namespace Smart_Garage.Controllers.MVC
{
    public class Admin_ServicesController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
