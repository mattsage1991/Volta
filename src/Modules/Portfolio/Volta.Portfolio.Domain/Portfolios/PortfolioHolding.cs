using System;
using Volta.BuildingBlocks.Domain;
using Volta.BuildingBlocks.Domain.Entities;
using Volta.Portfolios.Domain.Portfolios.Events;
using Volta.Portfolios.Domain.Stocks;

namespace Volta.Portfolios.Domain.Portfolios
{
    public class PortfolioHolding : Entity<PortfolioHoldingId>
    {
        internal PortfolioId PortfolioId { get; private set; }
        internal HoldingId HoldingId { get; private set; }
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
            HoldingId holdingId,
            DateTime addedDate,
            int quantity,
            MoneyValue averagePrice)
        {
            return new PortfolioHolding(portfolioId, holdingId, addedDate, quantity, averagePrice);
        }

        private PortfolioHolding(
            PortfolioId portfolioId,
            HoldingId holdingId,
            DateTime addedDate,
            int quantity,
            MoneyValue sharePrice)
        {
            PortfolioId = portfolioId;
            HoldingId = holdingId;
            _quantity = quantity;
            _sharePrice = sharePrice;
            _addedDate = addedDate;

            AddDomainEvent(new PortfolioHoldingAddedDomainEvent(
                PortfolioId,
                HoldingId,
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
                HoldingId,
                PortfolioId));
        }
    }
}