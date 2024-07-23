using api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace api.Services;

public class CryptoService
{
    private readonly IMongoCollection<CoinEntry> _coinsCollection;

    public CryptoService(IOptions<CryptoDatabaseSettings> cryptoDatabaseSettings)
    {
        var mongoClient = new MongoClient(cryptoDatabaseSettings.Value.ConnectionString);
        var database = mongoClient.GetDatabase(cryptoDatabaseSettings.Value.DatabaseName);
        _coinsCollection = database.GetCollection<CoinEntry>(cryptoDatabaseSettings.Value.CoinCollectionName);
    }

    public List<string> GetCoinsList() =>
        new List<string> { "BTC", "ETH", "USDT", "SOL", "DOGE" };

    public async Task<List<CoinEntry>> GetRecentPrices(string coinCode)
    {
        var filter = Builders<CoinEntry>.Filter.Eq(x => x.Code, coinCode);
        var sort = Builders<CoinEntry>.Sort.Descending(x => x.Time);
        var recentPrices = await _coinsCollection
            .Find(filter)
            .Sort(sort)
            .Limit(20)
            .ToListAsync();

        return recentPrices;
    }

    public async Task AddPrices(List<CoinEntry> coinEntries) =>
        await _coinsCollection.InsertManyAsync(coinEntries);

    public async Task DeleteOldEntries()
    {
        var filter = Builders<CoinEntry>.Filter.Lte(x => x.Time, DateTime.Now.AddMinutes(-10));
        await _coinsCollection.DeleteManyAsync(filter);
    }

}