using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index()
        {
            string currentUser = HttpContext.Session.GetString("user");

            var mechanicResponseDTO = mechanicService.GetAll(currentUser);

            var mechanicViewModels = autoMapper.Map<IList<MechanicViewModel>>(mechanicResponseDTO);

            return View(mechanicViewModels);
        }
    }
}
