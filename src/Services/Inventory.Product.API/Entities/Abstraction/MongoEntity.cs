using MongoDB.Bson.Serialization.Attributes;

namespace Inventory.Product.API.Entities.Abstraction
{
    public abstract class MongoEntity
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [BsonElement("_id")]
        public virtual string Id { get; protected init; }

        [BsonElement("createdDate")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        [BsonElement("lastModifiedDate")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime? LastModifiedDate { get; set; } = DateTime.UtcNow;

    }
}
