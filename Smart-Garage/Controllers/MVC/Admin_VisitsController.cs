using AutoMapper;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Smart_Garage.Helpers;
using Smart_Garage.Models;
using Smart_Garage.Models.ViewModel;
using Smart_Garage.Services;
using Smart_Garage.Services.Contracts;

namespace Smart_Garage.Controllers.MVC
{
    public class Admin_VisitsController : Controller
    {
        private readonly IMapper autoMapper;
        private readonly IVisitService visitService;
        private readonly SGContext _dbContext;
        private readonly IVehicleService vehicleService;

        public Admin_VisitsController(IMapper autoMapper, IVisitService visitService, SGContext _dbContext, IVehicleService vehicleService)
        {
            this.autoMapper = autoMapper;
            this.visitService = visitService;
            this._dbContext = _dbContext;
            this.vehicleService = vehicleService;
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
			var visitViewModel = new CreateVisitGetViewModel();
            visitViewModel.Vehicles = vehicleService.GetAll();

            return View(visitViewModel);
		}

		[HttpGet]
        [IsAuthenticated]
        public IActionResult Detail(int id)
		{
			//var visitViewModel = new VisitViewModel();

			return View();
		}

        public IActionResult GetLicensePlateSuggestions(string inputText)
        {
            // Query your database to fetch suggestions based on inputText
            var suggestions = _dbContext.Vehicles
                .Where(v => v.LicensePlate.StartsWith(inputText))
                .Select(v => v.LicensePlate)
                .ToList();

            return Json(suggestions);
        }

		[HttpGet]
		public IActionResult AutocompleteLicensePlates(string term)
		{
			var suggestions = _dbContext.Vehicles
				.Where(v => v.LicensePlate.StartsWith(term))
				.Select(v => v.LicensePlate)
				.ToList();

			return Json(suggestions);
		}
	}
}
