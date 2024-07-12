using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrollMarket.Presentation.Web.Models;
using TrollMarket.Presentation.Web.Services;

namespace TrollMarket.Presentation.Web.Controllers
{
    [Authorize (Roles = "Admin")]
    [Route("Admin")]
    public class AdminController : Controller
    {
        private readonly AdminService _service;

        public AdminController(AdminService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(AdminRegisterViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                return View(vm);
            }
            _service.InsertAdmin(vm);
            return View("Confirmation");
        }
    }
}
