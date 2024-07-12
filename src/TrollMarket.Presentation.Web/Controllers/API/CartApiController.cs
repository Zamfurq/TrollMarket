using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrollMarket.Presentation.Web.Models;
using TrollMarket.Presentation.Web.Services;

namespace TrollMarket.Presentation.Web.Controllers.API
{
    [Authorize(Roles = "Buyer")]
    [Route("Cart")]
    [ApiController]
    public class CartApiController : Controller
    {
        private readonly CartService _cartService;

        public CartApiController(CartService cartService)
        {
            _cartService = cartService;
        }
        [HttpPost]
        public IActionResult Purchase(CartindexViewModel vm)
        {
            string username = User.Claims.Where(c => c.Type == "username").Select(c => c.Value).SingleOrDefault();
            decimal checkBalance = _cartService.GetCalculation(vm, username);
            if (checkBalance < 0)
            {
                return BadRequest();
            }
            else
            {
                _cartService.PurchaseAll(vm, username);
                return Ok();
            }

        }
    }
}
