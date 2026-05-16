using ServiceContracts;
using Services;
using Stocks;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddOptions<TradingOptions>().BindConfiguration("TradingOptions");
builder.Services.AddSingleton<IFinnhubService, FinnhubService>();
builder.Services.AddSingleton<IStocksService, StocksService>();
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
