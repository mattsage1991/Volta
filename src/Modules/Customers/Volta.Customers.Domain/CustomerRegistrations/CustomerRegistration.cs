using System;
using System.Transactions;
using Volta.BuildingBlocks.Domain;
using Volta.Customers.Domain.CustomerRegistrations.Events;
using Volta.Customers.Domain.Customers;

namespace Volta.Customers.Domain.CustomerRegistrations
{
    public class CustomerRegistration : Entity, IAggregateRoot
    {
        public CustomerRegistrationId Id { get; set; }

        private string _email;

        private string _password;

        private string _firstName;

        private string _lastName;

        private DateTime _registerDateTime;

        private CustomerRegistration()
        {
        }

        private CustomerRegistration(string email, string password, string firstName, string lastName)
        {
            this.Id = new CustomerRegistrationId(Guid.NewGuid());
            _email = email;
            _password = password;
            _firstName = firstName;
            _lastName = lastName;
            _registerDateTime = DateTime.UtcNow;
            
            AddDomainEvent(new CustomerRegisteredDomainEvent(
                this.Id,
                _email,
                _firstName,
                _lastName,
                _registerDateTime));
        }

        public static CustomerRegistration RegisterNewCustomer(
            string email,
            string password,
            string firstName,
            string lastName)
        {
            return new CustomerRegistration(email, password, firstName, lastName);
        }

        public Customer CreateCustomer()
        {
            return Customer.CreateFromCustomerRegistration(
               this.Id,
               _email,
               _password,
               _firstName,
               _lastName);
        }
    }
}