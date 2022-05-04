using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CED.Data.Interfaces;
using CED.Models.Core;
using CED.Models.Utils;
using CED.Services.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using MimeKit;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace CED.Services.Core
{
  public class EmailService : IEmailService
  {
    private readonly MailServerConfig  _mailServerConfig;
    private readonly SendGridConfig _sendGridConfig;
    private readonly ITemplateRepository _templateRepository;

    private IWebHostEnvironment _hostingEnv;

    public EmailService(MailServerConfig mailServerConfig, ITemplateRepository templateRepository, SendGridConfig sendGridConfig, IWebHostEnvironment hostingEnv)
    {
        _mailServerConfig = mailServerConfig;
        _templateRepository = templateRepository;
        _sendGridConfig = sendGridConfig;
        _hostingEnv = hostingEnv;
    }

    public async Task<string> GetTemplateByKey(string key)
    {
      return await _templateRepository.GetTemplateByKey(key);
    }

    public async Task SendEmailTemplate(string key, List<EmailAddress> to, string subject, object data = null)
    {
        string fromEmail = _sendGridConfig.Email;
        var templateId = await _templateRepository.GetTemplateByKey(key);
        if (templateId == null)
            return;

        var message = new SendGridMessage();

        message.SetTemplateId(templateId);
        message.AddTos(to);
        message.SetFrom(fromEmail, _sendGridConfig.Name);
        message.SetSubject(subject);

        if (data != null)
            message.SetTemplateData(data);

        var apiKey = _sendGridConfig.ApiToken;
        var client = new SendGridClient(apiKey);
        var response = await client.SendEmailAsync(message);
    }

    public async Task SendSimpleMail(List<MailboxAddress> to, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_mailServerConfig.FriendlyName, _mailServerConfig.SendEmail));

        if (_mailServerConfig.RouteAllEmail)
        {
            to.Clear();
            foreach (var address in _mailServerConfig.ForwardingAddresses)
                to.Add(new MailboxAddress(address, address));
        }

        foreach (var address in to)
            message.To.Add(address);

        message.Subject = subject;
        message.Body = new TextPart("html")
        {
            Text = body
        };
        try {
            using var client = new MailKit.Net.Smtp.SmtpClient();
            await client.ConnectAsync(_mailServerConfig.Host, _mailServerConfig.Port, false);
            await client.AuthenticateAsync(_mailServerConfig.Username, _mailServerConfig.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        } catch (Exception e) {
            Console.WriteLine(e);
        }
        
    }
  }
}