using Volta.BuildingBlocks.Domain;

namespace Volta.Customers.Domain.Customers.Events
{
    public class CustomerCreatedDomainEvent : DomainEventBase
    {
        public CustomerCreatedDomainEvent(CustomerId id)
        {
            Id = id;
        }

        public new CustomerId Id { get; }
    }
}