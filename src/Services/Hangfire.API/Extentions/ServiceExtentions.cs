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
    }
}
