using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CED.Models.Core;
using CED.Services.Interfaces;
using MimeKit;

namespace CED.Services.Core
{
  public class EmailTemplateService : IEmailTemplateService
  {
    public async Task<BodyBuilder> WelcomeRequest(string name)
    {
       return await Task.Run(() =>
        {
            var baseDir = Directory.GetCurrentDirectory();
            var builder = new BodyBuilder();

            var pathToFile = $"{baseDir}\\Templates\\WelcomeTemplate.html";

            using StreamReader sourceReader = new StreamReader(pathToFile);
            string mailText = sourceReader.ReadToEnd();
            mailText = mailText.Replace("[username]", name).Replace("[email]", "dtatkison@gmail.com");
            builder.HtmlBody = mailText;
            return builder;
        });
    }
  }
}