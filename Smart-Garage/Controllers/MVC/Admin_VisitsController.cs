using AutoMapper;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet("Admin_VisitsController/Create")]
        [IsAuthenticated]
        public IActionResult Create()
        {
            var visitViewModel = new VisitViewModel();
            return View(visitViewModel);
        }

        [HttpPost("Admin_VisitsController/Create")]
        [IsAuthenticated]
        public IActionResult Create(VisitViewModel visitViewModel)
        {
			if (string.IsNullOrEmpty(visitViewModel.Vehicle.LicensePlate))
			{
				ModelState.AddModelError("Vehicle.LicensePlate", "The License Plate field is required !");
				return View(visitViewModel);
			}

			var vehicleResponseDTO = vehicleService.FilterByLicensePlate(visitViewModel.Vehicle.LicensePlate);
            //var userResponseDTO = userService.GetById(vehicleResponseDTO.UserId);


            if (vehicleResponseDTO == null)
            {
                ModelState.AddModelError("Vehicle.LicensePlate", "No vehicle found with the provided license plate !");
                return View(visitViewModel);
            }

            visitViewModel.Vehicle = autoMapper.Map<VehicleViewModel>(vehicleResponseDTO);
            visitViewModel.Vehicle.User = autoMapper.Map<CustomerViewModel>(vehicleResponseDTO.User);
            //visitViewModel.Vehicle.User = autoMapper.Map<CustomerViewModel>(userResponseDTO);

            return View(visitViewModel);
        }

        [HttpGet]
        [IsAuthenticated]
        public IActionResult Detail(int id)
		{
			//var visitViewModel = new VisitViewModel();

			return View();
		}

	}
}
