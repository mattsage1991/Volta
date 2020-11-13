using Volta.BuildingBlocks.Domain;

namespace Volta.Portfolios.Domain.Members
{
    public class Member : Entity, IAggregateRoot
    {
        public MemberId Id { get; private set; }

        private string _email;

    }
}