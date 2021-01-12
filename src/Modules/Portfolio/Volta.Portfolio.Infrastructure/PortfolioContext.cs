using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Volta.Portfolios.Domain.Portfolios;
using Volta.Portfolios.Infrastructure.Domain.Portfolios;

namespace Volta.Portfolios.Infrastructure
{
    public class PortfolioContext : DbContext
    {
        public DbSet<Portfolio> Portfolios { get; set; }

        public PortfolioContext(DbContextOptions<PortfolioContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PortfolioEntityTypeConfiguration());
        }

        public class OrderingContextDesignFactory : IDesignTimeDbContextFactory<PortfolioContext>
        {
            public PortfolioContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<PortfolioContext>()
                    .UseSqlServer("Server=.;Initial Catalog=PortfolioDb;Integrated Security=true");

                return new PortfolioContext(optionsBuilder.Options);
            }
        }
    }
}