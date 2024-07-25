﻿using Common.Logging;
using Infrastructure.Extensions;
using Serilog;
using Shared.Configurations;

namespace Hangfire.API.Extentions
{
    public static class HostExtentions
    {
        public static void AddAppConfigurations(this ConfigureHostBuilder host)
        {
            host.ConfigureAppConfiguration((context, config) =>
            {
                var env = context.HostingEnvironment;
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();
            }).UseSerilog(Serilogger.Configure);
        }

        internal static IApplicationBuilder UseHangfireDashboard(this IApplicationBuilder app, IConfiguration configuration)
        {
            var configDashboard = configuration.GetSection("HangFireSettings:Dashboard").Get<DashboardOptions>();
            var hangfireSettings = configuration.GetSection("HangFireSettings").Get<HangFireSettings>();
            var hangfireRoute = hangfireSettings?.Route;

            app.UseHangfireDashboard(hangfireRoute, new DashboardOptions
            {
                Authorization = new[] { new AuthorizationFilter() }, // Temporarily authorizes the configuration so that the console can be used on every other local field. Because the console automatically opens only for each local server field, other environments are required to have permissions
                DashboardTitle = configDashboard.DashboardTitle,
                StatsPollingInterval = configDashboard.StatsPollingInterval,
                AppPath = configDashboard.AppPath,
                IgnoreAntiforgeryToken = true,
            });

            return app;
        }
    }
}
