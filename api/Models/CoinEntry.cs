using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Models;

public class CoinEntry
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonElement("code")]
    public string Code { get; set; } = string.Empty;

    [BsonElement("rate")]
    public double rate { get; set; } = 0;

    [BsonElement("time")]
    public DateTime Time { get; set; } = DateTime.Now;

}