using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Smart_Garage.Helpers;
using Smart_Garage.Models.DTOs.RequestDTOs;
using Smart_Garage.Models.ViewModel;
using Smart_Garage.Services;
using Smart_Garage.Services.Contracts;

namespace Smart_Garage.Controllers.MVC
{
	public class Admin_CustomersController : Controller
	{

		private readonly IUserService userService;
		private readonly IMapper autoMapper;

		public Admin_CustomersController(IUserService userService, IMapper autoMapper)
		{
			this.autoMapper = autoMapper;
			this.userService = userService;
		}

        [HttpGet]
        [IsAuthenticated]
        public IActionResult Index()
        {
            string currentUser = HttpContext.Session.GetString("user");

            var customerResponseDTO = userService.GetAllNotAdmins();

            var customerViewModels = autoMapper.Map<IList<MechanicViewModel>>(customerResponseDTO);

            return View(customerViewModels);
        }

        [HttpGet]
        //[IsAuthenticated]
        public IActionResult SignUp()
		{
			var signUpViewModel = new SignUpViewModel();

			return View(signUpViewModel);
		}

		[HttpPost]
        //[IsAuthenticated]
        public IActionResult SignUp(SignUpViewModel signUpViewModel)
		{
			if (!ModelState.IsValid)
				return View(signUpViewModel);

			//if (signUpViewModel.Password != signUpViewModel.ConfirmPassword)
			//{
			//	ModelState.AddModelError("ConfirmPassword", "The password and confirmation password do not match.");
			//	return View(signUpViewModel);
			//}

			var signUpUserRequestDTO = autoMapper.Map<SignUpUserRequestDTO>(signUpViewModel);
			_ = userService.Create(signUpUserRequestDTO);

			return RedirectToAction("Detail", "Admin_Customers"); // TODO
		}
	}
}
