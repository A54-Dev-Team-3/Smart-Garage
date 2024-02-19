using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Smart_Garage.Helpers;
using Smart_Garage.Models.DTOs.RequestDTOs;
using Smart_Garage.Models.DTOs.ResponseDTOs;
using Smart_Garage.Models.ViewModel;
using Smart_Garage.Repositories.Contracts;
using Smart_Garage.Services;
using Smart_Garage.Services.Contracts;

namespace Smart_Garage.Controllers.MVC
{
    public class Admin_MechanicsController : Controller
    {
        private readonly IMechanicService mechanicService;
        private readonly IMapper autoMapper;

        public Admin_MechanicsController(IMechanicService mechanicService, IMapper autoMapper)
        {
            this.mechanicService = mechanicService;
            this.autoMapper = autoMapper;
        }

        [HttpGet]
        [IsAuthenticated]
        public IActionResult Index()
        {
            var mechanicResponseDTO = mechanicService.GetAll();

            var mechanicViewModels = autoMapper.Map<IList<MechanicViewModel>>(mechanicResponseDTO);

            return View(mechanicViewModels);
        }

        [HttpGet]
        [IsAuthenticated]
        public IActionResult Create()
        {
            var createMechanicViewModel = new CreateMechanicViewModel();
            return View(createMechanicViewModel);
        }

        [HttpPost]
        [IsAuthenticated]
        public IActionResult Create(CreateMechanicViewModel createdMechanicViewModel)
        {
            if (!ModelState.IsValid)
                return View();

            mechanicService.Create(autoMapper.Map<MechanicRequestDTO>(createdMechanicViewModel));
            return RedirectToAction("Index", "Admin_Mechanics");
        }

        [HttpGet]
        [IsAuthenticated]
        public IActionResult Detail([FromRoute] string id)
        {
            var mechanicResponseDTO = mechanicService.GetById(int.Parse(id));
            var mechanicViewModel = autoMapper.Map<MechanicViewModel>(mechanicResponseDTO);
            return View(mechanicViewModel);
        }

        [HttpPost]
        [IsAuthenticated]
        public IActionResult Detail(MechanicViewModel mechanicViewModel)
        {
            return View();
        }
    }
}
