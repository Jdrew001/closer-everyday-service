using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CED.Models.Core;
using MimeKit;
using SendGrid.Helpers.Mail;

namespace CED.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendSimpleMail(List<MailboxAddress> to, string subject, string body);
        Task SendEmailTemplate(string key, List<EmailAddress> to, string subject, object data = null);

        Task<string> GetTemplateByKey(string key);
    }
}