﻿using Inventory.Product.API.Persistence;
using MongoDB.Driver;
using Shared.Configurations;

namespace Inventory.Product.API.Extentions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var settings = services.GetService<MongoDbSettings>();
            if (settings == null || string.IsNullOrEmpty(settings.ConnectionString))
            {
                throw new ArgumentNullException("DatabaseSettings is not configured.");
            }
            var mongoClient = services.GetRequiredService<IMongoClient>();
            new InventoryDataSeed().SeedDataAsync(mongoClient, settings).Wait();
            return host;
        }
    }
}
