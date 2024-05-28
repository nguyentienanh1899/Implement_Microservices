using Contracts.Domains;
using Infrastructure.Extensions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Shared.Enums.Inventory;

namespace Inventory.Grpc.Entities
{
    [BsonCollection("InventoryEntries")]
    public class InventoryEntry : MongoEntity
    {
        [BsonElement("documentType")]
        public DocumentType DocumentType { get; set; }
        [BsonElement("documentNo")]
        public string DocumentNo { get; set; }
        [BsonElement("itemNo")]
        public string ItemNo { get; set; }
        [BsonElement("quantity")]
        [BsonRepresentation(BsonType.Decimal128)]
        public int Quantity { get; set; }
        [BsonElement("externalDocumentNo")]
        public string ExternalDocumentNo { get; set; }
    }
}
