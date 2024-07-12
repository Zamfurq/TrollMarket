using Microsoft.AspNetCore.Mvc;
using TrollMarket.Presentation.Web.Services;

namespace TrollMarket.Presentation.Web.Controllers.API
{
    [Route("api")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly AuthService _service;

        public AuthAPIController(AuthService service)
        {
            _service = service;
        }

        [HttpGet("{username}")]
        public IActionResult Index(string username)
        {
            var vm = _service.GetUser(username);
            return Ok(vm);
        }
    }
}
