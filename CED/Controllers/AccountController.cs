using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CED.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private IHostingEnvironment _hostingEnv;
        private readonly ILogger<AccountController> _log;

        public AccountController(IHostingEnvironment hostingEnv, ILogger<AccountController> log)
        {
            _hostingEnv = hostingEnv;
            _log = log;
        }

        [HttpGet("environment")]
        public IActionResult actionResult()
        {
            return Ok("Hello World");
        }
    }
}
