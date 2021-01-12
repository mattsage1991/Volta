﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Volta.Stocks.Infrastructure;

namespace Volta.Stocks.Infrastructure.Migrations
{
    [DbContext(typeof(StocksContext))]
    [Migration("20210108161159_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Volta.Stocks")
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Volta.Stocks.Domain.Stocks.Stock", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("Volta.Stocks.Domain.Stocks.Stock", b =>
                {
                    b.OwnsOne("Volta.Stocks.Domain.Stocks.CompanyName", "companyName", b1 =>
                        {
                            b1.Property<Guid>("StockId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("CompanyName");

                            b1.HasKey("StockId");

                            b1.ToTable("Stocks");

                            b1.WithOwner()
                                .HasForeignKey("StockId");
                        });

                    b.OwnsOne("Volta.Stocks.Domain.Stocks.DividendYield", "dividendYield", b1 =>
                        {
                            b1.Property<Guid>("StockId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal?>("Value")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("DividendYield");

                            b1.HasKey("StockId");

                            b1.ToTable("Stocks");

                            b1.WithOwner()
                                .HasForeignKey("StockId");
                        });

                    b.OwnsOne("Volta.Stocks.Domain.Stocks.MarketCap", "marketCap", b1 =>
                        {
                            b1.Property<Guid>("StockId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<long>("Value")
                                .HasColumnType("bigint")
                                .HasColumnName("MarketCap");

                            b1.HasKey("StockId");

                            b1.ToTable("Stocks");

                            b1.WithOwner()
                                .HasForeignKey("StockId");
                        });

                    b.OwnsOne("Volta.Stocks.Domain.Stocks.PeRatio", "peRatio", b1 =>
                        {
                            b1.Property<Guid>("StockId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal?>("Value")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("PeRatio");

                            b1.HasKey("StockId");

                            b1.ToTable("Stocks");

                            b1.WithOwner()
                                .HasForeignKey("StockId");
                        });

                    b.OwnsOne("Volta.Stocks.Domain.Stocks.PegRatio", "pegRatio", b1 =>
                        {
                            b1.Property<Guid>("StockId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal?>("Value")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("PegRatio");

                            b1.HasKey("StockId");

                            b1.ToTable("Stocks");

                            b1.WithOwner()
                                .HasForeignKey("StockId");
                        });

                    b.OwnsOne("Volta.Stocks.Domain.Stocks.PriceToBookRatio", "priceToBookRatio", b1 =>
                        {
                            b1.Property<Guid>("StockId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal?>("Value")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("PriceToBookRatio");

                            b1.HasKey("StockId");

                            b1.ToTable("Stocks");

                            b1.WithOwner()
                                .HasForeignKey("StockId");
                        });

                    b.OwnsOne("Volta.Stocks.Domain.Stocks.ProfitMargin", "profitMargin", b1 =>
                        {
                            b1.Property<Guid>("StockId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<decimal>("Value")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("ProfitMargin");

                            b1.HasKey("StockId");

                            b1.ToTable("Stocks");

                            b1.WithOwner()
                                .HasForeignKey("StockId");
                        });

                    b.OwnsOne("Volta.Stocks.Domain.Stocks.TickerSymbol", "tickerSymbol", b1 =>
                        {
                            b1.Property<Guid>("StockId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("TickerSymbol");

                            b1.HasKey("StockId");

                            b1.ToTable("Stocks");

                            b1.WithOwner()
                                .HasForeignKey("StockId");
                        });

                    b.OwnsOne("Volta.Stocks.Domain.Stocks.TotalRevenue", "totalRevenue", b1 =>
                        {
                            b1.Property<Guid>("StockId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<long>("Value")
                                .HasColumnType("bigint")
                                .HasColumnName("TotalRevenue");

                            b1.HasKey("StockId");

                            b1.ToTable("Stocks");

                            b1.WithOwner()
                                .HasForeignKey("StockId");
                        });

                    b.Navigation("companyName");

                    b.Navigation("dividendYield");

                    b.Navigation("marketCap");

                    b.Navigation("pegRatio");

                    b.Navigation("peRatio");

                    b.Navigation("priceToBookRatio");

                    b.Navigation("profitMargin");

                    b.Navigation("tickerSymbol");

                    b.Navigation("totalRevenue");
                });
#pragma warning restore 612, 618
        }
    }
}
