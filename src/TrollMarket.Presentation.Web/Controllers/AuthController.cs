using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using TrollMarket.Presentation.Web.Models;
using TrollMarket.Presentation.Web.Services;
using TrollMarket.DataAccess.Models.Enum;

namespace TrollMarket.Presentation.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILogger _logger;
        private readonly AuthService _services;

        public AuthController(ILogger<AuthController> logger, AuthService services)
        {
            _logger = logger;
            _services = services;
        }

        public IActionResult Login()
        {
            return View(new UserLoginViewModel { Roles = _services.GetRoles()});
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginViewModel vm)
        {
            vm.Roles = _services.GetRoles();
            try
            {
                var authTicket = _services.LoginUser(vm);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                        authTicket.Principal, authTicket.Properties);

                return RedirectToAction("Index","Home");
            } catch (Exception e)
            {
                ViewBag.ErrorMessage = e.Message;
                return View(vm);
            }
        }

        [HttpGet("Register/{role}")]
        public IActionResult Register(string role)
        {

            return View(new UserRegisterViewModel { Role = role});
        }

        [HttpPost("Register/{role}")]
        public IActionResult Register(string role, UserRegisterViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Role = role;
                return View("Register", vm);
            }
            _services.Register(vm);
            return RedirectToAction("");
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("");
        }

        [HttpGet("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
