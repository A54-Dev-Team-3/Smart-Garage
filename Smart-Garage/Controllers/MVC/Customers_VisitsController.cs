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
    public class Customers_VisitsController : Controller
    {
        private readonly IUserService userService;
        private readonly IMapper autoMapper;
        private readonly IVisitService visitService;
        public Customers_VisitsController(IUserService userService, IMapper autoMapper, IVisitService visitService)
        {
            this.userService = userService;
            this.autoMapper = autoMapper;
            this.visitService = visitService;
        }

        [HttpGet]
        [IsAuthenticated]
        public async Task<IActionResult> Index()
        {
            string username = HttpContext.Session.GetString("user");

            var visitQueryParameters = new VisitQueryParameters();
            visitQueryParameters.User = username;

            var visitsResponseDTO = visitService.FilterBy(visitQueryParameters, username);

            var visitsViewModel = autoMapper.Map<IList<VisitViewModel>>(visitsResponseDTO);

            return View(visitsViewModel);
        }

        [HttpPost]
        [IsAuthenticated]
        public IActionResult Index(string id)
        {
            return RedirectToAction("Detail", "Customers_Visits", new { newId = id });
        }

        [HttpGet]
        [IsAuthenticated]
        public IActionResult Detail(int newId)
        {
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
