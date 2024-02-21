using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using Smart_Garage.Exceptions;
using Smart_Garage.Helpers;
using Smart_Garage.Models;
using Smart_Garage.Models.Contracts;
using Smart_Garage.Models.DTOs.RequestDTOs;
using Smart_Garage.Models.QueryParameters;
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
                HttpContext.Session.SetString("isAdmin", user.IsAdmin.ToString());

                if(user.IsAdmin)
                    return RedirectToAction("Index", "Admin_Visits");
                else
                    return RedirectToAction("Index", "Customers_Visits");

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

        [HttpGet("Admin_Users/SetPassword/{email}")]
        public IActionResult SetPassword([FromRoute] string email)
        {
            var setPasswordViewModel = new SetPasswordViewModel();
            setPasswordViewModel.Email = email;

			return View(setPasswordViewModel);
        }

        [HttpPost]
        public IActionResult SetPassword(SetPasswordViewModel setPasswordViewModel)
        {
			if (!ModelState.IsValid)
				return View(setPasswordViewModel);

			if (setPasswordViewModel.Password != setPasswordViewModel.ConfirmPassword)
			{
				ModelState.AddModelError("ConfirmPassword", "The password and confirmation password do not match.");

				return View(setPasswordViewModel);
			}

            userService.SetPassword(setPasswordViewModel.Email, setPasswordViewModel.Password);

			return RedirectToAction("SignIn", "Admin_Users");
		}

        [HttpGet]
		public async Task<IActionResult> SendEmail()
        {
            return View(new SendEmailViewModel());
		}

        [HttpPost]
        public IActionResult SendEmail(SendEmailViewModel userEmail)
        {
            if (!userService.EmailExists(userEmail.Email))
            {
                ModelState.AddModelError("Email", "No profile with such email found !");
                return View();
            }

            var message = $"<p>Dear User,</p><br><p>It seems that you've forgotten your password for accessing our platform. Not to worry, we've got you covered! To reset your password and regain access to your account, please follow the link below:</p><br><p><a href=\"http://localhost:5068/Admin_Users/SetPassword/{userEmail.Email}\">[Password Reset Link]</a></p><br><p>If you did not initiate this password reset request, please ignore this email or contact our support team immediately.</p><p>Thank you for your attention to this matter.</p><br><p>Best regards,<br>The team of Smart-Garage</p>";

            userService.SendEmailLogic(userEmail, message);

            return RedirectToAction("SignIn", "Admin_Users");
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
