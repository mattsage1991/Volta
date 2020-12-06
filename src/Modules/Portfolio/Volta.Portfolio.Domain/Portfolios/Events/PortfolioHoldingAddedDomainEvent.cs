using System;
using Volta.BuildingBlocks.Domain;
using Volta.Portfolios.Domain.Stocks;

namespace Volta.Portfolios.Domain.Portfolios.Events
{
    public class PortfolioHoldingAddedDomainEvent : DomainEvent
    {
        public PortfolioHoldingAddedDomainEvent(
            PortfolioId portfolioId, 
            HoldingId holdingId,
            DateTime addedDate,
            decimal? sharePriceValue,
            string sharePriceCurrency,
            int quantity)
        {
            PortfolioId = portfolioId;
            HoldingId = holdingId;
            AddedDate = addedDate;
            SharePriceValue = sharePriceValue;
            SharePriceCurrency = sharePriceCurrency;
            Quantity = quantity;
        }

        public PortfolioId PortfolioId { get; set; }
        public HoldingId HoldingId { get; set; }
        public DateTime AddedDate { get; set; }
        public decimal? SharePriceValue { get; set; }
        public string SharePriceCurrency { get; set; }
        public int Quantity { get; set; }
    }
}