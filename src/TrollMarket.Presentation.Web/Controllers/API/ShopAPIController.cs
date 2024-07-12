using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrollMarket.Presentation.Web.Models;
using TrollMarket.Presentation.Web.Services;

namespace TrollMarket.Presentation.Web.Controllers.API
{
    //[Authorize(Roles = "Buyer")]
    [Route("api/shop")]
    [ApiController]
    public class ShopAPIController : ControllerBase
    {
        private readonly MerchandiseService _service;
        private readonly ShopService _shopService;

        public ShopAPIController(MerchandiseService service, ShopService shopService)
        {
            _service = service;
            _shopService = shopService;
        }

        [HttpGet("{id}")]
        public IActionResult Index(int id)
        {
            MerchViewModel vm = _service.GetMerchById(id);
            return Ok(vm);
        }

        [HttpPost]
        public IActionResult AddCart(CartViewModel cart)
        {
            cart.BuyerName = User.Claims.Where(c => c.Type == "username").Select(c => c.Value).SingleOrDefault();
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _shopService.AddtoCart(cart);
            return Ok();
        }
    }
}
