using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Smart_Garage.Exceptions;
using Smart_Garage.Helpers;
using Smart_Garage.Models.DTOs.RequestDTOs;
using Smart_Garage.Models.DTOs.ResponseDTOs;
using Smart_Garage.Models.ViewModel;
using Smart_Garage.Services;
using Smart_Garage.Services.Contracts;
using System.Security.Claims;

namespace Smart_Garage.Controllers.MVC
{
    public class Admin_ServicesController : Controller
    {
        private readonly IServiceService serviceService;
        private readonly IMapper autoMapper;
        private readonly IUserService userService;

        public Admin_ServicesController(IUserService userService, IServiceService serviceService, IMapper autoMapper)
        {
            this.autoMapper = autoMapper;
            this.serviceService = serviceService;
            this.userService = userService;
        }

        [HttpGet]
        [IsAuthenticated]
        public IActionResult Index()
        {
            var serviceResponseDTO = serviceService.GetAll(GetUsername());

            var serviceViewModels = autoMapper.Map<IList<ServiceViewModel>>(serviceResponseDTO);

            return View(serviceViewModels);
        }

        [HttpGet]
        [IsAuthenticated]
        public IActionResult Create()
        {
            var serviceViewModel = new CreateServiceViewModel();

            return View(serviceViewModel);
        }

		[HttpPost]
		[IsAuthenticated]
		public IActionResult Create(CreateServiceViewModel serviceViewModel)
		{
			if (!ModelState.IsValid)
				return View(serviceViewModel);

			CreateServiceRequestDTO serviceRequestDTO = new CreateServiceRequestDTO()
            {
                Name = serviceViewModel.Name,
                Price = serviceViewModel.Price
            };
			_ = serviceService.Create(serviceRequestDTO, GetUsername());

			return RedirectToAction("Index", "Admin_Services");
		}

        [HttpGet]
        [IsAuthenticated]
        public IActionResult Detail([FromRoute] string id)
        {

            var serviceResponseDTO = serviceService.GetById(int.Parse(id), GetUsername());

            var serviceViewModel = autoMapper.Map<ServiceViewModel>(serviceResponseDTO);

            return View(serviceViewModel);
        }

        [HttpPost]
        [IsAuthenticated]
        public IActionResult ShowDetail(string serviceId)
        {

            return RedirectToAction("Detail", "Admin_Services", new { id = serviceId });
        }

        [HttpGet]
        [IsAuthenticated]
        public IActionResult Delete(int id)
        {
            serviceService.Delete(id, GetUsername());

            return RedirectToAction("Index", "Admin_Services");
        }

        [HttpPost]
        [IsAuthenticated]
        public IActionResult Update(ServiceViewModel serviceViewModel)
        {
            var updateServiceDTO = autoMapper.Map<UpdateServiceRequestDTO>(serviceViewModel);
            serviceService.Update(serviceViewModel.Id, updateServiceDTO, GetUsername());

            return RedirectToAction("Index", "Admin_Services");
        }

        private string GetUsername()
        {
            return HttpContext.Session.GetString("user");
		}
	}
}
