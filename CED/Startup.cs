using AutoMapper;
using CED.Data.Infrastructure;
using CED.Middleware;
using CED.Models;
using CED.Models.Core;
using CED.Models.Utils;
using CED.Profiles;
using CED.Services.Infrastructure;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Reflection;
using System.Text;

namespace CED
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.Configure<ConnectionStrings>(
          options => Configuration.GetSection("ConnectionStrings").Bind(options));
      services.Configure<AppSettings>(options => Configuration.GetSection("AppSettings").Bind(options));
      services.Configure<JwtToken>(options => Configuration.GetSection("JwtToken").Bind(options));
      services.Configure<MailServerConfig>(options => Configuration.GetSection("MailServerConfig").Bind(options));
      services.Configure<SendGridConfig>(options => Configuration.GetSection("SendGridConfig").Bind(options));
      var connectionStrings = Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();

      services.AddFluentMigratorCore()
          .ConfigureRunner(c =>
          c.AddMySql4()
          .WithGlobalConnectionString(connectionStrings.CEDDB)
          .ScanIn(Assembly.Load("CED")).For.All())
          .AddLogging(config => config.AddFluentMigratorConsole());

      services.AddSingleton<MailServerConfig>(
          x => x.GetRequiredService<IOptions<MailServerConfig>>().Value);

      services.AddSingleton<SendGridConfig>(
          x => x.GetRequiredService<IOptions<SendGridConfig>>().Value);


      var appSettings = Configuration.GetSection("AppSettings").Get<AppSettings>();
      services.AddControllers();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "CED", Version = "v1" });
      });

      services.AddServices();
      services.AddRepositories();
      var mappingConfig = new MapperConfiguration(mc =>
      {
        mc.AddMaps("CED");
        mc.AddProfile(new HabitProfile());
        mc.AddProfile(new UserProfile());
        mc.AddProfile(new AuthCodeProfile());
      });
      IMapper mapper = mappingConfig.CreateMapper();
      services.AddSingleton(mapper);

      var allowedHost = Configuration.GetSection("AllowedHosts").Get<string>();

      services.AddAuthentication(option =>
      {
        option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
          .AddJwtBearer(options =>
          {
            options.RequireHttpsMetadata = true;
            options.SaveToken = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
              ValidIssuer = Configuration["JwtToken:Issuer"],
              ValidateIssuer = true,
              ValidAudience = Configuration["JwtToken:Audience"],
              ValidateAudience = true,
              ValidateLifetime = true,
              IssuerSigningKey =
                          new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtToken:SecretKey"])),
              ValidateIssuerSigningKey = true,
              ClockSkew = TimeSpan.Zero
            };
          });

      services.AddCors();
      services.AddControllers();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      // global cors policy
      app.UseCors(x => x
          .AllowAnyMethod()
          .AllowAnyHeader()
          .SetIsOriginAllowed(origin => true) // allow any origin
          .AllowCredentials()); // allow credentials

      if (env.IsEnvironment("Local"))
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CED v1"));
      }
      else
      {
        app.UseHsts();
      }


      app.UseExceptionHandlerMiddleware();
      app.UseRouting();
      app.UseAuthentication();

      app.UseAuthorization();

      app.UseSerilogRequestLogging();
      app.UseBlackListTokenMiddleware();

      // app.UseForwardedHeaders(new ForwardedHeadersOptions
      // {
      //     ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
      // });

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller}/{action=Index}/{id?}");
      });

      using var scope = app.ApplicationServices.CreateScope();
      var migrator = scope.ServiceProvider.GetService<IMigrationRunner>();
      migrator.MigrateUp();
    }
  }
}
