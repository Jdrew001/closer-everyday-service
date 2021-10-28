using CED.Data.Interfaces;
using CED.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CED.Data.Infrastructure
{
    public static class RepositoriesBindingModule
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddTransient<IDeviceRepository, DeviceRepository>();
            services.AddTransient<IHabitRepository, HabitRepository>();
            services.AddTransient<IFrequencyRepository, FrequencyRepository>();
            services.AddTransient<IReferenceRepository, ReferenceRepository>();
            services.AddTransient<IHabitStatRepository, HabitStatRepository>();
            services.AddTransient<IScheduleTypeRepository, ScheduleTypeRepository>();
            return services;
        }
    }
}
