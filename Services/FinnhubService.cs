using Microsoft.Extensions.Configuration;
using ServiceContracts;
using System.Net.Http;
using System.Text.Json;

namespace Services
{
    public class FinnhubService : IFinnhubService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public FinnhubService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public Dictionary<string, object>? GetCompanyProfile(string stockSymbol)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();

            HttpRequestMessage httpRequestMessage = new()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri
                ($"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={_configuration["FinnhubToken"]}")
            };
            HttpResponseMessage httpResponseMessage = httpClient.Send(httpRequestMessage);

            string responseBody = new StreamReader(httpResponseMessage.Content.ReadAsStream()).ReadToEnd();

            Dictionary<string, object>? companyProfile = JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody);
            if (companyProfile == null)
                throw new InvalidOperationException("No response from server");

            if (companyProfile.ContainsKey("error"))
                throw new InvalidOperationException(Convert.ToString(companyProfile["error"]));
            return companyProfile;
        }

        public Dictionary<string, object>? GetStockPriceQuote(string stockSymbol)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();

            HttpRequestMessage httpRequestMessage = new()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri
                ($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={_configuration["FinnhubToken"]}")
            };
            HttpResponseMessage httpResponseMessage = httpClient.Send(httpRequestMessage);

            string responseBody = new StreamReader(httpResponseMessage.Content.ReadAsStream()).ReadToEnd();

            Dictionary<string, object>? stockPriceQuote = JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody);
            if (stockPriceQuote == null)
                throw new InvalidOperationException("No response from server");

            if (stockPriceQuote.ContainsKey("error"))
                throw new InvalidOperationException(Convert.ToString(stockPriceQuote["error"]));
            return stockPriceQuote;
        }
    }
}
