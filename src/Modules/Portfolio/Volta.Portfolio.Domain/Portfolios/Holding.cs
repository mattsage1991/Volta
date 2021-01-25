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
        internal AverageCost AverageCost { get; }
        internal NumberOfShares NumberOfShares { get; }
        internal DateTime AddedDate;
        internal DateTime? RemovedDate;
        internal bool IsRemoved;

        private Holding() { }

        private Holding(StockId stockId, AverageCost averageCost, NumberOfShares numberOfShares)
        {
            StockId = stockId;
            AverageCost = averageCost;
            NumberOfShares = numberOfShares;
            AddedDate = DateTime.UtcNow;
            IsRemoved = false;
            RemovedDate = null;
        }

        internal static Holding Create(StockId stockId, AverageCost averageCost, NumberOfShares numberOfShares)
        {
            return new Holding(stockId, averageCost, numberOfShares);
        }

        internal bool IsActive()
        {
            return !IsRemoved;
        }

        internal void Remove()
        {
            if (!this.IsRemoved)
            {
                IsRemoved = true;
                RemovedDate = DateTime.UtcNow;

                AddDomainEvent(new HoldingRemovedDomainEvent(Id));
            }
        }
    }
}