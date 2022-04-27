using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CED.Models.Core;
using CED.Models.Utils;
using CED.Services.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;

namespace CED.Services.Core
{
  public class EmailService : IEmailService
  {
    private readonly MailServerConfig  _mailServerConfig;

    public EmailService(MailServerConfig mailServerConfig)
    {
        _mailServerConfig = mailServerConfig;
    }

    public async Task SendEmailTemplate(List<MailboxAddress> to, string subject, MimeEntity body)
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
        message.Body = body;

        using var client = new MailKit.Net.Smtp.SmtpClient();
        await client.ConnectAsync(_mailServerConfig.Host, _mailServerConfig.Port, false);
        await client.AuthenticateAsync(_mailServerConfig.Username, _mailServerConfig.Password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
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