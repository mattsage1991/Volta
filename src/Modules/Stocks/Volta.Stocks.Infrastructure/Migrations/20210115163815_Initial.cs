using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Volta.Stocks.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DividendYield = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MarketCap = table.Column<long>(type: "bigint", nullable: true),
                    PeRatio = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PegRatio = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PriceToBookRatio = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ProfitMargin = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TickerSymbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalRevenue = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stocks");
        }
    }
}
