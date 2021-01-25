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
            builder.HasKey(x => x.Id);

            builder.OwnsOne<CompanyName>("companyName", b =>
            {
                b.Property(x => x.Value).HasColumnName("CompanyName");
            });

            builder.OwnsOne<TickerSymbol>("tickerSymbol", b =>
            {
                b.Property(x => x.Value).HasColumnName("TickerSymbol");
            });

            builder.OwnsOne<MarketCap>("marketCap", b =>
            {
                b.Property(x => x.Value).HasColumnName("MarketCap");
            });

            builder.OwnsOne<PeRatio>("peRatio", b =>
            {
                b.Property(x => x.Value).HasColumnName("PeRatio");
            });

            builder.OwnsOne<PegRatio>("pegRatio", b =>
            {
                b.Property(x => x.Value).HasColumnName("PegRatio");
            });

            builder.OwnsOne<PriceToBookRatio>("priceToBookRatio", b =>
            {
                b.Property(x => x.Value).HasColumnName("PriceToBookRatio");
            });

            builder.OwnsOne<ProfitMargin>("profitMargin", b =>
            {
                b.Property(x => x.Value).HasColumnName("ProfitMargin");
            });

            builder.OwnsOne<TotalRevenue>("totalRevenue", b =>
            {
                b.Property(x => x.Value).HasColumnName("TotalRevenue");
            });

            builder.OwnsOne<DividendYield>("dividendYield", b =>
            {
                b.Property(x => x.Value).HasColumnName("DividendYield");
            });

            builder.Property("lastUpdatedDate").HasColumnName("LastUpdatedDate");
            
            builder.Ignore(x => x.DomainEvents);
        }
    }
}