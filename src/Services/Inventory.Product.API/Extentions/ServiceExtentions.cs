﻿using Infrastructure.Extensions;
using Inventory.Product.API.Mapping;
using MongoDB.Driver;

namespace Inventory.Product.API.Extentions
{
    public static class ServiceExtentions
    {
        public static IServiceCollection AddConfigurationSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseSettings = configuration.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>();
            services.AddSingleton(databaseSettings);
            return services;
        }

        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => cfg.AddProfile(new MappingProfile()));
        }

        public static void ConfigureMongoDbClient(this IServiceCollection services)
        {
            services.AddSingleton<IMongoClient>(
                new MongoClient(GetMongoConnectionString(services)))
                .AddScoped(x => x.GetService<IMongoClient>()?.StartSession());
        }

        private static string GetMongoConnectionString(this IServiceCollection services)
        {
            var settings = services.GetOptions<DatabaseSettings>(nameof(DatabaseSettings));
            if(settings == null || string.IsNullOrEmpty(settings.ConnectionString)) {
                throw new ArgumentNullException("DatabaseSettings is not configured");
            }

            var databaseName = settings.DatabaseName;
            var mongoDbConnectionString = settings.ConnectionString + "/" + databaseName + "?authSource=admin";
            return mongoDbConnectionString;
        }
    }
}
