namespace Ordering.API.Extensions
{
    public static class HostExtensions
    {
        public static void AddAppConfigurations(this ConfigureHostBuilder host)
        {
            host.ConfigureAppConfiguration((context, config) =>
            {
                var evn = context.HostingEnvironment;
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{evn.EnvironmentName}.json", optional: true, reloadOnChange: true)
                        .AddEnvironmentVariables();
            });
        }
    }
}
