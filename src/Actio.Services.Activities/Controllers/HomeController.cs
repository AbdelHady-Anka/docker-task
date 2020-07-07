using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace Actio.Api.Controllers
{
    [Route("")]
    public class HomeController : ControllerBase
    {
        public HomeController()
        {
        }

        public IActionResult Get() => Content("Hello from actio API!");

    }
}