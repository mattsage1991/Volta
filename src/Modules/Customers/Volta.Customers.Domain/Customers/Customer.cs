using Volta.BuildingBlocks.Domain;
using Volta.BuildingBlocks.Domain.Entities;
using Volta.Customers.Domain.CustomerRegistrations;
using Volta.Customers.Domain.Customers.Events;

namespace Volta.Customers.Domain.Customers
{
    public class Customer : Entity<CustomerId>, IAggregateRoot
    {
        private string email;

        private string password;

        private bool isActive;

        private string firstName;

        private string lastName;

        private Customer()
        {
        }

        public Customer(CustomerRegistrationId customerRegistrationId, string email, string password, string firstName, string lastName)
        {
            this.Id = new CustomerId(customerRegistrationId.Value);
            this.email = email;
            this.password = password;
            this.firstName = firstName;
            this.lastName = lastName;
            this.isActive = true;

            AddDomainEvent(new CustomerCreatedDomainEvent(this.Id));
        }
    }
}