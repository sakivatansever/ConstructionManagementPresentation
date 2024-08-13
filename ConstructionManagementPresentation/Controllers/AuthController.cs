using ConstructionManagementPresentation.Dtos;
using ConstructionManagementPresentation.Services;
using ConstructionManagementPresentation.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ConstructionManagementPresentation.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _authService; 

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var registerDto = new RegisterDto
            {
                Username = model.Username,
                Password = model.Password
            };

            var success = await _authService.Register(registerDto); 

            if (success)
            {
                return RedirectToAction("Login"); 
            }
            else
            {
                ModelState.AddModelError("", "Kayıt işlemi başarısız.");
                ViewBag.hata = "Girdiğiniz bilgiler hatalı  ";
                View(model);


            }

   
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
      
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var loginDto = new LoginDto
            {
                Username = model.Username,
                Password = model.Password
            };

            var tokenResponse = await _authService.Login(loginDto); 

            if (tokenResponse != null)
            {
                HttpContext.Session.SetString("Token", tokenResponse.Token); 
                return RedirectToAction("Index", "Home"); 
            }

            ModelState.AddModelError("", "Giriş işlemi başarısız."); 
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Token"); 
            return RedirectToAction("Login");
        }
    }
}
