using Infrastructure.Extensions;
using Inventory.Product.API.Mapping;
using Inventory.Product.API.Services;
using Inventory.Product.API.Services.Interfaces;
using MongoDB.Driver;
using Shared.Configurations;

namespace Inventory.Product.API.Extentions
{
    public static class ServiceExtentions
    {
        public static IServiceCollection AddConfigurationSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseSettings = configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
            services.AddSingleton(databaseSettings);
            return services;
        }

        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => cfg.AddProfile(new MappingProfile()));
            services.AddScoped<IInventoryServices, InventoryService>();
        }

        public static void ConfigureMongoDbClient(this IServiceCollection services)
        {
            services.AddSingleton<IMongoClient>(
                new MongoClient(GetMongoConnectionString(services)))
                .AddScoped(x => x.GetService<IMongoClient>()?.StartSession());
        }

        private static string GetMongoConnectionString(this IServiceCollection services)
        {
            var settings = services.GetOptions<MongoDbSettings>(nameof(MongoDbSettings));
            if(settings == null || string.IsNullOrEmpty(settings.ConnectionString)) {
                throw new ArgumentNullException("DatabaseSettings is not configured");
            }

            var databaseName = settings.DatabaseName;
            var mongoDbConnectionString = settings.ConnectionString + "/" + databaseName + "?authSource=admin";
            return mongoDbConnectionString;
        }
    }
}
