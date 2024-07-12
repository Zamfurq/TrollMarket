using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrollMarket.Presentation.Web.Models;
using TrollMarket.Presentation.Web.Services;

namespace TrollMarket.Presentation.Web.Controllers.API
{
    [Authorize (Roles ="Admin")]
    [ApiController]
    [Route("api/shipment")]
    public class ShipmentAPIController : ControllerBase
    {
        private readonly ShipmentService _service;

        public ShipmentAPIController(ShipmentService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var vm = _service.GetShipmentByID(id);
            return Ok(vm);
        }

        [HttpPost]
        public IActionResult Insert(ShipmentViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            _service.InsertShipment(vm);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id,ShipmentViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _service.UpdateShipment(id, vm);
            return Ok();
        }
    }
}
