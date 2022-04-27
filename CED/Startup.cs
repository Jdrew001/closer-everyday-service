using CED.Data.Infrastructure;
using CED.Middleware;
using CED.Models;
using CED.Models.Core;
using CED.Services.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using AutoMapper;
using CED.Profiles;
using Microsoft.AspNetCore.HttpOverrides;
using Serilog;
using CED.Models.Utils;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.FileProviders;
using System.IO;

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

            services.AddSingleton<MailServerConfig>(
                x => x.GetRequiredService<IOptions<MailServerConfig>>().Value);

            var connectionStrings = Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();
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

            services.AddCors();

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

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            if (env.IsEnvironment("Local"))
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CED v1"));
            } else
            {
                app.UseHsts();
            }

            app.UseExceptionHandlerMiddleware();

           // app.UseHttpsRedirection();

            app.UseRouting();
            // app.UseStaticFiles(new StaticFileOptions
            //     {
            //         FileProvider = new PhysicalFileProvider(
            //             Path.Combine(env.ContentRootPath, "Templates")),
            //         RequestPath = "/Templates"
            //     });
            
            Console.WriteLine("file env path", env.ContentRootPath);
            // global cors policy
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSerilogRequestLogging();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
