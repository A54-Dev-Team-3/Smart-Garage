using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Smart_Garage.Helpers;
using Smart_Garage.Models.ViewModel;
using Smart_Garage.Services;
using Smart_Garage.Services.Contracts;

namespace Smart_Garage.Controllers.MVC
{
    public class Customers_ProfileController : Controller
    {
        private readonly IUserService userService;
        private readonly IMapper autoMapper;
        public Customers_ProfileController(IUserService userService, IMapper autoMapper)
        {
            this.userService = userService;
            this.autoMapper = autoMapper;
        }

        [HttpGet]
        [IsAuthenticated]
        public IActionResult Index()
        {
            string username = HttpContext.Session.GetString("user");
            var customerResponseDTO = userService.GetByName(username);
            var customerViewModel = autoMapper.Map<CustomerViewModel>(customerResponseDTO);

            return View(customerViewModel);
        }
    }
}
