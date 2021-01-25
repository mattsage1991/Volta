using System;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using Moq;
using Volta.Stocks.Domain.Stocks;
using Volta.Stocks.Domain.Stocks.Events;
using Volta.Stocks.Domain.Stocks.Rules;
using Volta.Stocks.Domain.Stocks.Services;
using Volta.Stocks.Domain.UnitTests.SeedWork;
using Xunit;

namespace Volta.Stocks.Domain.UnitTests.Stocks
{
    public class StockTests : TestBase
    {
        private readonly TestHarness harness;

        public StockTests()
        {
            harness = new TestHarness();
        }

        [Fact]
        public void CreateStock_WhenValidParameters_ShouldSucceedAndRaiseStockCreatedDomainEvent()
        {
            // Arrange
            var companyName = CompanyName.Of("valid");
            var tickerSymbol = TickerSymbol.Of("valid");
            var stockLookup = harness.StockLookup;
            var stockExistsChecker = harness.AlwaysFalseStockExistsChecker;

            // Act 
            var stock = Stock.Create(companyName, tickerSymbol, stockExistsChecker, stockLookup);

            // Assert
            using var _ = new AssertionScope();

            var domainEvent = AssertPublishedDomainEvent<StockCreatedDomainEvent>(stock);
            domainEvent.StockId.Should().Be(stock.Id);
            domainEvent.CompanyName.Should().Be(companyName);
            domainEvent.TickerSymbol.Should().Be(tickerSymbol);
        }

        [Fact]
        public void CreateStock_WhenStockAlreadyExists_ShouldSucceedAndRaiseStockCreatedDomainEvent()
        {
            // Arrange
            var companyName = CompanyName.Of("valid");
            var tickerSymbol = TickerSymbol.Of("valid");
            var stockLookup = harness.StockLookup;
            var stockExistsChecker = harness.AlwaysTrueStockExistsChecker;

            // Act 
            Action act = () => Stock.Create(companyName, tickerSymbol, stockExistsChecker, stockLookup);

            // Assert
            AssertBrokenRule<StockMustNotExistRule>(act);

        }

        [Fact]
        public void UpdateStock_WhenValuesHaveNotChanged_ShouldSucceedAndNotRaiseDomainEvents()
        {
            // Arrange
            var companyName = CompanyName.Of("valid");
            var tickerSymbol = TickerSymbol.Of("valid");
            var stockLookup = harness.StockLookup;
            var stockExistsChecker = harness.AlwaysFalseStockExistsChecker;

            var stock = Stock.Create(companyName, tickerSymbol, stockExistsChecker, stockLookup);
            stock.ClearDomainEvents();

            // Act
            stock.Update(stockLookup);

            // Assert
            DomainEventsTestHelper.GetAllDomainEvents(stock).Should().ContainSingle();
        }

        [Fact]
        public void UpdateStock_WhenValuesHaveChanged_ShouldSucceedAndRaiseDomainEvents()
        {
            // Arrange
            var companyName = CompanyName.Of("valid");
            var tickerSymbol = TickerSymbol.Of("valid");
            var stockLookup = harness.StockLookup;
            var stockExistsChecker = harness.AlwaysFalseStockExistsChecker;

            var stock = Stock.Create(companyName, tickerSymbol, stockExistsChecker, stockLookup);
            stock.ClearDomainEvents();

            var updatedStockData = LiveStockData.Of(
                MarketCap.Of(22222222),
                PeRatio.Of(5),
                PegRatio.Of(2),
                PriceToBookRatio.Of(2),
                ProfitMargin.Of(0.2m),
                TotalRevenue.Of(20000),
                DividendYield.Of(0.25m));

            var stockLookupMock = new Mock<IStockLookup>();
            stockLookupMock.Setup(x => x.GetLiveStockData(It.IsAny<TickerSymbol>())).Returns(Task.FromResult(updatedStockData));
            
            // Act
            stock.Update(stockLookupMock.Object);

            // Assert
            using var _ = new AssertionScope();

            var marketCapDomainEvent = AssertPublishedDomainEvent<StockMarketCapChangedDomainEvent>(stock);
            marketCapDomainEvent.MarketCap.Should().Be(updatedStockData.MarketCap);
            marketCapDomainEvent.StockId.Should().Be(stock.Id);

            var peRatioDomainEvent = AssertPublishedDomainEvent<StockPeRatioChangedDomainEvent>(stock);
            peRatioDomainEvent.PeRatio.Should().Be(updatedStockData.PeRatio);
            peRatioDomainEvent.StockId.Should().Be(stock.Id);

            var pegRatioDomainEvent = AssertPublishedDomainEvent<StockPegRatioChangedDomainEvent>(stock);
            pegRatioDomainEvent.PegRatio.Should().Be(updatedStockData.PegRatio);
            pegRatioDomainEvent.StockId.Should().Be(stock.Id);

            var priceToBookRatioDomainEvent = AssertPublishedDomainEvent<StockPriceToBookRatioChangedDomainEvent>(stock);
            priceToBookRatioDomainEvent.PriceToBookRatio.Should().Be(updatedStockData.PriceToBookRatio);
            priceToBookRatioDomainEvent.StockId.Should().Be(stock.Id);

            var profitMarginDomainEvent = AssertPublishedDomainEvent<StockProfitMarginChangedDomainEvent>(stock);
            profitMarginDomainEvent.ProfitMargin.Should().Be(updatedStockData.ProfitMargin);
            profitMarginDomainEvent.StockId.Should().Be(stock.Id);

            var totalRevenueDomainEvent = AssertPublishedDomainEvent<StockTotalRevenueChangedDomainEvent>(stock);
            totalRevenueDomainEvent.TotalRevenue.Should().Be(updatedStockData.TotalRevenue);
            totalRevenueDomainEvent.StockId.Should().Be(stock.Id);

            var dividendYieldDomainEvent = AssertPublishedDomainEvent<StockDividendYieldChangedDomainEvent>(stock);
            dividendYieldDomainEvent.DividendYield.Should().Be(updatedStockData.DividendYield);
            dividendYieldDomainEvent.StockId.Should().Be(stock.Id);

            var lastUpdatedDateDomainEvent = AssertPublishedDomainEvent<StockLastUpdatedDateChangedDomainEvent>(stock);
            lastUpdatedDateDomainEvent.LastUpdatedDate.Date.Should().Be(DateTime.UtcNow.Date);
            lastUpdatedDateDomainEvent.StockId.Should().Be(stock.Id);
        }

    }
}