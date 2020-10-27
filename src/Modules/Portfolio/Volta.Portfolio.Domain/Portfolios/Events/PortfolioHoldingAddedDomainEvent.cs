using System;
using Volta.BuildingBlocks.Domain;
using Volta.Portfolios.Domain.Holding;

namespace Volta.Portfolios.Domain.Portfolios.Events
{
    public class PortfolioHoldingAddedDomainEvent : DomainEventBase
    {
        public PortfolioHoldingAddedDomainEvent(
            PortfolioId portfolioId, 
            StockId stockId,
            DateTime addedDate,
            decimal? sharePriceValue,
            string sharePriceCurrency,
            int quantity)
        {
            PortfolioId = portfolioId;
            StockId = stockId;
            AddedDate = addedDate;
            SharePriceValue = sharePriceValue;
            SharePriceCurrency = sharePriceCurrency;
            Quantity = quantity;
        }

        public PortfolioId PortfolioId { get; set; }
        public StockId StockId { get; set; }
        public DateTime AddedDate { get; set; }
        public decimal? SharePriceValue { get; set; }
        public string SharePriceCurrency { get; set; }
        public int Quantity { get; set; }
    }
}