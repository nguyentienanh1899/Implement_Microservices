using Contracts.ScheduledJobs;
using Contracts.Services;
using Hangfire.API.Services;
using Hangfire.API.Services.Interfaces;
using Infrastructure.ScheduledJobs.Services;
using Infrastructure.Services;
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
            services.AddTransient<IScheduledJobService, HangfireServices>()
                    .AddTransient<IBackgroundJobService, BackgroundJobService>()
                    .AddScoped<ISmtpEmailService, SmtpEmailService>();
            
            return services;
        }
    }
}
