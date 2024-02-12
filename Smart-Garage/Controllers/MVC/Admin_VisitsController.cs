using AutoMapper;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Rotativa.AspNetCore;
using Smart_Garage.Helpers;
using Smart_Garage.Models;
using Smart_Garage.Models.DTOs.RequestDTOs;
using Smart_Garage.Models.DTOs.ResponseDTOs;
using Smart_Garage.Models.ViewModel;
using Smart_Garage.Repositories.QueryParameters;
using Smart_Garage.Services;
using Smart_Garage.Services.Contracts;

namespace Smart_Garage.Controllers.MVC
{
    public class Admin_VisitsController : Controller
    {
        private readonly IMapper autoMapper;
        private readonly IVisitService visitService;
        private readonly IVehicleService vehicleService;
        private readonly IUserService userService;

        public Admin_VisitsController(IMapper autoMapper, IVisitService visitService, IVehicleService vehicleService, IUserService userService)
        {
            this.autoMapper = autoMapper;
            this.visitService = visitService;
            this.vehicleService = vehicleService;
            this.userService = userService;
        }

        [HttpGet]
        [IsAuthenticated]
        public IActionResult Index()
        {
            string currentUser = HttpContext.Session.GetString("user");

            var visitResponseDTO = visitService.GetAll(currentUser);

            var visitViewModels = autoMapper.Map<IList<VisitViewModel>>(visitResponseDTO);

            return View(visitViewModels);
        }

        [HttpGet]
        [IsAuthenticated]
        public IActionResult Create()
        {
            string serializedVisitViewModel = TempData["VisitViewModel"] as string;
            VisitViewModel visitViewModel = JsonConvert.DeserializeObject<VisitViewModel>(serializedVisitViewModel);
            return View(visitViewModel);
        }

        [HttpPost]
        [IsAuthenticated]
        public async Task<IActionResult> Create(VisitViewModel visitViewModel)
        {
            return View();
        }

        [HttpGet]
        public IActionResult SearchByLicensePlate()
        {
            var vehicleViewModel = new VehicleViewModel();
            return View(vehicleViewModel);
        }

        [HttpPost]
        [IsAuthenticated]
        public IActionResult SearchByLicensePlate(string licensePlate)
        {
            if (string.IsNullOrEmpty(licensePlate))
            {
                ModelState.AddModelError("Vehicle.LicensePlate", "The License Plate field is required !");
                return View(licensePlate);
            }

            var vehicleResponseDTO = vehicleService.FilterByLicensePlate(licensePlate);

            if (vehicleResponseDTO == null)
            {
                ModelState.AddModelError("Vehicle.LicensePlate", "No vehicle found with the provided license plate !");
                return View(licensePlate);
            }

            var vehicleViewModel = autoMapper.Map<VehicleViewModel>(vehicleResponseDTO);
            var customerViewModel = autoMapper.Map<CustomerViewModel>(vehicleResponseDTO.User);

            var visitViewModel = new VisitViewModel();
            visitViewModel.Vehicle = vehicleViewModel;
            visitViewModel.Vehicle.User = customerViewModel;

            string serializedVisitViewModel = JsonConvert.SerializeObject(visitViewModel);
            TempData["VisitViewModel"] = serializedVisitViewModel;

            return RedirectToAction("Create", "Admin_Visits");
            //return RedirectToAction("Detail", "Admin_Customers", new { id = customerId });
        }

        [HttpGet]
        [IsAuthenticated]
        public IActionResult Detail(int id)
		{
			return View();
		}  
    }
}
