using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Smart_Garage.Models.ViewModel;
using Smart_Garage.Services;
using Smart_Garage.Services.Contracts;

namespace Smart_Garage.Controllers.MVC
{
    public class Admin_VisitsController : Controller
    {
        private readonly IMapper autoMapper;
        private readonly IVisitService visitService;

        public Admin_VisitsController(IMapper autoMapper, IVisitService visitService)
        {
            this.autoMapper = autoMapper;
            this.visitService = visitService;
        }

        public IActionResult Index()
        {
            string currentUser = HttpContext.Session.GetString("user");

            var visitResponseDTO = visitService.GetAll(currentUser);

            var visitViewModels = autoMapper.Map<IList<VisitViewModel>>(visitResponseDTO);

            return View(visitViewModels);
        }
    }
}
