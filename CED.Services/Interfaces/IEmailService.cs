using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CED.Models.Core;
using MimeKit;

namespace CED.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendSimpleMail(List<MailboxAddress> to, string subject, string body);
        Task SendEmailTemplate(List<MailboxAddress> to, string subject, MimeEntity body);
    }
}