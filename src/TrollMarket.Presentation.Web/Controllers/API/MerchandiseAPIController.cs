using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrollMarket.Presentation.Web.Models;
using TrollMarket.Presentation.Web.Services;

namespace TrollMarket.Presentation.Web.Controllers.API
{
    [Authorize(Roles = "Seller")]
    [Route("api/merchandise")]
    [ApiController]
    public class MerchandiseAPIController : ControllerBase
    {
        private readonly MerchandiseService _service;

        public MerchandiseAPIController(MerchandiseService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public IActionResult Info(int id)
        {
            MerchViewModel vm = _service.GetMerchById(id);
            return Ok(vm);
        }
    }
}
