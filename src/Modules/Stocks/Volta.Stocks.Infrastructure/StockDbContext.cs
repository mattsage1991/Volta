using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage;
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

        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;
        private IDbContextTransaction _currentTransaction;

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
                    .UseSqlServer("Server=.;Initial Catalog=Stocks;Integrated Security=true");

                return new StockDbContext(optionsBuilder.Options);
            }
        }
    }
}