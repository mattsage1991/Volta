using System.Collections.Generic;
using Volta.BuildingBlocks.Domain.Events;
using Volta.BuildingBlocks.Domain.ValueObjects;

namespace Volta.BuildingBlocks.Domain.Entities
{
    public abstract class Entity<TId> : IEntity
        where TId : TypedIdValueBase
    {
        private List<IDomainEvent> domainEvents;

        public TId Id { get; protected set; }

        public IReadOnlyCollection<IDomainEvent> DomainEvents => this.domainEvents?.AsReadOnly();

        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            domainEvents ??= new List<IDomainEvent>();
            this.domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            domainEvents?.Clear();
        }

        protected void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }

        public override bool Equals(object obj)
        {
            var other = obj as Entity<TId>;

            if (ReferenceEquals(other, null))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (GetType() != other.GetType())
            {
                return false;
            }

            return Id == other.Id;
        }


        public static bool operator ==(Entity<TId> a, Entity<TId> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            {
                return true;
            }

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            {
                return false;
            }

            return a.Equals(b);
        }

        public static bool operator !=(Entity<TId> a, Entity<TId> b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().ToString() + Id.Value).GetHashCode();
        }
    }
}
