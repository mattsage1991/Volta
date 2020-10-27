using System;
using Volta.BuildingBlocks.Domain;
using Volta.Portfolios.Domain.Holding;
using Volta.Portfolios.Domain.Portfolios.Events;

namespace Volta.Portfolios.Domain.Portfolios
{
    public class PortfolioHolding : Entity
    {
        internal PortfolioId PortfolioId { get; private set; }
        internal StockId StockId { get; private set; }
        private DateTime _addedDate;
        private MoneyValue _sharePrice;
        private int _quantity;
        private bool _isRemoved;
        private DateTime _removedDate;

        private PortfolioHolding()
        {
        }

        internal static PortfolioHolding Create(
            PortfolioId portfolioId,
            StockId stockId,
            DateTime addedDate,
            int quantity,
            MoneyValue averagePrice)
        {
            return new PortfolioHolding(portfolioId, stockId, addedDate, quantity, averagePrice);
        }

        private PortfolioHolding(
            PortfolioId portfolioId,
            StockId stockId,
            DateTime addedDate,
            int quantity,
            MoneyValue sharePrice)
        {
            PortfolioId = portfolioId;
            StockId = stockId;
            _quantity = quantity;
            _sharePrice = sharePrice;
            _addedDate = addedDate;

            AddDomainEvent(new PortfolioHoldingAddedDomainEvent(
                PortfolioId,
                StockId,
                addedDate,
                sharePrice.Value,
                sharePrice.Currency,
                quantity));
        }

        internal bool IsActive()
        {
            return !_isRemoved;
        }

        public void Remove()
        {
            _isRemoved = true;
            _removedDate = DateTime.UtcNow;

            AddDomainEvent(new PortfolioHoldingRemovedDomainEvent(
                StockId,
                PortfolioId));
        }

    }
}