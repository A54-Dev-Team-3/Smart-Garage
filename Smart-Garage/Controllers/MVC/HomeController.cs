using Microsoft.AspNetCore.Mvc;
using Smart_Garage.Exceptions;
using Smart_Garage.Helpers;
using Smart_Garage.Models.ViewModel;

namespace Smart_Garage.Controllers.MVC
{
    public class HomeController : Controller
    {
        private readonly AuthManager authManager;

        public HomeController(AuthManager authManager)
        {
            this.authManager = authManager;
        }

        [HttpGet]
        [IsAuthenticated]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            var loginViewModel = new LoginViewModel();

            return View(loginViewModel);
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginviewModel)
        {
            if (!ModelState.IsValid)
                return View(loginviewModel);

            try
            {
                var user = authManager.AuthenticateUser(loginviewModel.Username, loginviewModel.Password);
                HttpContext.Session.SetString("user", user.Username);
                HttpContext.Session.SetString("id", user.Id.ToString());

                return RedirectToAction("Index", "Home");
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
    }
}
