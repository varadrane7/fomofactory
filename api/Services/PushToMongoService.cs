namespace api.Services;

public class PushToMongoService : BackgroundService
{
    private readonly IHttpClientFactory _http;

    private readonly CryptoService _crypto;

    public PushToMongoService(IHttpClientFactory http, CryptoService crypto)
    {
        _http = http;
        _crypto = crypto;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await FetchDataAndStoreInDB();
            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }

    private async Task FetchDataAndStoreInDB()
    {

    }
}