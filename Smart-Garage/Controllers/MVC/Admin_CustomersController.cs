using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
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
            var customerResponseDTO = userService.GetAllNotAdmins();

            var customerViewModels = autoMapper.Map<IList<CustomerViewModel>>(customerResponseDTO);

            return View(customerViewModels);
        }

		[HttpPost]
		[IsAuthenticated]
		public IActionResult Index(int customerId)
		{
            return RedirectToAction("Detail", "Admin_Customers", new { id = customerId} );
        }

        [HttpGet("Customers/Detail/{id}")]
        [IsAuthenticated]
        public IActionResult Detail([FromRoute] int id)
        {

			var userReponseDTO = userService.GetById(id);

			var customerViewModel = autoMapper.Map<CustomerViewModel>(userReponseDTO);

            return View(customerViewModel);
        }

        [HttpPost("Customers/Detail/{id}")]
        [IsAuthenticated]
        public IActionResult Detail([FromRoute] int? customerId)
        {

            return RedirectToAction("Detail", "Admin_Customers", new { id = customerId });
        }

        [HttpPost]
		[IsAuthenticated]
		public IActionResult Update(CustomerViewModel customerViewModel)
		{
			var updateUserRequetDTO = autoMapper.Map<UpdateUserRequestDTO>(customerViewModel);
            userService.Update(customerViewModel.Id, updateUserRequetDTO, updateUserRequetDTO.Username);

            return RedirectToAction("Detail", "Admin_Customers", new { id = customerViewModel.Id });
        }

		[HttpGet]
        [IsAuthenticated]
        public IActionResult SignUp()
		{
			var signUpViewModel = new SignUpViewModel();

			return View(signUpViewModel);
		}

		[HttpPost]
        [IsAuthenticated]
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

			return RedirectToAction("Index", "Admin_Customers"); // TODO
		}

        [HttpGet] // TODO: [Question] Shouldn't be "HttpDelete"
        [IsAuthenticated]
        public IActionResult Delete(int id)
        {
            string currentUser = HttpContext.Session.GetString("user");
            userService.Delete(id, currentUser);

            return RedirectToAction("Index", "Admin_Customers");
        }
    }
}
