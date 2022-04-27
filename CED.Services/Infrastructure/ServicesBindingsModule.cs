using CED.Services.Core;
using CED.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddScoped<IHabitStatService, HabitStatService>();
            services.AddScoped<IFrequencyService, FrequencyService>();
            services.AddScoped<IFriendService, FriendService>();
            services.AddScoped<IScheduleService, ScheduleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<IEmailService, EmailService>();
            services.AddSingleton<IEmailTemplateService, EmailTemplateService>();

            return services;
        }
    }
}
