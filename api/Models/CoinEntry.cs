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

    [BsonElement("delta")]
    public DeltaEntry Delta = new();

    [BsonElement("rank")]
    public Int32 Rank { get; set; }

    [BsonElement("time")]
    public DateTime Time { get; set; } = DateTime.Now;

}

public class DeltaEntry
{
    [BsonElement("hour")]
    public double Hour { get; set; } = 0;

    [BsonElement("day")]
    public double Day { get; set; } = 0;

    [BsonElement("week")]
    public double Week { get; set; } = 0;
}