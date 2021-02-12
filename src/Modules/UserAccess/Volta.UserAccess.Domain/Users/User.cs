using System;
using System.Net.Sockets;
using Volta.BuildingBlocks.Domain.Entities;
using Volta.UserAccess.Domain.Users.Events;

namespace Volta.UserAccess.Domain.Users
{
    public class User : Entity<UserId>, IAggregateRoot
    {
        private Title title;
        private FirstName firstName;
        private LastName lastName;
        private Address address;
        private PhoneNumber phoneNumber;
        private Email email;
        private Password password;
        private CreatedDate createdDate;
        private bool isActive;

        private User() { }

        private User(Email email, Password password)
        {
            Id = new UserId(Guid.NewGuid());
            this.email = email;
            this.password = password;
            this.isActive = true;
            this.createdDate = CreatedDate.Of(DateTime.UtcNow);

            AddDomainEvent(new UserCreatedDomainEvent(this.Id, this.email, this.createdDate));
        }

        public static User Register(Email email, Password password)
        {
            return new User(email, password);
        }
    }
}