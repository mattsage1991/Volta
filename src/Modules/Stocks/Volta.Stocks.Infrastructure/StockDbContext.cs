using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Volta.Stocks.Domain.Stocks;
using Volta.Stocks.Infrastructure.EntityConfigurations;

namespace Volta.Stocks.Infrastructure
{
    public class StockDbContext : DbContext
    {
        public StockDbContext(DbContextOptions<StockDbContext> options) : base(options)
        {
        }

        public DbSet<Stock> Stocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Volta.Stock");

            modelBuilder.ApplyConfiguration(new StockEntityConfiguration());
        }

        public class StockDbContextDesignFactory : IDesignTimeDbContextFactory<StockDbContext>
        {
            public StockDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<StockDbContext>()
                    .UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=StocksDb;Integrated Security=SSPI;");

                return new StockDbContext(optionsBuilder.Options);
            }
        }
    }
}