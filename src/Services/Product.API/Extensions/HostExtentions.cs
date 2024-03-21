using Microsoft.EntityFrameworkCore;

namespace Product.API.Extensions
{
    /// <summary>
    /// Method auto migration when start project
    /// </summary>
    public static class HostExtentions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host) where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var configuration = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();

                try
                {
                    logger.LogInformation("Migrating mysql database.");
                    ExcuteMigrations(context);
                    
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the mysql database");
                }

            }
            return host;
        }

        private static void ExcuteMigrations<TContext>(TContext context) where TContext : DbContext
        {
            context.Database.Migrate();
        }
    }
}
