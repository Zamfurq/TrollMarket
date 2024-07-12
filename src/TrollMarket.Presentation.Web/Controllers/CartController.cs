using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrollMarket.Presentation.Web.Models;
using TrollMarket.Presentation.Web.Services;

namespace TrollMarket.Presentation.Web.Controllers
{
    [Authorize (Roles = "Buyer")]
    [Route("Cart")]
    [ApiController]
    public class CartController : Controller
    {

        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            int maxPage = 10;
            string username = User.Claims.Where(c => c.Type == "username").Select(c => c.Value).SingleOrDefault();
            var vm = _cartService.GetAllCarts(page, maxPage, username);
            return View(vm);
        }

        [HttpGet("Sorry")]
        public IActionResult Sorry()
        {
            return View("Sorry");
        }

        [HttpGet("delete")]
        public IActionResult Delete(int merchId) {
            string username = User.Claims.Where(c => c.Type == "username").Select(c => c.Value).SingleOrDefault();
            _cartService.Delete(merchId, username);
            return RedirectToAction("Index");
        }
    }
}
