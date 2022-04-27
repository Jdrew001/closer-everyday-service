using System.Collections.Generic;
using System.Threading.Tasks;
using CED.Models.Core;
using CED.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace CED.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private IHostingEnvironment _hostingEnv;
        private readonly ILogger<AccountController> _log;
        private IEmailService _emailService;

        private IEmailTemplateService _emailTemplateService;
        public AccountController(
            IHostingEnvironment hostingEnv, 
            ILogger<AccountController> log, 
            IEmailService emailService,
            IEmailTemplateService emailTemplateService)
        {
            _hostingEnv = hostingEnv;
            _log = log;
            _emailService = emailService;
            _emailTemplateService = emailTemplateService;
        }

        [HttpGet("email")]
        public async Task<IActionResult> sendTestEmailAsync()
        {
            // var to = new List<MailboxAddress>() { new MailboxAddress("dtatkison@gmail.com", "dtatkison@gmail.com") };
            // // await this._emailService.SendSimpleMail(addresses, "Test email", "This is a test email");

            // var template = await _emailTemplateService.WelcomeRequest("Drew Atkison");
            // await _emailService.SendEmailTemplate(to, "Test Email Template", template.ToMessageBody());

            return Ok(_hostingEnv.EnvironmentName);
        }

        [HttpGet("environment")]
        public IActionResult actionResult()
        {
            return Ok(_hostingEnv.EnvironmentName);
        }
    }
}
