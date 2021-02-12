using System;
using Volta.BuildingBlocks.Domain;
using Volta.UserAccess.Domain.Users.Services;

namespace Volta.UserAccess.Domain.Users
{
    public class Password : ValueObject
    {
        public string Hash { get; }

        private Password() { }

        private Password(string password, IPasswordHasher passwordHasher)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("password cannot be null or empty", nameof(password));

            if (passwordHasher is null)
                throw new ArgumentNullException(nameof(passwordHasher));

            var hash = passwordHasher.HashPassword(password);

            this.Hash = hash;
        }

        public static Password Of(string password, IPasswordHasher passwordHasher)
        {
            return new Password(password, passwordHasher);
        }
    }
}