using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volta.Portfolios.Domain.PortfolioOwners;
using Volta.Portfolios.Domain.Portfolios;
using Volta.Portfolios.Domain.Stocks;

namespace Volta.Portfolios.Infrastructure.EntityConfigurations
{
    public class PortfolioEntityConfiguration : IEntityTypeConfiguration<Portfolio>
    {
        public void Configure(EntityTypeBuilder<Portfolio> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne<PortfolioOwner>().WithMany().HasForeignKey("portfolioOwnerId").IsRequired();
            builder.Property(x => x.portfolioOwnerId).HasColumnName("PortfolioOwnerId").IsRequired(true);
            
            builder.OwnsOne<PortfolioName>("portfolioName", b =>
            {
                b.Property(x => x.Value).HasColumnName("PortfolioName");
            });

            builder.OwnsOne<PortfolioCreatedDate>("portfolioCreatedDate", b =>
            {
                b.Property(x => x.Value).HasColumnName("PortfolioCreatedDate");
            });
            
            builder.HasMany<Holding>("holdings");

            builder.Ignore(x => x.DomainEvents);
        }
    }
}