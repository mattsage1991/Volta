using System;
using System.Transactions;
using Volta.BuildingBlocks.Domain;
using Volta.BuildingBlocks.Domain.Entities;
using Volta.Customers.Domain.CustomerRegistrations.Events;
using Volta.Customers.Domain.CustomerRegistrations.Rules;
using Volta.Customers.Domain.Customers;

namespace Volta.Customers.Domain.CustomerRegistrations
{
    public class CustomerRegistration : Entity
    {
        public CustomerRegistrationId Id { get; }

        private string _email;

        private string _password;

        private string _firstName;

        private string _lastName;

        private DateTime _registerDate;

        private DateTime? _confirmedDate;

        public CustomerRegistration(string email, string password, string firstName, string lastName, ICustomerRegistrationValidator customerRegistrationValidator)
        {
            this.CheckRule(new EmailAddressMustBeUniqueRule(customerRegistrationValidator, email));

            this.Id = new CustomerRegistrationId(Guid.NewGuid());
            _email = email;
            _password = password;
            _firstName = firstName;
            _lastName = lastName;
            _registerDate = DateTime.UtcNow;
            
            AddDomainEvent(new CustomerRegisteredDomainEvent(this));
        }
        
        public Customer CreateCustomer()
        {
            return new Customer(
               this.Id,
               _email,
               _password,
               _firstName,
               _lastName);
        }

        public void Confirm()
        {
            _confirmedDate = DateTime.UtcNow;

            AddDomainEvent(new CustomerRegistrationConfirmedDomainEvent(this));
        }
    }
}