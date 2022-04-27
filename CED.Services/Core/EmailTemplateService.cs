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
    public async Task<BodyBuilder> RegisterCode(string email, string code)
    {
      return await Task.Run(() =>
        {
            var baseDir = Directory.GetCurrentDirectory();
            var builder = new BodyBuilder();

            var pathToFile = $"{baseDir}/Templates/RegisterCode.html";

            using StreamReader sourceReader = new StreamReader(pathToFile);
            string mailText = sourceReader.ReadToEnd();
            mailText = mailText.Replace("[email]", email).Replace("[code]", code);
            builder.HtmlBody = mailText;
            return builder;
        });
    }

    public async Task<BodyBuilder> ResetCode(string email, string code)
    {
      return await Task.Run(() =>
        {
            var baseDir = Directory.GetCurrentDirectory();
            var builder = new BodyBuilder();

            var pathToFile = $"{baseDir}/Templates/ResetCode.html";

            using StreamReader sourceReader = new StreamReader(pathToFile);
            string mailText = sourceReader.ReadToEnd();
            mailText = mailText.Replace("[email]", email).Replace("[code]", code);
            builder.HtmlBody = mailText;
            return builder;
        });
    }

    public async Task<BodyBuilder> WelcomeRequest(string name)
    {
      return await Task.Run(() =>
        {
            var baseDir = Directory.GetCurrentDirectory();
            var builder = new BodyBuilder();

            var pathToFile = $"{baseDir}/Templates/WelcomeTemplate.html";

            using StreamReader sourceReader = new StreamReader(pathToFile);
            string mailText = sourceReader.ReadToEnd();
            mailText = mailText.Replace("[username]", name).Replace("[email]", "dtatkison@gmail.com");
            builder.HtmlBody = mailText;
            return builder;
        });
    }
  }
}