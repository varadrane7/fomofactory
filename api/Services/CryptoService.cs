using api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace api.Services;

public class CryptoService
{
    private readonly IMongoCollection<CoinEntry> _coinsCollection;

    private readonly IMongoCollection<PriceEntry> _priceCollection;

    public CryptoService(IOptions<CryptoDatabaseSettings> cryptoDatabaseSettings)
    {
        var mongoClient = new MongoClient(cryptoDatabaseSettings.Value.ConnectionString);
        var database = mongoClient.GetDatabase(cryptoDatabaseSettings.Value.DatabaseName);
        _coinsCollection = database.GetCollection<CoinEntry>(cryptoDatabaseSettings.Value.CoinCollectionName);
        _priceCollection = database.GetCollection<PriceEntry>(cryptoDatabaseSettings.Value.PriceCollectionName);
    }

    public async Task<List<CoinEntry>> GetCoinsList() =>
        await _coinsCollection.Find(_ => true).ToListAsync();

    public async Task<List<PriceEntry>> GetRecentPrices(string coinCode)
    {
        var filter = Builders<PriceEntry>.Filter.Eq(x => x.CoinCode, coinCode);
        var sort = Builders<PriceEntry>.Sort.Descending(x => x.Time);
        var recentPrices = await _priceCollection
            .Find(filter)
            .Sort(sort)
            .Limit(20)
            .ToListAsync();

        return recentPrices;
    }

    public async Task AddPrice(PriceEntry priceEntry) =>
        await _priceCollection.InsertOneAsync(priceEntry);

}