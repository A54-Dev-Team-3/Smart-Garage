using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Smart_Garage.Exceptions;
using Smart_Garage.Helpers;
using Smart_Garage.Models.DTOs.RequestDTOs;
using Smart_Garage.Models.ViewModel;
using Smart_Garage.Services;
using Smart_Garage.Services.Contracts;

namespace Smart_Garage.Controllers.MVC
{
    public class Admin_UsersController : Controller
    {
        private readonly AuthManager authManager;
        private readonly IUserService userService;
		private readonly IMapper autoMapper;

		public Admin_UsersController(AuthManager authManager, IMapper autoMapper, IUserService userService)
        {
            this.authManager = authManager;
            this.autoMapper = autoMapper;
            this.userService = userService;
		}

        [HttpGet]
        public IActionResult SignIn()
        {
            var logInViewModel = new LogInViewModel();

            return View(logInViewModel);
        }

        [HttpPost]
        public IActionResult SignIn(LogInViewModel loginviewModel)
        {
            if (!ModelState.IsValid)
                return View(loginviewModel);

            try
            {
                var user = authManager.AuthenticateUser(loginviewModel.Username, loginviewModel.Password);
                HttpContext.Session.SetString("user", user.Username);
                HttpContext.Session.SetString("id", user.Id.ToString());

                return RedirectToAction("Index", "Admin_Visits");
            }
            catch (UnauthorizedOperationException ex)
            {
                ModelState.AddModelError("Username", ex.Message);
                ModelState.AddModelError("Password", ex.Message);

                return View(loginviewModel);
            }
            catch (WrongPasswordException ex)
            {
                ModelState.AddModelError("Username", ex.Message);
                ModelState.AddModelError("Password", ex.Message);
                return View(loginviewModel);
            }
        }

        [HttpGet]
        [IsAuthenticated]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user");
            HttpContext.Session.Remove("id");

            return RedirectToAction("SignIn", "Admin_Users");
        }
    }
}
