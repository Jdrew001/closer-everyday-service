using System.Collections.Generic;
using System.Threading.Tasks;
using CED.Models.Core;
using CED.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MimeKit;

using SendGrid;
using SendGrid.Helpers.Mail;

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
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("admin@atkisondevserver.me", "Example User");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("dtatkison@gmail.com", "Example User");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);

            return Ok(response);
        }

        [HttpGet("environment")]
        public IActionResult actionResult()
        {
            return Ok(_hostingEnv.EnvironmentName);
        }
    }
}
