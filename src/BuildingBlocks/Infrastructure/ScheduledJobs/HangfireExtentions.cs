using Hangfire;
using Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Shared.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ScheduledJobs
{
    public static class HangfireExtentions
    {
        public static IServiceCollection AddHangfireServiceCustom(this IServiceCollection services)
        {
            var settings = services.GetOptions<HangFireSettings>("HangFireSettings");
            if(settings == null 
                || settings.Storage == null
                || string.IsNullOrEmpty(settings.Storage.ConnectionString)) 
            {
                throw new Exception("HangFireSettings is not configured properly!");
            }

            services.ConfigureHangfireServices(settings);
            services.AddHangfireServer(serverOptions =>
            {
                serverOptions.ServerName = settings.ServerName;
            });
            return services;
        }

        private static IServiceCollection ConfigureHangfireServices(this IServiceCollection services, HangFireSettings hangFireSetting)
        {
            if(string.IsNullOrEmpty(hangFireSetting.Storage.DBProvider))
            {
                throw new Exception("HangFire DBProvider is not configured.");
            }

            switch(hangFireSetting.Storage.DBProvider.ToLower())
            {
                case "mongodb":
                    break;
                case "postgresql":
                    break;
                case "mssql":
                    break;
                default:
                    throw new Exception($"HangFire Storage Provide {hangFireSetting.Storage.DBProvider} is not supported.");
            }

            return services;
        }
    }
}
