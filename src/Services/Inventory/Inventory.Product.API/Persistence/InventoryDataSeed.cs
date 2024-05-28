using Inventory.Product.API.Entities;
using Inventory.Product.API.Extentions;
using MongoDB.Driver;
using Shared.Configurations;

namespace Inventory.Product.API.Persistence
{
    public class InventoryDataSeed
    {
        public async Task SeedDataAsync(IMongoClient mongoClient, MongoDbSettings databaseSettings)
        {
            var databaseName = databaseSettings.DatabaseName;
            var database = mongoClient.GetDatabase(databaseName);
            var inventoryCollection = database.GetCollection<InventoryEntry>("InventoryEntries");
            if(await inventoryCollection.EstimatedDocumentCountAsync() == 0)
            {
                await inventoryCollection.InsertManyAsync(GetPreconfiguredInventoryEntries());
            }
        }

        private IEnumerable<InventoryEntry> GetPreconfiguredInventoryEntries()
        {
            return new List<InventoryEntry>
            {
                new()
                {
                    Quantity = 6,
                    DocumentNo = Guid.NewGuid().ToString(),
                    ItemNo = "Iphone",
                    ExternalDocumentNo = Guid.NewGuid().ToString(),
                    DocumentType = Shared.Enums.Inventory.DocumentType.Purchase
                },
                new()
                {
                    Quantity = 8,
                    DocumentNo = Guid.NewGuid().ToString(),
                    ItemNo = "Iphone2",
                    ExternalDocumentNo = Guid.NewGuid().ToString(),
                    DocumentType = Shared.Enums.Inventory.DocumentType.Purchase
                }
            };
        }
    }
}
