using System;
using Volta.BuildingBlocks.Domain;
using Volta.Portfolios.Domain.Holding;

namespace Volta.Portfolios.Domain.Portfolios
{
    public class PortfolioHolding : Entity
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
            int quantity,
            MoneyValue averagePrice)
        {
            return new PortfolioHolding(portfolioId, holdingId, quantity, averagePrice);
        }

        private PortfolioHolding(
            PortfolioId portfolioId,
            HoldingId holdingId,
            int quantity,
            MoneyValue sharePrice)
        {
            PortfolioId = portfolioId;
            HoldingId = holdingId;
            _quantity = quantity;
            _sharePrice = sharePrice;
            _addedDate = DateTime.UtcNow;
        }

        internal bool IsActive()
        {
            return !_isRemoved;
        }

        public void Remove()
        {
            _isRemoved = true;
            _removedDate = DateTime.UtcNow;
        }

    }
}