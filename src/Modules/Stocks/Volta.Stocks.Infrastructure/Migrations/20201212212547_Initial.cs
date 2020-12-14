using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Volta.Stocks.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Volta.Stock");

            migrationBuilder.CreateTable(
                name: "Stocks",
                schema: "Volta.Stock",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyName = table.Column<string>(type: "varchar(50)", nullable: false),
                    Symbol = table.Column<string>(type: "varchar(5)", nullable: false),
                    MarketCap = table.Column<long>(type: "bigint", nullable: true),
                    PeRatio = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PegRatio = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PriceToBookRatio = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ProfitMargin = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalRevenue = table.Column<long>(type: "bigint", nullable: true),
                    DividendYield = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stocks",
                schema: "Volta.Stock");
        }
    }
}
