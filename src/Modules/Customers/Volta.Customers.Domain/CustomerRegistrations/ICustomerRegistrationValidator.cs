namespace Volta.Customers.Domain.CustomerRegistrations
{
    public interface ICustomerRegistrationValidator
    {
        bool IsEmailAddressInUse(string emailAddress);
    }
}