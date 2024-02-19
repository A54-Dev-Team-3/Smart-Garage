using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Smart_Garage.Helpers;
using Smart_Garage.Models.DTOs.RequestDTOs;
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

        [HttpGet]
        [IsAuthenticated]
        public IActionResult Create()
        {
            var vehicleViewModel = new CreateVehicleViewModel();

            return View(vehicleViewModel);
        }

        [HttpPost]
        [IsAuthenticated]
        public IActionResult Create(CreateVehicleViewModel vehicleViewModel)
        {
            if (!ModelState.IsValid)
                return View(vehicleViewModel);

            VehicleRequestDTO vehicleRequestDTO = new VehicleRequestDTO()
            {
                LicensePlate = vehicleViewModel.LicensePlate,
                VIN = vehicleViewModel.VIN,
                CreationYear = vehicleViewModel.CreationYear,
                Model = vehicleViewModel.Model,
                Brand = vehicleViewModel.Brand
            };
            _ = vehicleService.Create(GetUsername(), vehicleRequestDTO);

            return RedirectToAction("Index", "Admin_Vehicles");
        }

        [HttpGet]
        [IsAuthenticated]
        public IActionResult Detail([FromRoute] string id)
        {

            var vehicleResponseDTO = vehicleService.GetById(int.Parse(id));

            var vehicleViewModel = autoMapper.Map<VehicleViewModel>(vehicleResponseDTO);

            return View(vehicleViewModel);
        }

        [HttpPost]
        [IsAuthenticated]
        public IActionResult ShowDetail(string vehicleId)
        {

            return RedirectToAction("Detail", "Admin_Vehicles", new { id = vehicleId });
        }

        [HttpGet]
        [IsAuthenticated]
        public IActionResult Delete(int id)
        {
            vehicleService.Delete(id);

            return RedirectToAction("Index", "Admin_Vehicles");
        }

        [HttpPost]
        [IsAuthenticated]
        public IActionResult Update(VehicleViewModel vehicleViewModel)
        {
            var updateVehicleDTO = autoMapper.Map<VehicleRequestDTO>(vehicleViewModel);
            vehicleService.Update(vehicleViewModel.Id, updateVehicleDTO);

            return RedirectToAction("Detail", "Admin_Vehicle", new { id = vehicleViewModel.Id });
        }

        private string GetUsername()
        {
            return HttpContext.Session.GetString("user");
        }
    }
}
