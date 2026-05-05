using ServiceContracts;
using ServiceContracts.DTO;
using Services;

namespace StocksUnitx
{
    public class StocksTest
    {
        private readonly IStocksService _stocksService;
        public StocksTest()
        {
            _stocksService = new StocksService();
        }
        #region CreateBuyOrder

        [Fact]
        public void CreateBuyOrder_NullBuyOrder_ToBeArgumentNullException()
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = null;
            //Act
            Assert.Throws<ArgumentNullException>(() =>
            {
                _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        [Theory]
        [InlineData(0)]
        public void CreateBuyOrder_QuantityIsLessThanMinimum_ToBeArgumentException(uint buyOrderQuantity)
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", Price = 1, Quantity = buyOrderQuantity };
            //Act
            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        [Theory]
        [InlineData(100001)]
        public void CreateBuyOrder_QuantityIsGreaterThanMaximum_ToBeArgumentException(uint buyOrderQuantity)
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", Price = 1, Quantity = buyOrderQuantity };
            //Act
            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        [Theory]
        [InlineData(0)]
        public void CreateBuyOrder_PriceIsLessThanMinimum_ToBeArgumentException(double buyOrderPrice)
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", Price = buyOrderPrice, Quantity = 1 };
            //Act
            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        [Theory]
        [InlineData(10001)]
        public void CreateBuyOrder_PriceIsGreaterThanMaximum_ToBeArgumentException(double buyOrderPrice)
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", Price = buyOrderPrice, Quantity = 1 };
            //Act
            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        [Fact]
        public void CreateBuyOrder_StockSymbolIsNull_ToBeArgumentException()
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest() { StockSymbol = null, Price = 1, Quantity = 1 };
            //Act
            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        [Fact]
        public void CreateBuyOrder_DateOfOrderIsLessThanYear2000_ToBeArgumentException()
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", DateAndTimeOfOrder = Convert.ToDateTime("1999-12-31"), Price = 1, Quantity = 1 };
            //Act
            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateBuyOrder(buyOrderRequest);
            });
        }

        [Fact]
        public void CreateBuyOrder_ValidData_ToBeSuccessful()
        {
            //Arrange
            BuyOrderRequest? buyOrderRequest = new BuyOrderRequest() { StockSymbol = "MSFT", StockName = "Microsoft", DateAndTimeOfOrder = Convert.ToDateTime("2024-12-31"), Price = 1, Quantity = 1 };
            //Act
            BuyOrderResponse buyOrderResponseFromCreate = _stocksService.CreateBuyOrder(buyOrderRequest);
            //Assert
            Assert.NotEqual(Guid.Empty, buyOrderResponseFromCreate.BuyOrderID);
        }

        #endregion

        #region CreateSellOrder
        // 1. Null request
        [Fact]
        public void CreateSellOrder_NullSellOrderRequest_ToBeArgumentNullException()
        {
            // Arrange
            SellOrderRequest? sellOrderRequest = null;

            // Act
            Assert.Throws<ArgumentNullException>(() =>
            {
                _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        // 2. Quantity = 0 (below minimum)
        [Theory]
        [InlineData(0)]
        public void CreateSellOrder_QuantityIsLessThanMinimum_ToBeArgumentException(uint sellOrderQuantity)
        {
            // Arrange
            SellOrderRequest? sellOrderRequest = new SellOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                Price = 1,
                Quantity = sellOrderQuantity,
                DateAndTimeOfOrder = DateTime.Parse("2000-01-01")
            };

            // Act
            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        // 3. Quantity = 100001 (above maximum)
        [Theory]
        [InlineData(100001)]
        public void CreateSellOrder_QuantityIsGreaterThanMaximum_ToBeArgumentException(uint sellOrderQuantity)
        {
            // Arrange
            SellOrderRequest? sellOrderRequest = new SellOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                Price = 1,
                Quantity = sellOrderQuantity,
                DateAndTimeOfOrder = DateTime.Parse("2000-01-01")
            };

            // Act
            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        // 4. Price = 0 (below minimum)
        [Theory]
        [InlineData(0)]
        public void CreateSellOrder_PriceIsLessThanMinimum_ToBeArgumentException(uint sellOrderPrice)
        {
            // Arrange
            SellOrderRequest? sellOrderRequest = new SellOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                Price = sellOrderPrice,
                Quantity = 1,
                DateAndTimeOfOrder = DateTime.Parse("2000-01-01")
            };

            // Act
            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        // 5. Price = 10001 (above maximum)
        [Theory]
        [InlineData(10001)]
        public void CreateSellOrder_PriceIsGreaterThanMaximum_ToBeArgumentException(uint sellOrderPrice)
        {
            // Arrange
            SellOrderRequest? sellOrderRequest = new SellOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                Price = sellOrderPrice,
                Quantity = 1,
                DateAndTimeOfOrder = DateTime.Parse("2000-01-01")
            };

            // Act
            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        // 6. StockSymbol = null
        [Fact]
        public void CreateSellOrder_StockSymbolIsNull_ToBeArgumentException()
        {
            // Arrange
            SellOrderRequest? sellOrderRequest = new SellOrderRequest()
            {
                StockSymbol = null,
                StockName = "Microsoft",
                Price = 1,
                Quantity = 1,
                DateAndTimeOfOrder = DateTime.Parse("2000-01-01")
            };

            // Act
            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        // 7. DateAndTimeOfOrder = 1999-12-31 (before minimum date)
        [Theory]
        [InlineData("1999-12-31")]
        public void CreateSellOrder_DateIsOlderThanMinimum_ToBeArgumentException(string dateAndTimeOfOrder)
        {
            // Arrange
            SellOrderRequest? sellOrderRequest = new SellOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                Price = 1,
                Quantity = 1,
                DateAndTimeOfOrder = DateTime.Parse(dateAndTimeOfOrder)
            };

            // Act
            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateSellOrder(sellOrderRequest);
            });
        }

        // 8. All valid values
        [Fact]
        public void CreateSellOrder_ValidSellOrderRequest_ToBeSuccessful()
        {
            // Arrange
            SellOrderRequest? sellOrderRequest = new SellOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                Price = 1,
                Quantity = 1,
                DateAndTimeOfOrder = DateTime.Parse("2000-01-01")
            };

            // Act
            SellOrderResponse sellOrderResponse = _stocksService.CreateSellOrder(sellOrderRequest);

            // Assert
            Assert.NotNull(sellOrderResponse);
            Assert.NotEqual(Guid.Empty, sellOrderResponse.SellOrderID);
        }
        #endregion

