using System.Threading.Tasks;
using Actio.Common.Commands;
using Actio.DomainServices.Services;
using Microsoft.AspNetCore.Mvc;

namespace Actio.Servies.Identity.Controllers
{
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            this._userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AuthenticateUser command)
            =>  Ok(await _userService.LoginAsync(command.Email, command.Password));
    }
}