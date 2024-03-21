using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Product.API.Persistence;

namespace Product.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.Configure<RouteOptions>(options => { options.LowercaseQueryStrings = true; });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // add configuration for product context (MySql) 
            services.ConfigurationProductDbContext(configuration);
            return services;
        }

        private static IServiceCollection ConfigurationProductDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            // get connectionString from appsetting 
            var connectionString = configuration.GetConnectionString("DefaultConnectionString");
            var builder = new MySqlConnectionStringBuilder(connectionString);

            services.AddDbContext<ProductContext>(c => c.UseMySql(builder.ConnectionString, ServerVersion.AutoDetect(builder.ConnectionString), e =>
            {
                e.MigrationsAssembly("Product.API");
                e.SchemaBehavior(MySqlSchemaBehavior.Ignore);
            }));
            return services;
        }
    }
}
