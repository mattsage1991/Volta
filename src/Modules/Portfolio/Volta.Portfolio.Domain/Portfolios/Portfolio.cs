using System;
using System.Collections.Generic;
using System.Linq;
using Volta.BuildingBlocks.Domain;
using Volta.Portfolios.Domain.Holding;

namespace Volta.Portfolios.Domain.Portfolios
{
    public class Portfolio : Entity, IAggregateRoot
    {
        public PortfolioId Id { get; private set; }

        private string _name;

        private List<PortfolioHolding> _holdings;

        private DateTime _createDate;

        private Portfolio()
        {
            _holdings = new List<PortfolioHolding>();
        }

        private Portfolio(string name)
        {
            _name = name;
            _createDate = DateTime.UtcNow;
        }

        public static Portfolio CreateNew(string name)
        {
            return new Portfolio(name);
        }

        public void AddHolding(HoldingId holdingId, MoneyValue averagePrice, int shareQuantity)
        {
            _holdings.Add(PortfolioHolding.Create(
                Id, 
                holdingId,
                shareQuantity,
                averagePrice
                ));
        }

        public void RemoveHolding(HoldingId holdingId)
        {
            var holding = _holdings.Single(x => x.HoldingId == holdingId);

            holding.Remove();
        }
    }
}
