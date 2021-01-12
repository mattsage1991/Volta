using System;
using Volta.BuildingBlocks.Domain;

namespace Volta.Customers.Domain.CustomerRegistrations.Events
{
    public class CustomerRegisteredDomainEvent : DomainEvent
    {
        public CustomerRegistration CustomerRegistration { get; }
        
        public CustomerRegisteredDomainEvent(CustomerRegistration customerRegistration)
        {
            CustomerRegistration = customerRegistration ?? throw new ArgumentNullException(nameof(customerRegistration));
        }
    }
}