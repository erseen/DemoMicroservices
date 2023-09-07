using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ThirdParty.BouncyCastle.Utilities.IO.Pem;

namespace OrderApi.Models
{
    [Serializable, BsonIgnoreExtraElements]
    public class OrderedDetail
    {
        [BsonId, BsonElement("product_id"), BsonRepresentation(BsonType.Int32)]
        public int ProductId { get; set; }
        [ BsonElement("quantity"), BsonRepresentation(BsonType.Decimal128)]
        public decimal Quantity { get; set; }
        [BsonElement("unit_price"), BsonRepresentation(BsonType.Decimal128)]
        public decimal UnitPrice { get; set; }

    }
}
