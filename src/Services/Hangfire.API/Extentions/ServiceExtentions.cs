using Contracts.ScheduledJobs;
using Infrastructure.ScheduledJobs.Services;
using Shared.Configurations;

namespace Hangfire.API.Extentions
{
    public static class ServiceExtentions
    {
        internal static IServiceCollection AddConfigurationSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var hangFireSettings = configuration.GetSection(nameof(HangFireSettings)).Get<HangFireSettings>();
            services.AddSingleton(hangFireSettings);
            return services;
        }

        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<IScheduledJobService, HangfireServices>();
            return services;
        }
    }
}
