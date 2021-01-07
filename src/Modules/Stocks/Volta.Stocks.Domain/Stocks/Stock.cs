using System;
using System.Threading.Tasks;
using Volta.BuildingBlocks.Domain.Entities;
using Volta.Stocks.Domain.Stocks.Events;
using Volta.Stocks.Domain.Stocks.Services;

namespace Volta.Stocks.Domain.Stocks
{
    public class Stock : Entity<StockId>, IAggregateRoot
    {
        private CompanyName companyName;
        private TickerSymbol tickerSymbol;
        private MarketCap marketCap;
        private PeRatio peRatio;
        private PegRatio pegRatio;
        private PriceToBookRatio priceToBookRatio;
        private ProfitMargin profitMargin;
        private TotalRevenue totalRevenue;
        private DividendYield dividendYield;

        private Stock() { }

        private Stock(CompanyName companyName, TickerSymbol tickerSymbol, MarketCap marketCap, PeRatio peRatio, PegRatio pegRatio, PriceToBookRatio priceToBookRatio,
            ProfitMargin profitMargin, TotalRevenue totalRevenue, DividendYield dividendYield)
        {
            Id = new StockId(Guid.NewGuid());
            this.companyName = companyName;
            this.tickerSymbol = tickerSymbol;
            this.marketCap = marketCap;
            this.peRatio = peRatio;
            this.pegRatio = pegRatio;
            this.priceToBookRatio = priceToBookRatio;
            this.profitMargin = profitMargin;
            this.totalRevenue = totalRevenue;
            this.dividendYield = dividendYield;

            AddDomainEvent(new StockCreatedDomainEvent(this.Id, this.companyName, this.tickerSymbol));
        }

        public static async Task<Stock> Create(CompanyName companyName, TickerSymbol tickerSymbol, IStockLookup stockLookup)
        {
            var keyStats = await stockLookup.GetKeyStats(tickerSymbol).ConfigureAwait(false);
            return new Stock(companyName, tickerSymbol, keyStats.MarketCap, keyStats.PeRatio, keyStats.PegRatio, 
                keyStats.PriceToBookRatio, keyStats.ProfitMargin, keyStats.TotalRevenue, keyStats.DividendYield);
        }

        public void Update(MarketCap marketCap, PeRatio peRatio, PegRatio pegRatio, PriceToBookRatio priceToBookRatio,
            ProfitMargin profitMargin, TotalRevenue totalRevenue, DividendYield dividendYield)
        {
            ChangeMarketCap(marketCap);
            ChangePeRatio(peRatio);
            ChangePegRatio(pegRatio);
            ChangePriceToBookRatio(priceToBookRatio);
            ChangeProfitMargin(profitMargin);
            ChangeTotalRevenue(totalRevenue);
            ChangeDividendYield(dividendYield);
        }

        private void ChangeMarketCap(MarketCap marketCap)
        {
            if (this.marketCap == marketCap) return;
            this.marketCap = marketCap;

            AddDomainEvent(new StockMarketCapChangedDomainEvent(Id, this.marketCap));
        }

        private void ChangePeRatio(PeRatio peRatio)
        {
            if (this.peRatio == peRatio) return;
            this.peRatio = peRatio;

            AddDomainEvent(new StockPeRatioChangedDomainEvent(Id, this.peRatio));
        }

        private void ChangePegRatio(PegRatio pegRatio)
        {
            if (this.pegRatio == pegRatio) return;
            this.pegRatio = pegRatio;

            AddDomainEvent(new StockPegRatioChangedDomainEvent(Id, this.pegRatio));
        }

        private void ChangePriceToBookRatio(PriceToBookRatio priceToBookRatio)
        {
            if (this.priceToBookRatio == priceToBookRatio) return;
            this.priceToBookRatio = priceToBookRatio;

            AddDomainEvent(new StockPriceToBookRatioChangedDomainEvent(Id, this.priceToBookRatio));
        }

        private void ChangeProfitMargin(ProfitMargin profitMargin)
        {
            if (this.profitMargin == profitMargin) return;
            this.profitMargin = profitMargin;

            AddDomainEvent(new StockProfitMarginChangedDomainEvent(Id, this.profitMargin));
        }

        private void ChangeTotalRevenue(TotalRevenue totalRevenue)
        {
            if (this.totalRevenue == totalRevenue) return;
            this.totalRevenue = totalRevenue;

            AddDomainEvent(new StockTotalRevenueChangedDomainEvent(Id, this.totalRevenue));
        }

        private void ChangeDividendYield(DividendYield dividendYield)
        {
            if (this.dividendYield == dividendYield) return;
            this.dividendYield = dividendYield;

            AddDomainEvent(new StockDividendYieldChangedDomainEvent(Id, this.dividendYield));
        }

    }
}