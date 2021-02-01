using System;
using System.Collections.Generic;
using System.Linq;
using Volta.BuildingBlocks.Domain.Entities;
using Volta.Portfolios.Domain.PortfolioOwners;
using Volta.Portfolios.Domain.Portfolios.Events;
using Volta.Portfolios.Domain.Portfolios.Rules;
using Volta.Portfolios.Domain.Stocks;

namespace Volta.Portfolios.Domain.Portfolios
{
    public class Portfolio : Entity<PortfolioId>, IAggregateRoot
    {
        public PortfolioOwnerId portfolioOwnerId;
        private PortfolioName name;
        private PortfolioCreatedDate portfolioCreatedDate;

        private List<Holding> holdings;

        private Portfolio()
        {
            holdings = new();
        }
        
        private Portfolio(PortfolioOwnerId portfolioOwnerId, PortfolioName name)
        {
            Id = new PortfolioId(Guid.NewGuid());
            this.portfolioOwnerId = portfolioOwnerId;
            this.name = name;
            this.portfolioCreatedDate = PortfolioCreatedDate.Of(DateTime.UtcNow);

            this.holdings = new List<Holding>();

            AddDomainEvent(new PortfolioCreatedDomainEvent(this.Id, this.portfolioOwnerId, this.name, this.portfolioCreatedDate));
        }

        public static Portfolio Create(PortfolioOwnerId memberId, PortfolioName name)
        {
            return new Portfolio(memberId, name);
        }

        public HoldingId AddHolding(StockId stockId, AverageCost averageCost, NumberOfShares numberOfShares)
        {
            CheckRule(new StockCannotBeAHoldingOfPortfolioMoreThanOnce(stockId, holdings));

            var holding = Holding.Create(stockId, averageCost, numberOfShares);
            holdings.Add(holding);

            AddDomainEvent(new HoldingAddedDomainEvent(this.Id, stockId, averageCost, numberOfShares));
            return holding.Id;
        }

        public void UpdateHolding(StockId stockId, AverageCost averageCost, NumberOfShares numberOfShares)
        {
            var holding = holdings.Single(x => x.StockId == stockId);

            holding.Update(averageCost, numberOfShares);
        }
        
        public void RemoveHolding(StockId stockId)
        {
            CheckRule(new OnlyActiveHoldingCanBeRemovedFromPortfolioRule(stockId, holdings));

            var holding = holdings.Single(x => x.StockId == stockId);

            holding.Remove();
        }
    }
}
