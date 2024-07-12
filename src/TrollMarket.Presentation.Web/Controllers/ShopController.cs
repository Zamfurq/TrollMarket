using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrollMarket.Presentation.Web.Services;

namespace TrollMarket.Presentation.Web.Controllers
{
    [Authorize(Roles = "Buyer")]
    [Route("Shop")]
    public class ShopController : Controller
    {
        private readonly ShopService _service;

        public ShopController(ShopService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Index(int page = 1, string name = "", string category = "")
        {
            int pageSize = 10;
            string username = User.Claims.Where(c => c.Type == "username").Select(c => c.Value).SingleOrDefault();
            var vm = _service.GetAvailableProduct(page, pageSize, username, name, category);
            return View(vm);
        }


    }
}
