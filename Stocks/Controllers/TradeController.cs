using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using Stocks.Models;
using System.Text.Json;

namespace Stocks.Controllers
{
    public class TradeController : Controller
    {
        private readonly IFinnhubService _finnhubService;
        private readonly TradingOptions _tradingOptions;
        private readonly IStocksService _stocksService;

        public TradeController(IFinnhubService finnhubService, IOptions<TradingOptions> tradingOptions, IStocksService stocksService)
        {
            _finnhubService = finnhubService;
            _tradingOptions = tradingOptions.Value;
            _stocksService = stocksService;
        }

        [Route("/")]
        public IActionResult Index()
        {
            Dictionary<string, object>? companyProfile = _finnhubService.GetCompanyProfile(_tradingOptions.DefaultStockSymbol ?? "AAPL");
            Dictionary<string, object>? stockPriceQuote = _finnhubService.GetStockPriceQuote(_tradingOptions.DefaultStockSymbol ?? "AAPL");

            StockTrade stockTrade = new()
            {
                StockSymbol = Convert.ToString(companyProfile?["ticker"]),
                StockName = Convert.ToString(companyProfile?["name"]),
                Price = ((JsonElement)stockPriceQuote["c"]).GetDouble(),
                Quantity = 10000
            };

            return View(stockTrade);
        }
    }
}
