using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CED.Models.Core;
using MimeKit;

namespace CED.Services.Interfaces
{
    public interface IEmailTemplateService
    {
        Task<BodyBuilder> WelcomeRequest(string name);
        Task<BodyBuilder> RegisterCode(string email, string code);
    }
}