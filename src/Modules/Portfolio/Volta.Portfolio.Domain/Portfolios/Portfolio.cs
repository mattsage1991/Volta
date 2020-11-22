using System;
using System.Collections.Generic;
using System.Linq;
using Volta.BuildingBlocks.Domain;
using Volta.Portfolios.Domain.Portfolios.Rules;
using Volta.Portfolios.Domain.Stocks;

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
            _holdings = new List<PortfolioHolding>();
        }

        public static Portfolio CreateNew(string name)
        {
            return new Portfolio(name);
        }

        public void AddHolding(HoldingId holdingId, MoneyValue averagePrice, int shareQuantity)
        {
            CheckRule(new StockCannotBeAHoldingOfPortfolioMoreThanOnce(holdingId, _holdings));

            _holdings.Add(PortfolioHolding.Create(
                Id, 
                holdingId,
                DateTime.UtcNow,
                shareQuantity,
                averagePrice
                ));
        }

        public void RemoveHolding(HoldingId holdingId)
        {
            CheckRule(new OnlyActiveHoldingCanBeRemovedFromPortfolioRule(holdingId, _holdings));

            var holding = _holdings.Single(x => x.HoldingId == holdingId);

            holding.Remove();
        }
    }
}
