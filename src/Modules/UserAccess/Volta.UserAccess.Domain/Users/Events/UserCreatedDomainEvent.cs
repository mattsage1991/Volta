using Volta.BuildingBlocks.Domain;

namespace Volta.UserAccess.Domain.Users.Events
{
    public class UserCreatedDomainEvent : DomainEvent
    {
        public UserId UserId { get; }
        public Email Email { get; }
        public CreatedDate CreatedDate { get; }

        public UserCreatedDomainEvent(UserId userId, Email email, CreatedDate createdDate)
        {
            UserId = userId;
            Email = email;
            CreatedDate = createdDate;
        }
    }
}