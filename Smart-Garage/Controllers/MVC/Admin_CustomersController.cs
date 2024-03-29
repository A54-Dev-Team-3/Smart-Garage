﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Rotativa.AspNetCore;
using Smart_Garage.Helpers;
using Smart_Garage.Models.DTOs.RequestDTOs;
using Smart_Garage.Models.QueryParameters;
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
        public IActionResult Index(string searchOption, string searchString)
        {
            var userQueryParameters = new UserQueryParameters();

            switch (searchOption)
            {
                case "Username":
                    userQueryParameters.Username = searchString;
                    break;
                case "FirstName":
                    userQueryParameters.FirstName = searchString;
                    break;
                case "LastName":
                    userQueryParameters.LastName = searchString;
                    break;
                case "PhoneNumber":
                    userQueryParameters.PhoneNumber = searchString;
                    break;
                case "VehicleBrand":
                    userQueryParameters.Brand = searchString;
                    break;
                case "VehicleModel":
                    userQueryParameters.Model = searchString;
                    break;
            }

            var userResponseDTOs = userService.FilterBy(userQueryParameters);
            var customerViewModels = autoMapper.Map<IList<CustomerViewModel>>(userResponseDTOs);

            return View(customerViewModels);
        }

        [HttpGet]
        [IsAuthenticated]
        public IActionResult Detail([FromRoute] string id)
        {

			var userReponseDTO = userService.GetById(int.Parse(id));

			var customerViewModel = autoMapper.Map<CustomerViewModel>(userReponseDTO);

            return View(customerViewModel);
        }

        [HttpPost]
        [IsAuthenticated]
        public IActionResult ShowDetail(string customerId)
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

			var signUpUserRequestDTO = autoMapper.Map<SignUpUserRequestDTO>(signUpViewModel);
			_ = userService.Create(signUpUserRequestDTO);

            var userEmail = new SendEmailViewModel();
            userEmail.Email = signUpViewModel.Email;

            var message = $"<p>Dear User,</p><br><p>You have just created an account. Please click the link below to create a password:</p><br><p><a href=\"http://localhost:5068/Admin_Users/SetPassword/{userEmail.Email}\">[Password Reset Link]</a></p><br><p>Thank you for your attention to this matter.</p><br><p>Best regards,<br>The team of Smart-Garage</p>";

            userService.SendEmailLogic(userEmail, message);

            return RedirectToAction("Index", "Admin_Customers");
		}

        [HttpGet]
        [IsAuthenticated]
        public IActionResult Delete(int id)
        {
            string currentUser = HttpContext.Session.GetString("user");
            userService.Delete(id, currentUser);

            return RedirectToAction("Index", "Admin_Customers");
        }
    }
}
