using System;
using Volta.BuildingBlocks.Domain;

namespace Volta.Customers.Domain.CustomerRegistrations.Events
{
    public class CustomerRegistrationConfirmedDomainEvent : DomainEvent
    {
        public CustomerRegistration CustomerRegistration { get; }

        public CustomerRegistrationConfirmedDomainEvent(CustomerRegistration customerRegistration)
        {
            CustomerRegistration = customerRegistration ?? throw new ArgumentNullException(nameof(customerRegistration));
        }
    }
}