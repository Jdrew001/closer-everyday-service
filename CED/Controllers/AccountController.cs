﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CED.Models.Core;
using CED.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MimeKit;
using moment.net;
using Newtonsoft.Json;
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
            var message = new SendGridMessage();

            // message.SetTemplateId("d-d336182397714541874da8d70a480139");
            // message.AddTo("dtatkison@gmail.com", "Drew Atkison");
            // message.SetFrom("admin@atkisondevserver.me", "Closer Everyday Admin");
            // message.SetSubject("Testing Email");
            // //message.SetTemplateData({"test": "test"});


            // var apiKey = "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";
            // var client = new SendGridClient(apiKey);
            // var response = await client.SendEmailAsync(message);

            return Ok("");
        }

        [HttpGet("test")]
        public IActionResult actionResult()
        {
            var currentDate = DateTime.Parse("05/23/2022", System.Globalization.CultureInfo.InvariantCulture);
            var firstDayOfCurrentWeek = currentDate.FirstDateInWeek();
            
            return Ok(JsonConvert.SerializeObject(new Week() 
            {
                firstDay = firstDayOfCurrentWeek.AddDays(-7.0 * 4).ToShortDateString(),
                lastDay = firstDayOfCurrentWeek.AddDays((7.0 * 4) + 6).ToShortDateString()
            }));
        }
    }

    public class Week {
        public string firstDay;
        public string lastDay;
    }
}
