using Inventory.Product.API.Entities.Abstraction;
using MongoDB.Bson.Serialization.Attributes;
using Shared.Enums.Inventory;

namespace Inventory.Product.API.Entities
{
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
        public string DocumentNo {  get; set; }
        [BsonElement("itemNo")]
        public string ItemNo {  get; set; }
        [BsonElement("quantity")]
        public int Quantity {  get; set; }
        [BsonElement("externalDocumentNo")]
        public string ExternalDocumentNo {  get; set; }
    }
}
