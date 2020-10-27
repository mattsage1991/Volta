using System;
using System.Collections.Generic;
using System.Linq;
using Volta.BuildingBlocks.Domain;
using Volta.Portfolios.Domain.Holding;
using Volta.Portfolios.Domain.Portfolios.Rules;

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

        public void AddHolding(StockId stockId, MoneyValue averagePrice, int shareQuantity)
        {
            CheckRule(new StockCannotBeAHoldingOfPortfolioMoreThanOnce(stockId, _holdings));

            _holdings.Add(PortfolioHolding.Create(
                Id, 
                stockId,
                DateTime.UtcNow,
                shareQuantity,
                averagePrice
                ));
        }

        public void RemoveHolding(StockId stockId)
        {
            CheckRule(new OnlyActiveHoldingCanBeRemovedFromPortfolioRule(stockId, _holdings));

            var holding = _holdings.Single(x => x.StockId == stockId);

            holding.Remove();
        }
    }
}
