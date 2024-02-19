using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Rotativa.AspNetCore;
using Smart_Garage.Helpers;
using Smart_Garage.Models.DTOs.ResponseDTOs;
using Smart_Garage.Models.QueryParameters;
using Smart_Garage.Models.ViewModel;
using Smart_Garage.Services.Contracts;
using System.Net.Http;

namespace Smart_Garage.Controllers.MVC
{

    // signalR
    public class Customers_VisitsController : Controller
    {
        private readonly IUserService userService;
        private readonly IMapper autoMapper;
        private readonly IVisitService visitService;
        private readonly IVehicleService vehicleService;

        public Customers_VisitsController(IUserService userService, IMapper autoMapper, IVisitService visitService, IVehicleService vehicleService)
        {
            this.userService = userService;
            this.autoMapper = autoMapper;
            this.visitService = visitService;
            this.vehicleService = vehicleService;
        }

        [HttpGet]
        [IsAuthenticated]
        public async Task<IActionResult> Index()
        {
            string username = HttpContext.Session.GetString("user");

            var visitQueryParameters = new VisitQueryParameters();
            visitQueryParameters.User = username;

            if (TempData.ContainsKey("LicensePlate"))
                visitQueryParameters.LicensePlate = TempData["LicensePlate"] as string;

            if (TempData.ContainsKey("StartDate"))
                visitQueryParameters.StartDate = TempData["StartDate"] as string;

            if (TempData.ContainsKey("EndDate"))
                visitQueryParameters.EndDate = TempData["EndDate"] as string;

            var visitsResponseDTO = visitService.FilterBy(visitQueryParameters);

            var visitsViewModel = autoMapper.Map<IList<VisitViewModel>>(visitsResponseDTO);

            var licensePlates = vehicleService.GetLicensePlateByUser(username);
            ViewData["Vehicles"] = licensePlates;

            return View(visitsViewModel);
        }

        [HttpPost]
        [IsAuthenticated]
        public IActionResult Index(string id)
        {
            return RedirectToAction("Detail", "Customers_Visits", new { newId = id });
        }

        [HttpPost]
        [IsAuthenticated]
        public IActionResult FilterByLicensePlate(string licensePlate, DateTime? startDate, DateTime? endDate)
        {
            if (licensePlate == "All")
                TempData.Remove("LicensePlate");
            else
                TempData["LicensePlate"] = licensePlate;

            if(startDate == null)
                TempData.Remove("StartDate");
            else
                TempData["StartDate"] = startDate?.ToString("dd/MM/yyyy");

            if (startDate != null && endDate == null)
                TempData["EndDate"] = DateTime.Now.ToString("dd/MM/yyyy");
            else if (endDate == null)
                TempData.Remove("EndDate");
            else
                TempData["EndDate"] = endDate?.ToString("dd/MM/yyyy");

            return RedirectToAction("Index", "Customers_Visits");
        }

        [HttpGet]
        [IsAuthenticated]
        public IActionResult Detail(int newId)
        {
            //TODO add Date in the View
            var visitResponseDTO = visitService.GetById(newId);
            var visitViewModel = autoMapper.Map<VisitViewModel>(visitResponseDTO);

            return View(visitViewModel);
        }

        [HttpPost]
        [IsAuthenticated]
        public IActionResult Detail(string Id)
        {
            return RedirectToAction("GeneratePdf", "Customers_Visits", new { id = Id });
        }

        [HttpGet]
        [IsAuthenticated]
        public IActionResult GeneratePdf(int id)
        {
            var visitResponseDTO = visitService.GetById(id);
            var visitViewModel = autoMapper.Map<VisitViewModel>(visitResponseDTO);

            var pdf = new ViewAsPdf("Detail", visitViewModel);
            return pdf;
        }
    }
}
