using Volta.BuildingBlocks.Domain;

namespace Volta.UserAccess.Domain.Users
{
    public class Email : ValueObject
    {
        public string Value { get; }

        private Email() { }

        private Email(string value)
        {
            this.Value = value;
        }

        public static Email Of(string value)
        {
            return new Email(value);
        }
    }
}