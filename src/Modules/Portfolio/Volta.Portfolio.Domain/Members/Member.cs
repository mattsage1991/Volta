using Volta.BuildingBlocks.Domain;
using Volta.BuildingBlocks.Domain.Entities;

namespace Volta.Portfolios.Domain.Members
{
    public class Member : Entity, IAggregateRoot
    {
        public MemberId Id { get; private set; }

        private string _email;

    }
}