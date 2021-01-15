using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volta.BuildingBlocks.Domain.Entities;
using Volta.BuildingBlocks.Domain.ValueObjects;

namespace Volta.BuildingBlocks.Test.Mocks
{

    public class MockEntityId : TypedIdValueBase
    {
        public MockEntityId(Guid value) : base(value)
        {
        }
    }

    public class MockEntity : Entity<MockEntityId>
    {
        private MockEntity()
        {
            // Only for EF
        }

        public MockEntity(MockEntityId id)
        {
            Id = id;
        }

        public string Foo { get; set; }

        public void CreateBarEvent(string bar)
        {
            this.Foo = bar;
            this.AddDomainEvent(new MockEvent(this));
        }
    }

    public class MockEntityConfiguration : IEntityTypeConfiguration<MockEntity>
    {
        public void Configure(EntityTypeBuilder<MockEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Foo);

            builder.Ignore(x => x.DomainEvents);
        }
    }
}