        #region GetAllBuyOrders
        // ==================== GetAllBuyOrders ====================

        // 1. Default empty list
        [Fact]
        public void GetAllBuyOrders_DefaultList_ToBeEmpty()
        {
            // Act
            List<BuyOrderResponse> buyOrderResponses = _stocksService.GetBuyOrders();

            // Assert
            Assert.Empty(buyOrderResponses);
        }

        // 2. After adding buy orders, list should contain all of them
        [Fact]
        public void GetAllBuyOrders_AfterAddingBuyOrders_ToContainAllAddedOrders()
        {
            // Arrange
            BuyOrderRequest buyOrderRequest1 = new BuyOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                Price = 1,
                Quantity = 1,
                DateAndTimeOfOrder = DateTime.Parse("2000-01-01")
            };

            BuyOrderRequest buyOrderRequest2 = new BuyOrderRequest()
            {
                StockSymbol = "GOOGL",
                StockName = "Google",
                Price = 2,
                Quantity = 2,
                DateAndTimeOfOrder = DateTime.Parse("2000-01-01")
            };

            // Act
            BuyOrderResponse buyOrderResponse1 = _stocksService.CreateBuyOrder(buyOrderRequest1);
            BuyOrderResponse buyOrderResponse2 = _stocksService.CreateBuyOrder(buyOrderRequest2);

            List<BuyOrderResponse> allBuyOrders = _stocksService.GetBuyOrders();

            // Assert
            Assert.NotEmpty(allBuyOrders);
            Assert.Contains(buyOrderResponse1, allBuyOrders);
            Assert.Contains(buyOrderResponse2, allBuyOrders);
        }
        #endregion

        #region GetAllSellOrders

        // ==================== GetAllSellOrders ====================

        // 1. Default empty list
        [Fact]
        public void GetAllSellOrders_DefaultList_ToBeEmpty()
        {
            // Act
            List<SellOrderResponse> sellOrderResponses = _stocksService.GetSellOrders();

            // Assert
            Assert.Empty(sellOrderResponses);
        }

        // 2. After adding sell orders, list should contain all of them
        [Fact]
        public void GetAllSellOrders_AfterAddingSellOrders_ToContainAllAddedOrders()
        {
            // Arrange
            SellOrderRequest sellOrderRequest1 = new SellOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                Price = 1,
                Quantity = 1,
                DateAndTimeOfOrder = DateTime.Parse("2000-01-01")
            };

            SellOrderRequest sellOrderRequest2 = new SellOrderRequest()
            {
                StockSymbol = "GOOGL",
                StockName = "Google",
                Price = 2,
                Quantity = 2,
                DateAndTimeOfOrder = DateTime.Parse("2000-01-01")
            };

            // Act
            SellOrderResponse sellOrderResponse1 = _stocksService.CreateSellOrder(sellOrderRequest1);
            SellOrderResponse sellOrderResponse2 = _stocksService.CreateSellOrder(sellOrderRequest2);

            List<SellOrderResponse> allSellOrders = _stocksService.GetSellOrders();

            // Assert
            Assert.NotEmpty(allSellOrders);
            Assert.Contains(sellOrderResponse1, allSellOrders);
            Assert.Contains(sellOrderResponse2, allSellOrders);
        } 
        #endregion

    }
}