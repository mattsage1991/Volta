using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volta.Portfolios.Domain.PortfolioOwners;
using Volta.Portfolios.Domain.Portfolios;
using Volta.Portfolios.Domain.Stocks;

namespace Volta.Portfolios.Infrastructure.EntityConfigurations
{
    public class PortfolioOwnerEntityConfiguration : IEntityTypeConfiguration<PortfolioOwner>
    {
        public void Configure(EntityTypeBuilder<PortfolioOwner> builder)
        {
            builder.HasKey(x => x.Id);

            builder.OwnsOne<CreatedDate>("createdDate", b =>
            {
                b.Property(x => x.Value).HasColumnName("CreatedDate");
            });

            builder.Ignore(x => x.DomainEvents);
        }
    }
}