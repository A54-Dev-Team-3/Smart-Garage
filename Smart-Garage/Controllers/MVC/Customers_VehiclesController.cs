using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Smart_Garage.Helpers;
using Smart_Garage.Models.DTOs.ResponseDTOs;
using Smart_Garage.Models.QueryParameters;
using Smart_Garage.Models.ViewModel;
using Smart_Garage.Repositories.QueryParameters;
using Smart_Garage.Services;
using Smart_Garage.Services.Contracts;

namespace Smart_Garage.Controllers.MVC
{
    public class Customers_VehiclesController : Controller
    {
        private readonly IUserService userService;
        private readonly IMapper autoMapper;
        private readonly IVehicleService vehicleService;
        public Customers_VehiclesController(IUserService userService, IMapper autoMapper, IVehicleService vehicleService)
        {
            this.userService = userService;
            this.autoMapper = autoMapper;
            this.vehicleService = vehicleService;
        }

        [HttpGet]
        [IsAuthenticated]
        public IActionResult Index()
        {
            string username = HttpContext.Session.GetString("user");
            var userVehiclesResponseDTO = userService.GetByName(username).Vehicles;
            var vehicleViewModel = autoMapper.Map<IList<VehicleViewModel>>(userVehiclesResponseDTO);
            
            return View(vehicleViewModel);
        }

        [HttpPost]
        [IsAuthenticated]
        public IActionResult Index(string id)
        {
            return RedirectToAction("Detail", "Customers_Vehicles", new { newId = id });
        }

        [HttpGet]
        [IsAuthenticated]
        public IActionResult Detail(int newId)
        {
            return View();
        }
    }
}
