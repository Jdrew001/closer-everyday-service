using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
namespace CED
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var baseDir = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            var configuration = new ConfigurationBuilder()
                .AddJsonFile($@"{baseDir}/appsettings.json")
                .Build();

            var levelSwitch = new LoggingLevelSwitch();

            Log.Logger = new LoggerConfiguration()
                .Enrich.WithProperty("Environment", configuration.GetValue(typeof(string), "AppSettings:Environment"))
                .Enrich.WithProperty("Application", configuration.GetValue(typeof(string), "AppSettings:Application"))
                .Enrich.WithMachineName()
                .MinimumLevel.ControlledBy(levelSwitch)
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
            try
            {
                Log.Information("Starting up");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
