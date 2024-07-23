using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace api.Models;

public class PriceEntry
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; } = string.Empty;

    [BsonElement("price")]
    public double Price { get; set; } = 0;

    [BsonElement("time")]
    public DateTime Time { get; set; } = DateTime.Now;

    [BsonElement("coin_code")]
    public string CoinCode { get; set; } = string.Empty;
}