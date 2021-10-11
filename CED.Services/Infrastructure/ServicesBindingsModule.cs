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
            services.AddScoped<IHabitService, HabitService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IReferenceService, ReferenceService>();
            return services;
        }
    }
}
