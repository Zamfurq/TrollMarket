using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrollMarket.Presentation.Web.Services;

namespace TrollMarket.Presentation.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("History")]
    public class HistoryController : Controller
    {

        private readonly TransactionService _transactionService;

        public HistoryController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public IActionResult Index(int page = 1, string sellerName = "", string buyerName = "")
        {
            int maxItem = 10;
            var vm = _transactionService.GetAllTransaction(page, maxItem, sellerName, buyerName);
            return View(vm);
        }
    }
}
