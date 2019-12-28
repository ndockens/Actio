using System.Threading.Tasks;
using Actio.Common.Commands;
using Actio.Services.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Actio.Services.Identity.Controllers
{
    [Route("")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] AuthenticateUser command)
        {
            var token = await _userService.LoginAsync(command.Email, command.Password);
            return Json(token);
        }
    }
}