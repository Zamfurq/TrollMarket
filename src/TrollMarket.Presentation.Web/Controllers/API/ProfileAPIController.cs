using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrollMarket.Presentation.Web.Services;

namespace TrollMarket.Presentation.Web.Controllers.API
{
    [Authorize(Roles = "Buyer")]
    [Route("api/profile")]
    [ApiController]
    public class ProfileAPIController : ControllerBase
    {
        private readonly ProfileService _profileService;

        public ProfileAPIController(ProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpPatch("{username}")]
        public IActionResult AddBalance(decimal balance,string username)
        {
            if (balance <= 0 || balance == null)
            {
                return BadRequest();
            }
            _profileService.AddBalance(username, balance);
            return Ok();
        }
    }
}
