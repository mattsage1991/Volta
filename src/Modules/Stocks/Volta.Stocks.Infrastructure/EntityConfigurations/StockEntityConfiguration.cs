using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volta.Stocks.Domain.Stocks;

namespace Volta.Stocks.Infrastructure.EntityConfigurations
{
    public class StockEntityConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.Property(x => x.Id).HasConversion(x => x.Value, x => StockId.Of(x));
            builder.Property(x => x.CompanyName).IsRequired().HasColumnType("varchar(50)");
            builder.Property(x => x.Symbol).IsRequired().HasColumnType("varchar(5)");
            builder.OwnsOne(x => x.KeyStats).Property(x => x.MarketCap).HasColumnName("MarketCap");
            builder.OwnsOne(x => x.KeyStats).Property(x => x.TotalRevenue).HasColumnName("TotalRevenue");
            builder.OwnsOne(x => x.KeyStats).Property(x => x.DividendYield).HasColumnName("DividendYield");
            builder.OwnsOne(x => x.KeyStats).Property(x => x.PeRatio).HasColumnName("PeRatio");
            builder.OwnsOne(x => x.KeyStats).Property(x => x.PegRatio).HasColumnName("PegRatio");
            builder.OwnsOne(x => x.KeyStats).Property(x => x.PriceToBookRatio).HasColumnName("PriceToBookRatio");
            builder.OwnsOne(x => x.KeyStats).Property(x => x.ProfitMargin).HasColumnName("ProfitMargin");

            builder.Ignore(x => x.DomainEvents);
        }
    }
}