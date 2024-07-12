using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrollMarket.Presentation.Web.Services;

namespace TrollMarket.Presentation.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("Shipment")]
    public class ShipmentController : Controller
    {

        private readonly ShipmentService _service;

        public ShipmentController(ShipmentService service) { 
            _service = service;
        }

        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            int maxPage = 10;
            var vm = _service.GetShipments(page, maxPage);
            return View(vm);
        }


        [HttpGet("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var cart = _service.GetCart(id);
            var transaction = _service.GetTransactions(id);
            if(cart.Count != 0 || transaction.Count != 0) {
                return View("Error");
            }
            _service.DeleteShipment(id);
            return RedirectToAction("Index");
        }

        [HttpGet("StopService/{id}")]
        public IActionResult StopService(int id)
        {
            _service.StopShipment(id);
            return RedirectToAction("Index");
        }
    }
}
