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
using Smart_Garage.Models.QueryParameters;
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
        private readonly IPartService partService;
        private readonly IServiceService serviceService;
        private readonly IMechanicService mechanicService;
        private readonly IBrandService brandService;
        private readonly IModelService modelService;

        public Admin_VisitsController(IMapper autoMapper, IVisitService visitService,
            IVehicleService vehicleService, IUserService userService, IPartService partService,
            IServiceService serviceService, IMechanicService mechanicService, IBrandService brandService,
            IModelService modelService)
        {
            this.autoMapper = autoMapper;
            this.visitService = visitService;
            this.vehicleService = vehicleService;
            this.userService = userService;
            this.partService = partService;
            this.serviceService = serviceService;
            this.mechanicService = mechanicService;
            this.brandService = brandService;
            this.modelService = modelService;
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

        [HttpPost]
        [IsAuthenticated]
        public IActionResult Index(string searchOption, string searchString, DateTime? startDate, DateTime? endDate, string sortBy, string SortOrder)
        {
            var visitQueryParameters = new VisitQueryParameters();

            switch (searchOption)
            {
                case "Username":
                    visitQueryParameters.User = searchString;
                    break;
                case "VehicleBrand":
                    visitQueryParameters.Brand = searchString;
                    break;
                case "VehicleModel":
                    visitQueryParameters.Model = searchString;
                    break;
                case "LicensePlate":
                    visitQueryParameters.LicensePlate = searchString;
                    break;
            }

            switch (sortBy)
            {
                case "Username":
                    visitQueryParameters.SortBy = sortBy;
                    break;
            }

            if (startDate != null)
                visitQueryParameters.StartDate = startDate?.ToString("dd/MM/yyyy");

            if (startDate != null && endDate == null)
                visitQueryParameters.EndDate = DateTime.Now.ToString("dd/MM/yyyy");
            else if (endDate == null)
                TempData.Remove("EndDate");
            else
                visitQueryParameters.EndDate = endDate?.ToString("dd/MM/yyyy");

            var visitsResponseDTO = visitService.FilterBy(visitQueryParameters);

            var visitsViewModel = autoMapper.Map<IList<VisitViewModel>>(visitsResponseDTO);

            return View(visitsViewModel);
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
        public IActionResult Create(VisitViewModel visitViewModel)
        {
            return View();
        }

        [HttpGet]
        [IsAuthenticated]
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

            var vehicleViewModel = autoMapper.Map<VehicleVisitViewModel>(vehicleResponseDTO);
            var customerViewModel = autoMapper.Map<CustomerVisitViewModel>(vehicleResponseDTO.User);

            var visitViewModel = new VisitViewModel();
            visitViewModel.Vehicle = vehicleViewModel;
            visitViewModel.User = customerViewModel;

            string serializedVisitViewModel = JsonConvert.SerializeObject(visitViewModel);
            TempData["VisitViewModel"] = serializedVisitViewModel;

            return RedirectToAction("Create", "Admin_Visits");
        }

        [HttpGet]
        [IsAuthenticated]
        public IActionResult Detail([FromRoute] string id)
        {
            var visitResponseDTO = visitService.GetById(int.Parse(id));
            var visitViewModel = autoMapper.Map<VisitViewModel>(visitResponseDTO);

            visitViewModel.Parts = autoMapper.Map<IList<PartViewModel>>(partService.GetAll());
            visitViewModel.Services = autoMapper.Map<IList<ServiceViewModel>>(serviceService.GetAll());
            visitViewModel.Mechanics = autoMapper.Map<IList<MechanicViewModel>>(mechanicService.GetAll());

            return View(visitViewModel);
        }

        [HttpPost]
        [IsAuthenticated]
        public IActionResult Detail(VisitViewModel visitViewModel)
        {
            var visitRequestDTO = autoMapper.Map<VisitRequestDTO>(visitViewModel);
            visitService.Update(visitRequestDTO);
            return RedirectToAction("Index", "Admin_Visits");
        }

        [HttpGet]
        [IsAuthenticated]
        public IActionResult CreateVisitForNewCustomer()
        {
            var visitViewModel = new VisitViewModel();

            visitViewModel.Brands = autoMapper.Map<IList<BrandViewModel>>(brandService.GetAll());

            return View(visitViewModel);
        }

        [HttpPost]
        [IsAuthenticated]
        public IActionResult CreateVisitForNewCustomer(VisitViewModel visitViewModel)
        {
            return View();
        }
    }
}
