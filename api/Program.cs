using api.Models;
using api.Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.Configure<CryptoDatabaseSettings>(
    builder.Configuration.GetSection("CryptoDatabase"));

// Add Mongo Service
builder.Services.AddSingleton<CryptoService>();
// Add HttpClient for LiveCoinWatch
builder.Services.AddHttpClient("livecoinwatch", client =>
{
    string baseUrl = config["LiveCoinWatch:URL"]!.ToString();
    client.BaseAddress = new Uri(baseUrl);

    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.DefaultRequestHeaders.Add("User-Agent", "FomoFactory-TA-api/1.0");
    client.DefaultRequestHeaders.Add("X-API-KEY", config["LiveCoinWatch:APIKey"]!.ToString());
});

builder.Services.AddHostedService<PushToMongoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.MapGet("/coins", (CryptoService crypto) =>
{
    return Results.Ok(crypto.GetCoinsList());
}).WithOpenApi();

app.MapGet("/coins/{coinCode}", async (CryptoService crypto, string coinCode) =>
{
    return Results.Ok(await crypto.GetRecentPrices(coinCode));
}).WithOpenApi();

app.Run();
