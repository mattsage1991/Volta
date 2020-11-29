using System;
using FluentAssertions;
using Moq;
using Volta.BuildingBlocks.Domain;
using Volta.Customers.Domain.CustomerRegistrations;
using Volta.Customers.Domain.CustomerRegistrations.Events;
using Volta.Customers.Domain.CustomerRegistrations.Rules;
using Xunit;

namespace Volta.Customers.Domain.Tests.CustomerRegistrations
{
    public class CustomerRegistrationTests
    {
        [Fact]
        public void WhenEmailAddressIsUnique_RegistrationIsSuccessful()
        {
            var email = "john.doe@gmail.com";
            var password = "password";
            var firstName = "John";
            var lastName = "Doe";
            var customerRegistrationValidator = new Mock<ICustomerRegistrationValidator>();
            customerRegistrationValidator.Setup(x => x.IsEmailAddressInUse(email)).Returns(false);


            var customerRegistration = new CustomerRegistration(email, password, firstName, lastName, customerRegistrationValidator.Object);

            customerRegistration.DomainEvents.Should().ContainItemsAssignableTo<CustomerRegisteredDomainEvent>();
        }

        [Fact]
        public void WhenEmailAddressIsNotUnique_BreaksEmailMustBeUniqueRule()
        {
            var email = "john.doe@gmail.com";
            var password = "password";
            var firstName = "John";
            var lastName = "Doe";

            var customerRegistrationValidator = new Mock<ICustomerRegistrationValidator>();
            customerRegistrationValidator.Setup(x => x.IsEmailAddressInUse(email)).Returns(true);

            Action act = () => new CustomerRegistration(email, password, firstName, lastName, customerRegistrationValidator.Object);

            act.Should().Throw<BusinessRuleValidationException>().WithMessage(new EmailAddressMustBeUniqueRule(customerRegistrationValidator.Object, email).Message);
        }
    }
}