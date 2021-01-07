using System;

namespace Volta.BuildingBlocks.Application.EventBus
{
    public class IntegrationEvent
    {
        public Guid Id { get; }

        public DateTime OccurredOn { get; }

        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            OccurredOn = DateTime.UtcNow;
        }

        public IntegrationEvent(Guid id, DateTime occurredOn)
        {
            Id = id;
            OccurredOn = occurredOn;
        }
    }
}