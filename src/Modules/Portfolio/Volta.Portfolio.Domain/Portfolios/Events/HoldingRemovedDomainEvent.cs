using Volta.BuildingBlocks.Domain;

namespace Volta.Portfolios.Domain.Portfolios.Events
{
    public class HoldingRemovedDomainEvent : DomainEvent
    {
        public HoldingId HoldingId { get; }

        public HoldingRemovedDomainEvent(HoldingId holdingId)
        {
            this.HoldingId = holdingId;
        }
    }
}