using System;
using System.Collections.Generic;
using System.Linq;
using Volta.BuildingBlocks.Domain;
using Volta.BuildingBlocks.Domain.Entities;
using Volta.Portfolios.Domain.Members;
using Volta.Portfolios.Domain.Portfolios.Rules;
using Volta.Portfolios.Domain.Stocks;

namespace Volta.Portfolios.Domain.Portfolios
{
    public class Portfolio : Entity
    {
        public PortfolioId Id { get; }

        public MemberId MemberId { get; }
        
        public Portfolio(PortfolioId id, MemberId memberId, string name)
        {
            Id = id;
            MemberId = memberId;
            _name = name;
            _createDate = DateTime.UtcNow;
            _holdings = new List<PortfolioHolding>();
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

        private string _name;

        private List<PortfolioHolding> _holdings;

        private DateTime _createDate;
    }
}
