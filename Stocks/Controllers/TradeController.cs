using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using ServiceContracts.DTO;
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
                StockName = Convert.ToString(companyProfile?["currency"]),
                Price = ((JsonElement)stockPriceQuote["c"]).GetDouble(),
                Quantity = _tradingOptions.DefaultOrderQuantity
            };

            return View(stockTrade);
        }

        [Route("Trade/BuyOrder")]
        [HttpPost]
        public IActionResult BuyOrder(BuyOrderRequest buyOrderRequest)
        {
            buyOrderRequest.DateAndTimeOfOrder = DateTime.Now;
            ModelState.Clear();
            TryValidateModel(buyOrderRequest);
            if (!ModelState.IsValid)
            {
                return View("Index", buyOrderRequest);
            }
            BuyOrderResponse buyOrderResponse = _stocksService.CreateBuyOrder(buyOrderRequest);

            return RedirectToAction(nameof(Orders));
        }

        [Route("Trade/SellOrder")]
        [HttpPost]
        public IActionResult SellOrder(SellOrderRequest sellOrderRequest)
        {
            sellOrderRequest.DateAndTimeOfOrder = DateTime.Now;
            ModelState.Clear();
            TryValidateModel(sellOrderRequest);
            if (!ModelState.IsValid)
            {
                return View("Index", sellOrderRequest);
            }
            SellOrderResponse sellOrderResponse = _stocksService.CreateSellOrder(sellOrderRequest);
            return RedirectToAction(nameof(Orders));
        }
        [Route("Trade/Orders")]
        [HttpGet]
        public IActionResult Orders()
        {
            List<BuyOrderResponse> buyOrderResponses = _stocksService.GetBuyOrders();
            List<SellOrderResponse> sellOrderResponses = _stocksService.GetSellOrders();


            Orders orders = new()
            {
                BuyOrders = buyOrderResponses,
                SellOrders = sellOrderResponses
            };
            
            return View(orders);
        }
    }
}
