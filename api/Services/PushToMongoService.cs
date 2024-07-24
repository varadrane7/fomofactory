using api.Models;

namespace api.Services;

public class PushToMongoService : BackgroundService
{
    private readonly IHttpClientFactory _httpClientFactory;

    private readonly CryptoService _crypto;

    public PushToMongoService(IHttpClientFactory httpClientFactory, CryptoService crypto)
    {
        _httpClientFactory = httpClientFactory;
        _crypto = crypto;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await FetchDataAndStoreInDB();
            await DeleteOldEntries();
            await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
        }
    }

    private async Task FetchDataAndStoreInDB()
    {
        var client = _httpClientFactory.CreateClient("livecoinwatch");
        var content = new
        {
            codes = new string[] { "BTC", "ETH", "USDT", "SOL", "DOGE" },
            sort = "rank",
            order = "ascending",
            offset = 0,
            limit = 0,
            meta = false
        };
        var response = await client.PostAsJsonAsync("/coins/map", content);
        var responseList = await response.Content.ReadFromJsonAsync<List<CoinEntry>>();
        if (responseList != null)
            await _crypto.AddPrices(responseList);
    }

    private async Task DeleteOldEntries() =>
        await _crypto.DeleteOldEntries();
}