using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Volta.BuildingBlocks.Infrastructure.EntityFramework;
using Volta.UserAccess.Domain.Users;

namespace Volta.UserAccess.Infrastructure
{
    public class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions<UsersContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(UsersContext)));
        }

        public class StockDbContextDesignFactory : IDesignTimeDbContextFactory<UsersContext>
        {
            public UsersContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<UsersContext>()
                    .ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>()
                    .UseSqlServer("Server=.;Initial Catalog=Database;Integrated Security=true");

                return new UsersContext(optionsBuilder.Options);
            }
        }
    }
}