using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Smart_Garage.Helpers;
using Smart_Garage.Models.ViewModel;
using Smart_Garage.Services;
using Smart_Garage.Services.Contracts;

namespace Smart_Garage.Controllers.MVC
{
    public class Admin_VehiclesController : Controller
    {
        private readonly IVehicleService vehicleService;
        private readonly IMapper autoMapper;

        public Admin_VehiclesController(IVehicleService vehicleService, IMapper autoMapper)
        {
            this.vehicleService = vehicleService;
            this.autoMapper = autoMapper;
        }

        [HttpGet]
        [IsAuthenticated]
        public IActionResult Index()
        {
            var vehicleResponseDTO = vehicleService.GetAll();

            var vehicleViewModels = autoMapper.Map<IList<VehicleViewModel>>(vehicleResponseDTO);

            return View(vehicleViewModels);
        }
    }
}
