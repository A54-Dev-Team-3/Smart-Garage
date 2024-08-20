using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Smart_Garage.Helpers;
using Smart_Garage.Models.DTOs.RequestDTOs;
using Smart_Garage.Models.DTOs.ResponseDTOs;
using Smart_Garage.Models.QueryParameters;
using Smart_Garage.Models.ViewModel;
using Smart_Garage.Repositories.QueryParameters;
using Smart_Garage.Services;
using Smart_Garage.Services.Contracts;

namespace Smart_Garage.Controllers.MVC
{
    public class Admin_VehiclesController : Controller
    {
        private readonly IVehicleService vehicleService;
        private readonly IMapper autoMapper;
        private readonly IBrandService brandService;
        private readonly IModelService modelService;

        public Admin_VehiclesController(IVehicleService vehicleService, IMapper autoMapper, IBrandService brandService, IModelService modelService)
        {
            this.vehicleService = vehicleService;
            this.autoMapper = autoMapper;
            this.brandService = brandService;
            this.modelService = modelService;
        }

        [HttpGet]
        [IsAuthenticated]
        public IActionResult Index()
        {
            var vehicleResponseDTO = vehicleService.GetAll();

            var vehicleViewModels = autoMapper.Map<IList<VehicleViewModel>>(vehicleResponseDTO);

            return View(vehicleViewModels);
        }

        [HttpPost]
        [IsAuthenticated]
        public IActionResult Index(string searchOption, string searchString)
        {
            var vehicleQueryParameters = new VehicleQueryParameters();

            switch (searchOption)
            {
                case "Brand":
                    vehicleQueryParameters.Brand = searchString;
                    break;
                case "LicensePlate":
                    vehicleQueryParameters.LicensePlate = searchString;
                    break;
                case "Model":
                    vehicleQueryParameters.Model = searchString;
                    break;
                case "VIN":
                    vehicleQueryParameters.VIN = searchString;
                    break;
                case "Owner":
                    vehicleQueryParameters.Owner = searchString;
                    break;
                case "YearOfCreation":
                    vehicleQueryParameters.YearOfCreation = searchString;
                    break;
            }

            var vehicleResponseDTOs = vehicleService.FilterBy(vehicleQueryParameters);
            var vehicleViewModels = autoMapper.Map<IList<VehicleViewModel>>(vehicleResponseDTOs);

            return View(vehicleViewModels);
        }

        [HttpGet]
        [IsAuthenticated]
        public IActionResult Create()
        {
            var vehicleViewModel = new CreateVehicleViewModel();

            vehicleViewModel.Brands = autoMapper.Map<IList<BrandViewModel>>(brandService.GetAll());

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

            return RedirectToAction("Index", "Admin_Vehicle");
        }

        private string GetUsername()
        {
            return HttpContext.Session.GetString("user");
        }
    }
}
