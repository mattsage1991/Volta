using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Volta.BuildingBlocks.Infrastructure;
using Volta.Stocks.Domain.Stocks;

namespace Volta.Stocks.Infrastructure
{
    public class StocksContext : DbContext
    {
        public StocksContext(DbContextOptions<StocksContext> options) : base(options)
        {
        }

        public DbSet<Stock> Stocks { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Volta.Stocks");

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(StocksContext)));

        }

        public class StockDbContextDesignFactory : IDesignTimeDbContextFactory<StocksContext>
        {
            public StocksContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<StocksContext>()
                    .ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>()
                    .UseSqlServer("Server=.;Initial Catalog=Database;Integrated Security=true");

                return new StocksContext(optionsBuilder.Options);
            }
        }
    }
}