using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pholium.Application.Interfaces;

namespace Pholium.Template.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(this.userService.Get());
        }
    }
}
