using System.Diagnostics.Eventing.Reader;

namespace Product.API.Extensions
{
    public static class ConfigureHostExtensions
    {
        /// <summary>
        /// Get environment variables from appsetting.
        /// </summary>
        /// <param name="host"></param>
        public static void AddAppConfigurations(this ConfigureHostBuilder host)
        {
            host.ConfigureAppConfiguration((context, config) =>
            {
                var env = context.HostingEnvironment;
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();
            });
        }
    }
}
