using Volta.BuildingBlocks.Domain;

namespace Volta.UserAccess.Domain.Users
{
    public class PhoneNumber : ValueObject
    {
        public string Value { get; }

        private PhoneNumber() { }

        private PhoneNumber(string value)
        {
            this.Value = value;
        }

        public static PhoneNumber Of(string value)
        {
            return new PhoneNumber(value);
        }
    }
}