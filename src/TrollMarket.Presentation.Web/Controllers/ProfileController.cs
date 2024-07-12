using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TrollMarket.Presentation.Web.Models;
using TrollMarket.Presentation.Web.Services;

namespace TrollMarket.Presentation.Web.Controllers
{
    [Authorize(Roles = "Seller,Buyer")]
    [Route("Profile")]
    public class ProfileController : Controller
    {
        private readonly ProfileService _services;

        public ProfileController(ProfileService services)
        {
            _services = services;
        }

        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            string username = User.Claims.Where(c => c.Type == "username").Select(c => c.Value).SingleOrDefault();
            string role = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault();
            ProfileViewModel vm = _services.GetProfile(username);
            vm.Role = role;
            int maxPage = 10;
            TransactionIndexViewModel transactionVm = _services.GetTransactionIndex(username, role, page, maxPage);
            return View(new ProfileDetailViewModel { Profile = vm, TransactionIndex = transactionVm});
        }
    }
}
