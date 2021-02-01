using System;
using Volta.BuildingBlocks.Domain;
using Volta.BuildingBlocks.Domain.Entities;
using Volta.Portfolios.Domain.Portfolios.Events;
using Volta.Portfolios.Domain.Stocks;

namespace Volta.Portfolios.Domain.Portfolios
{
    public class Holding : Entity<HoldingId>
    {
        internal StockId StockId { get; }
        internal DateTime AddedDate { get; }
        private AverageCost averageCost;
        private NumberOfShares numberOfShares;
        private DateTime? removedDate;
        private bool isRemoved;

        private Holding() { }

        private Holding(StockId stockId, AverageCost averageCost, NumberOfShares numberOfShares)
        {
            StockId = stockId;
            this.averageCost = averageCost;
            this.numberOfShares = numberOfShares;
            AddedDate = DateTime.UtcNow;
            isRemoved = false;
            removedDate = null;
        }

        internal static Holding Create(StockId stockId, AverageCost averageCost, NumberOfShares numberOfShares)
        {
            return new Holding(stockId, averageCost, numberOfShares);
        }

        internal bool IsActive()
        {
            return !isRemoved;
        }

        internal void Update(AverageCost averageCost, NumberOfShares numberOfShares)
        {
            this.averageCost = averageCost;
            this.numberOfShares = numberOfShares;

            AddDomainEvent(new HoldingUpdatedDomainEvent(Id, averageCost, numberOfShares));
        }

        internal void Remove()
        {
            if (!this.isRemoved)
            {
                isRemoved = true;
                removedDate = DateTime.UtcNow;

                AddDomainEvent(new HoldingRemovedDomainEvent(Id));
            }
        }
    }
}