using Volta.BuildingBlocks.Domain;

namespace Volta.Portfolios.Domain.Portfolios.Events
{
    public class HoldingUpdatedDomainEvent : DomainEvent
    {
        public HoldingId HoldingId { get; }
        public AverageCost AverageCost { get; }
        public NumberOfShares NumberOfShares { get; }

        public HoldingUpdatedDomainEvent(HoldingId holdingId, AverageCost averageCost, NumberOfShares numberOfShares)
        {
            HoldingId = holdingId;
            AverageCost = averageCost;
            NumberOfShares = numberOfShares;
        }
    }
}