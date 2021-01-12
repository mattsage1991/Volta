using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volta.Portfolios.Domain.Portfolios;
using Volta.Portfolios.Domain.Stocks;

namespace Volta.Portfolios.Infrastructure.Domain.Portfolios
{
    public class PortfolioEntityTypeConfiguration : IEntityTypeConfiguration<Portfolio>
    {
        public void Configure(EntityTypeBuilder<Portfolio> builder)
        {
            builder.ToTable("Portfolios", "portfolios");

            builder.HasKey(x => x.Id);

            builder.Property<string>("_name").HasColumnName("Name");
            builder.Property<DateTime>("_createDate").HasColumnName("CreateDate");

            builder.OwnsMany<PortfolioHolding>("_holdings", y =>
            {
                y.WithOwner().HasForeignKey("PortfolioId");
                y.Property<HoldingId>("HoldingId");
                y.Property<PortfolioId>("PortfolioId");
                y.Property<DateTime>("_addedDate");
                y.Property<int>("_quantity");
                y.Property<bool>("_isRemoved");
                y.Property<DateTime>("_removedDate");

                y.OwnsOne<MoneyValue>("_averagePrice", b =>
                {
                    b.Property(p => p.Value).HasColumnName("SharePriceValue");
                    b.Property(p => p.Currency).HasColumnName("SharePriceCurrency");
                });
            });
        }
    }
}