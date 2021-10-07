using CED.Services.Core;
using CED.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CED.Services.Infrastructure
{
    public static class ServicesBindingsModule
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IDeviceService, DeviceService>();
            //services.AddScoped<IClientService, ClientService>();
            //services.AddScoped<IEmailService, EmailService>();
            //services.AddScoped<ICompanyService, CompanyService>();
            //services.AddScoped<IMaintenanceService, MaintenanceService>();
            //services.AddScoped<IRoleService, RoleService>();
            //services.AddScoped<IUserService, UserService>();
            //services.AddScoped<IEmailTemplateService, EmailTemplateService>();
            return services;
        }
    }
}
