using Volta.BuildingBlocks.Domain;

namespace Volta.Customers.Domain.Customers.Events
{
    public class CustomerCreatedDomainEvent : DomainEvent
    {
        public CustomerCreatedDomainEvent(CustomerId id)
        {
            Id = id;
        }

        public new CustomerId Id { get; }
    }
}