using Volta.BuildingBlocks.Domain;

namespace Volta.Customers.Domain.CustomerRegistrations.Rules
{
    public class EmailAddressMustBeUniqueRule : IBusinessRule
    {
        private readonly ICustomerRegistrationValidator _customerRegistrationValidator;
        private readonly string _emailAddress;

        public EmailAddressMustBeUniqueRule(ICustomerRegistrationValidator customerRegistrationValidator, string emailAddress)
        {
            _customerRegistrationValidator = customerRegistrationValidator;
            _emailAddress = emailAddress;
        }

        public bool IsBroken() => _customerRegistrationValidator.IsEmailAddressInUse(_emailAddress);

        public string Message => "Email address is already in use";
    }
}