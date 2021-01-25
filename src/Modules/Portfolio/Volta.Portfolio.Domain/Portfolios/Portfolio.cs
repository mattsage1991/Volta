using System;
using System.Collections.Generic;
using System.Linq;
using Volta.BuildingBlocks.Domain.Entities;
using Volta.BuildingBlocks.Domain.Events;
using Volta.Portfolios.Domain.Portfolios.Events;
using Volta.Portfolios.Domain.Portfolios.Rules;
using Volta.Portfolios.Domain.Stocks;

namespace Volta.Portfolios.Domain.Portfolios
{
    public class Portfolio : Entity<PortfolioId>, IAggregateRoot
    {
        private MemberId memberId;
        private PortfolioName name;
        private DateTime createdDate;

        private List<Holding> holdings;

        private Portfolio()
        {
            holdings = new();
        }
        
        private Portfolio(MemberId memberId, PortfolioName name)
        {
            Id = new PortfolioId(Guid.NewGuid());
            this.memberId = memberId;
            this.name = name;
            this.createdDate = DateTime.UtcNow;

            AddDomainEvent(new PortfolioCreatedDomainEvent(this.Id, this.memberId, this.name, this.createdDate));
        }

        public static Portfolio Create(MemberId memberId, PortfolioName name)
        {
            return new Portfolio(memberId, name);
        }

        public HoldingId AddHolding(StockId stockId, AverageCost averageCost, NumberOfShares numberOfShares)
        {
            holdings ??= new List<Holding>();

            CheckRule(new StockCannotBeAHoldingOfPortfolioMoreThanOnce(stockId, holdings));

            var holding = Holding.Create(stockId, averageCost, numberOfShares);
            holdings.Add(holding);

            AddDomainEvent(new PortfolioHoldingAddedDomainEvent(this.Id, stockId, averageCost, numberOfShares));
            return holding.Id;
        }
        
        public void RemoveHolding(StockId stockId)
        {
            CheckRule(new OnlyActiveHoldingCanBeRemovedFromPortfolioRule(stockId, holdings));

            var holding = holdings.Single(x => x.StockId == stockId);

            holding.Remove();
        }
    }
}
