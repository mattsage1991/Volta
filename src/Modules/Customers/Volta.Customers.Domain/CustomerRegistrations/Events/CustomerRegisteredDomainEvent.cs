using System;
using Volta.BuildingBlocks.Domain;

namespace Volta.Customers.Domain.CustomerRegistrations.Events
{
    public class CustomerRegisteredDomainEvent : DomainEventBase
    {
        public CustomerRegistrationId CustomerRegistrationId { get; }

        public string Email { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public DateTime RegisterDate { get; }

        public CustomerRegisteredDomainEvent(
            CustomerRegistrationId customerRegistrationId,
            string email,
            string firstName,
            string lastName,
            DateTime registerDate)
        {
            CustomerRegistrationId = customerRegistrationId;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            RegisterDate = registerDate;
        }
    }
}