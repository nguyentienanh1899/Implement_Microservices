﻿using Contracts.Domains;
using Infrastructure.Extensions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Shared.Enums.Inventory;

namespace Inventory.Product.API.Entities
{
    [BsonCollection("InventoryEntries")]
    public class InventoryEntry : MongoEntity
    {
        public InventoryEntry()
        {
            DocumentType = DocumentType.Purchase;
            DocumentNo = Guid.NewGuid().ToString();
            ExternalDocumentNo = Guid.NewGuid().ToString();
        }
        public InventoryEntry(string id) { id = Id; }

        [BsonElement("documentType")]
        public DocumentType DocumentType { get; set; }
        [BsonElement("documentNo")]
        public string DocumentNo { get; set; } = Guid.NewGuid().ToString();
        [BsonElement("itemNo")]
        public string ItemNo { get; set; }
        [BsonElement("quantity")]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Quantity { get; set; }
        [BsonElement("externalDocumentNo")]
        public string ExternalDocumentNo { get; set; } = Guid.NewGuid().ToString();
    }
}
