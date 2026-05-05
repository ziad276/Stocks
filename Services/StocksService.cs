
using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;

namespace Services
{
    public class StocksService : IStocksService
    {
        private readonly List<BuyOrder?> _buyOrders;
        private readonly List<SellOrder?> _sellOrders;

        public StocksService()
        {
            _buyOrders = new List<BuyOrder?>();
            _sellOrders = new List<SellOrder?>();
        }
        public BuyOrderResponse CreateBuyOrder(BuyOrderRequest? buyOrderRequest)
        {
            if (buyOrderRequest == null) throw new ArgumentNullException(nameof(buyOrderRequest));

            //Model Validation
            ValidationHelper.ModelValidation(buyOrderRequest);

            // Convert BuyOrderRequest to BuyOrder

            BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

            // generate BuyOrderID
            buyOrder.BuyOrderID = Guid.NewGuid();

            // Add to buy orders list
            _buyOrders.Add(buyOrder);

            return buyOrder.ToBuyOrderResponse();
        }

        public SellOrderResponse CreateSellOrder(SellOrderRequest? sellOrderRequest)
        {
            if (sellOrderRequest == null) throw new ArgumentNullException(nameof(sellOrderRequest));

            //Model Validation
            ValidationHelper.ModelValidation(sellOrderRequest);

            // Convert SellOrderRequest to SellOrder
            SellOrder sellOrder = sellOrderRequest.ToSellOrder();

            // generate SellOrderID
            sellOrder.SellOrderID = Guid.NewGuid();

            // Add to sell orders list
            _sellOrders.Add(sellOrder);

            return sellOrder.ToSellOrderResponse();
        }

        public List<BuyOrderResponse> GetBuyOrders()
        {
            //Convert all BuyOrder objects into BuyOrderResponse objects
            return _buyOrders
             .OrderByDescending(temp => temp.DateAndTimeOfOrder)
             .Select(temp => temp.ToBuyOrderResponse()).ToList();
        }


        public List<SellOrderResponse> GetSellOrders()
        {
            //Convert all SellOrder objects into SellOrderResponse objects
            return _sellOrders
             .OrderByDescending(temp => temp.DateAndTimeOfOrder)
             .Select(temp => temp.ToSellOrderResponse()).ToList();
        }
    }
}
