using Volta.BuildingBlocks.Domain;
using Volta.BuildingBlocks.Domain.Entities;

namespace Volta.Portfolios.Domain.Members
{
    public class Member : Entity<MemberId>, IAggregateRoot
    {
        private string email;
    }
}