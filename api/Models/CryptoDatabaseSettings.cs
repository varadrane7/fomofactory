namespace api.Models;

public class CryptoDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string CoinCollectionName { get; set; } = null!;

    public string PriceCollectionName { get; set; } = null!;
}