using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volta.Portfolios.Domain.Portfolios;
using Volta.Portfolios.Domain.Stocks;

namespace Volta.Portfolios.Infrastructure.EntityConfigurations
{
    public class PortfolioEntityConfiguration : IEntityTypeConfiguration<Portfolio>
    {
        public void Configure(EntityTypeBuilder<Portfolio> builder)
        {
            builder.HasKey(x => x.Id);

            builder.OwnsOne<MemberId>("memberId", b =>
            {
                b.Property(x => x.Value).HasColumnName("MemberId").IsRequired();
            });

            builder.OwnsOne<PortfolioName>("portfolioName", b =>
            {
                b.Property(x => x.Value).HasColumnName("portfolioName");
            });
            
            builder.Property("createdDate").HasColumnName("CreatedDate");

            builder.HasMany<Holding>("holdings");

            builder.Ignore(x => x.DomainEvents);
        }
    }
}